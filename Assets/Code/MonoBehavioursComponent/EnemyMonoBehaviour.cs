using Code.Abstractions;
using Code.Components;
using Leopotam.Ecs;
using UnityEngine;
using Time = Code.Components.Time;

namespace Code.MonoBehavioursComponent
{
    public enum Move
    {
        Down = 0,
        Random = 1
    }
    public class EnemyMonoBehaviour:MonoBehavioursEntity
    {
        [SerializeField] private Move _move;
        public override void Initial(EcsEntity entity, EcsWorld world)
        {
            base.Initial(entity, world);
            entity.Get<EnemyTag>();
            if (_move == Move.Random)
                entity.Get<RandomTag>();
            entity.Get<Time>().Value = Random.Range(0.5f, 2f);
            entity.Get<Done>();
            GetComponentInChildren<TriggerListener>().Initial(world);
        }
    }
}