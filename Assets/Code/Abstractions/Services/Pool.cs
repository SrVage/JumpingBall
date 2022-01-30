using System.Collections.Generic;
using Code.Abstractions.Interfaces;
using Leopotam.Ecs;
using UnityEngine;

namespace Code.Abstractions.Services
{
    public class StairPool:IPool<GameObject>
    {
        private Queue<GameObject> _stairs;
        private readonly int _capacityPool;
        private Transform _rootPool;
        
        public StairPool(int capacityPool, GameObject prefab, EcsWorld world)
        {
            _stairs = new Queue<GameObject>();
            _capacityPool = capacityPool;
            if (!_rootPool)
            {
                _rootPool = new GameObject("RootPool").transform;
            }
            for (int i = 0; i < _capacityPool; i++)
            {
                var stair = GameObject.Instantiate(prefab, Vector3.zero, Quaternion.identity);
                stair.GetComponent<MonoBehavioursEntity>().Initial(world.NewEntity(), world);
                stair.transform.SetParent(_rootPool);
                stair.SetActive(false);
                _stairs.Enqueue(stair);
            }
        }
        
        public GameObject GetObject()
        {
            var stair = _stairs.Dequeue();
            return stair;
        }

        public void ReturnToPool(GameObject obj)
        {
            obj.SetActive(false);
            _stairs.Enqueue(obj);
            obj.transform.position = new Vector3(0, 0, -2);
            obj.transform.SetParent(_rootPool);
        }
    }
}