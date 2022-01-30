using Code.Components;
using Code.Gameplay.Extensions;
using Code.StatesSwitcher;
using Leopotam.Ecs;

namespace Code.Gameplay.Systems
{
    public sealed class TriggerHandlerSystem:IEcsRunSystem
    {
        private readonly EcsFilter<Triggered> _trigger = null;
        private readonly EcsWorld _world = null;
        public void Run()
        {
            foreach (var tdx in _trigger)
            {
                ref var triggerEntity = ref _trigger.Get1(tdx).Value;
                if (triggerEntity.Has<PlayerTag>())
                {
                    ChangeGameState.Change(GameStates.LoseState);
                    triggerEntity.DestroyWithGameObject();
                }
                ref var entity = ref _trigger.GetEntity(tdx);
                entity.Destroy();
            }
        }
    }
}