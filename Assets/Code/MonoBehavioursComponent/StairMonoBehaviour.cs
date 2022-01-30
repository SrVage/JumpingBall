using Code.Abstractions;
using Code.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Code.MonoBehavioursComponent
{
    public class StairMonoBehaviour:MonoBehavioursEntity
    {
        [SerializeField] private int _number;
        public override void Initial(EcsEntity entity, EcsWorld world)
        {
            base.Initial(entity, world);
            entity.Get<Stair>().Number = _number;
        }
    }
}