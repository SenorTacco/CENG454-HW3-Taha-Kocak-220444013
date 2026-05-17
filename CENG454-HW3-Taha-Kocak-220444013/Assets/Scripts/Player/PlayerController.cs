using UnityEngine;

namespace CoreBreach
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] float moveSpeed = 6f;
        [SerializeField] float baseFireCooldown = 0.22f;
        [SerializeField] int baseProjectileDamage = 1;
        [SerializeField] Transform muzzle;
        [SerializeField] PoolService poolService;

        Rigidbody2D rb;
        Camera cam;
        Vector2 moveInput;
        Vector2 aimDir = Vector2.right;
        IWeapon weapon;

        void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            cam = Camera.main;
            weapon = new BaseWeapon(baseProjectileDamage, baseFireCooldown);
        }

        void Update()
        {
            ReadInput();
            UpdateAim();
            weapon.Tick(Time.deltaTime);
            HandleFire();
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
            if (!weapon.CanFire) return;
            if (poolService == null) return;

            Vector2 origin = muzzle != null ? (Vector2)muzzle.position : (Vector2)transform.position;
            int count = weapon.ProjectileCount;
            float spread = weapon.SpreadDegrees;
            int dmg = weapon.Damage;

            for (int i = 0; i < count; i++)
            {
                float t = count > 1 ? (i / (float)(count - 1)) : 0.5f;
                float offset = (count > 1) ? Mathf.Lerp(-spread * 0.5f, spread * 0.5f, t) : 0f;
                Vector2 shot = RotateBy(aimDir, offset);
                poolService.SpawnProjectile(origin, shot, dmg);
            }
            weapon.NoteFired();
        }

        static Vector2 RotateBy(Vector2 v, float angleDeg)
        {
            float r = angleDeg * Mathf.Deg2Rad;
            float c = Mathf.Cos(r);
            float s = Mathf.Sin(r);
            return new Vector2(v.x * c - v.y * s, v.x * s + v.y * c);
        }

        public void ApplyUpgrade(UpgradeKind kind)
        {
            switch (kind)
            {
                case UpgradeKind.Damage:    weapon = new DamageUpDecorator(weapon); break;
                case UpgradeKind.RapidFire: weapon = new RapidFireDecorator(weapon); break;
                case UpgradeKind.MultiShot: weapon = new MultiShotDecorator(weapon); break;
            }
        }
    }
}
