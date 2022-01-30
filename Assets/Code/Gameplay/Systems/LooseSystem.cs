using Code.Abstractions;
using Code.Components;
using Code.Gameplay.Extensions;
using Code.StatesSwitcher.States;
using Leopotam.Ecs;

namespace Code.Gameplay.Systems
{
    public class LooseSystem:IEcsRunSystem
    {
        private readonly EcsFilter<GameObjectRef> _gameObject = null;
        private readonly EcsFilter<GeneralStairsNumber> _generalStairs = null;
        private readonly EcsFilter<LoseState> _signal = null;
        public void Run()
        {
            if (_signal.IsEmpty())
                return;
            foreach (var gdx in _gameObject)
            {
                ref var entity = ref _gameObject.GetEntity(gdx);
                entity.DestroyWithGameObject();
            }
        }
    }
}