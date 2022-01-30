using Code.Components;
using Code.StatesSwitcher.States;
using Leopotam.Ecs;
using UnityEngine;

namespace Code.Gameplay.Systems
{
    public sealed class InputSystem:IEcsRunSystem
    {
        private readonly EcsFilter<PlayState> _play = null;
        private readonly EcsWorld _world = null;
        private EcsEntity _entity;
        public void Run()
        {
            if (_play.IsEmpty())
                return;
            if (Input.GetMouseButtonDown(0))
            {
                _entity = _world.NewEntity();
                _entity.Get<InputVector>().StartValue = Input.mousePosition;
            }

            if (Input.GetMouseButtonUp(0))
            {
                if (_entity.IsNull()||!_entity.IsAlive())
                    return;
                _entity.Get<InputVector>().EndValue = Input.mousePosition;
                _entity.Get<Done>();
            }
        }
    }
}