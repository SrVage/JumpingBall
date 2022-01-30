using System;
using Code.Abstractions;
using Code.Components;
using Code.Gameplay.Extensions;
using Leopotam.Ecs;

namespace Code.Gameplay.Systems
{
    public class DestroyEnemySystem:IEcsRunSystem
    {
        private readonly EcsFilter<GameObjectRef, PlayerTag> _player = null;
        private readonly EcsFilter<GameObjectRef, EnemyTag>.Exclude<Time> _enemy = null;
        
        public void Run()
        {
            foreach (var edx in _enemy)
            {
                ref var transformEnemy = ref _enemy.Get1(edx).Transform;
                foreach (var pdx in _player)
                {
                    ref var transformPlayer = ref _player.Get1(pdx).Transform;
                    if ((transformPlayer.position.y - transformEnemy.position.y) > 6)
                    {
                        ref var entity = ref _enemy.GetEntity(edx);
                        entity.DestroyWithGameObject();
                    }
                }
            }
        }
    }
}