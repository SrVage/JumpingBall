using Code.Abstractions;
using Code.Components;
using Code.Configs;
using Leopotam.Ecs;
using UnityEngine;
using Time = Code.Components.Time;

namespace Code.Gameplay.Systems
{
    public sealed class CreateEnemySystem:IEcsRunSystem
    {
        private readonly EcsFilter<GameObjectRef, Stair> _stairs = null;
        private readonly EcsFilter<GeneralStairsNumber> _number = null;
        private readonly EcsFilter<Time, EnemyTag>.Exclude<GameObjectRef> _time = null;
        private readonly EcsWorld _world = null;
        private readonly SceneObjects _sceneObjects;
        public void Run()
        {
            if (!_time.IsEmpty())
                return;
            foreach (var ndx in _number)
            {
                ref var maxStair = ref _number.Get1(ndx).MaxValue;
                foreach (var sdx in _stairs)
                {
                    ref var stairNumber = ref _stairs.Get2(sdx).Number;
                    if (stairNumber!=maxStair)
                        continue;
                    ref var transform = ref _stairs.Get1(sdx).Transform;
                    var enemy = GameObject.Instantiate(_sceneObjects.EnemyPrefab[Random.Range(0, 2)],
                        transform.position+new Vector3(Random.Range(-2,3),0,0), Quaternion.identity);
                    enemy.GetComponent<MonoBehavioursEntity>().Initial(_world.NewEntity(), _world);
                    var time = _world.NewEntity();
                        time.Get<Time>().Value = 5;
                        time.Get<EnemyTag>();
                }
            }
        }
    }
}