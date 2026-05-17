using System.Collections.Generic;
using UnityEngine;

namespace CoreBreach
{
    public class ObjectPool<T> where T : Component, IPoolable
    {
        readonly T prefab;
        readonly Transform parent;
        readonly Stack<T> available = new Stack<T>();

        public ObjectPool(T prefab, int prewarm, Transform parent)
        {
            this.prefab = prefab;
            this.parent = parent;

            for (int i = 0; i < prewarm; i++)
            {
                var inst = Object.Instantiate(prefab, parent);
                inst.gameObject.SetActive(false);
                available.Push(inst);
            }
        }

        public T Get(Vector3 position, Quaternion rotation)
        {
            T inst;
            if (available.Count > 0)
            {
                inst = available.Pop();
                inst.transform.SetPositionAndRotation(position, rotation);
            }
            else
            {
                inst = Object.Instantiate(prefab, position, rotation, parent);
            }
            inst.gameObject.SetActive(true);
            inst.OnSpawn();
            return inst;
        }

        public void Release(T instance)
        {
            if (instance == null) return;
            instance.OnDespawn();
            instance.gameObject.SetActive(false);
            available.Push(instance);
        }
    }
}
