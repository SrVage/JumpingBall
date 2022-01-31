using System;
using Code.Components;
using Code.Gameplay.Extensions;
using Code.StatesSwitcher;
using Leopotam.Ecs;

namespace Code.Gameplay.Systems
{
    public sealed class InitLoseSystem:IEcsRunSystem
    {
        private readonly EcsFilter<PlayerTag, Destroy> _player = null;
        public void Run()
        {
            foreach (var pdx in _player)
            {
                ref var entity = ref _player.GetEntity(pdx);
                entity.DestroyWithGameObject();
                ChangeGameState.Change(GameStates.LoseState);
            }
        }
    }
}