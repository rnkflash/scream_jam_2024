using System;
using System.Collections.Generic;
using _Project.Scripts.utils;
using UnityEngine;
using UnityEngine.Pool;

namespace _Project.Scripts.player
{
    public class GameObjectPool: Singleton<GameObjectPool>
    {
        [SerializeField] private bool collectionCheck = true;
        [SerializeField] private int defaultCapacity = 100;
        [SerializeField] private int maxPoolSize = 100;

        private Dictionary<GameObject, ObjectPool<GameObject>> pools = new();
        
        private ObjectPool<GameObject> CreatePool(GameObject prefab)
        {
            return new ObjectPool<GameObject>(
                () => Instantiate(prefab), 
                o => o.SetActive(true), 
                o => o.SetActive(false), 
                Destroy, 
                collectionCheck, 
                defaultCapacity, 
                maxPoolSize
            );
        }

        private ObjectPool<GameObject> GetPool(GameObject prefab)
        {
            if (pools.ContainsKey(prefab))
            {
                return pools[prefab];
            }
            else
            {
                var pool = CreatePool(prefab);
                pools[prefab] = pool;
                return pool;
            }
        }

        public GameObject GetObject(GameObject prefab)
        {
            var pool = GetPool(prefab);
            return pool.Get();
        }

        public void ReturnObject(GameObject prefab, GameObject obj)
        {
            obj.transform.parent = transform;
            var pool = GetPool(prefab);
            pool.Release(obj);
        }
    }
}