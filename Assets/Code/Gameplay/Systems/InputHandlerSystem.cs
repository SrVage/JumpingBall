using System;
using Code.Components;
using Leopotam.Ecs;
using Unity.Android.Types;

namespace Code.Gameplay.Systems
{
    public sealed class InputHandlerSystem:IEcsRunSystem
    {
        private const int DeadZone = 10;
        private readonly EcsFilter<InputVector, Done> _input = null;
        private readonly EcsWorld _world = null;
        public void Run()
        {
            foreach (var idx in _input)
            {
                ref var startPoint = ref _input.Get1(idx).StartValue;
                ref var endPoint = ref _input.Get1(idx).EndValue;
                float x = startPoint.x - endPoint.x;
                if (Math.Abs(x)<DeadZone)
                {
                    _world.NewEntity().Get<Jump>();
                }
                else
                {
                    if (x > 0)
                    {
                        _world.NewEntity().Get<SideJump>().Value = Side.Right;
                    }
                    else
                    {
                        _world.NewEntity().Get<SideJump>().Value = Side.Left;
                    }
                }
                _input.GetEntity(idx).Destroy();
            }
        }
    }
}