using System.Security.Cryptography;
using Code.Abstractions;
using Code.Components;
using Code.Gameplay.Extensions;
using Code.StatesSwitcher;
using Leopotam.Ecs;

namespace Code.Gameplay.Systems
{
    public sealed class PlayerFallSystem:IEcsRunSystem
    {
        private const int FallVelocity = -10;
        private readonly EcsFilter<GameObjectRef, Physic, PlayerTag>.Exclude<IsMoving> _player = null;
        
        public void Run()
        {
            foreach (var pdx in _player)
            {
                ref var physic = ref _player.Get2(pdx).Value;
                ref var entity = ref _player.GetEntity(pdx);
                if (physic.velocity.y < FallVelocity)
                {
                    entity.Get<Destroy>();
                }
            }
        }
    }
}