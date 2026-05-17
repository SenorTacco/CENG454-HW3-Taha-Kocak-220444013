using UnityEngine;

namespace CoreBreach
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] float moveSpeed = 6f;
        [SerializeField] float fireCooldown = 0.22f;
        [SerializeField] int projectileDamage = 1;
        [SerializeField] Transform muzzle;
        [SerializeField] PoolService poolService;

        Rigidbody2D rb;
        Camera cam;
        Vector2 moveInput;
        Vector2 aimDir = Vector2.right;
        float fireTimer;

        void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            cam = Camera.main;
        }

        void Update()
        {
            ReadInput();
            UpdateAim();
            HandleFire();
            if (fireTimer > 0f) fireTimer -= Time.deltaTime;
        }

        void FixedUpdate()
        {
            rb.linearVelocity = moveInput * moveSpeed;
        }

        void ReadInput()
        {
            moveInput.x = Input.GetAxisRaw("Horizontal");
            moveInput.y = Input.GetAxisRaw("Vertical");
            if (moveInput.sqrMagnitude > 1f) moveInput.Normalize();
        }

        void UpdateAim()
        {
            Vector3 mouseW = cam.ScreenToWorldPoint(Input.mousePosition);
            aimDir = ((Vector2)mouseW - (Vector2)transform.position).normalized;
            float angle = Mathf.Atan2(aimDir.y, aimDir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, angle);
        }

        void HandleFire()
        {
            if (!Input.GetMouseButton(0)) return;
            if (fireTimer > 0f) return;
            if (poolService == null) return;

            Vector2 spawn = muzzle != null ? (Vector2)muzzle.position : (Vector2)transform.position;
            poolService.SpawnProjectile(spawn, aimDir, projectileDamage);
            fireTimer = fireCooldown;
        }
    }
}
