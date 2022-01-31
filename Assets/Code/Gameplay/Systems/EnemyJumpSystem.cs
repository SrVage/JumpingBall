using System;
using Code.Abstractions;
using Code.Components;
using DG.Tweening;
using Leopotam.Ecs;
using UnityEngine;
using Random = UnityEngine.Random;
using Time = Code.Components.Time;

namespace Code.Gameplay.Systems
{
    public class EnemyJumpSystem:IEcsRunSystem
    {
        private readonly EcsFilter<GameObjectRef, EnemyTag, Done>.Exclude<Time> _enemy = null;
        
        public void Run()
        {
            foreach (var edx in _enemy)
            {
                ref var transform = ref _enemy.Get1(edx).Transform;
                var entity = _enemy.GetEntity(edx);
                entity.Del<Done>();
                int stair = Random.Range(1, 4);
                Vector3 position = transform.position;
                Vector3 target = Vector3.zero;
                if (entity.Has<RandomTag>())
                    target = new Vector3(Random.Range(-2, 3), position.y-stair, position.z+stair);
                else
                    target = new Vector3(position.x, position.y-stair, position.z+stair);
                transform.DOJump(target, stair+1, 1, 0.5f*stair).SetEase(Ease.InQuad).OnComplete(() =>
                {
                    entity.Get<Time>().Value = Random.Range(0.5f, 2f);
                    entity.Get<Done>();
                });
            }
        }
    }
}