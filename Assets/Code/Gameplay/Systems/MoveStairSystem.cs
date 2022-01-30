using Code.Abstractions;
using Code.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Code.Gameplay.Systems
{
    public sealed class MoveStairSystem:IEcsRunSystem
    {
        private readonly EcsFilter<GameObjectRef, Stair> _stairs = null;
        private readonly EcsFilter<GeneralStairsNumber> _number = null;
        private readonly EcsFilter<ChangeStair> _signal = null;
        private EcsEntity _entity;

        public void Run()
        {
            if (_signal.IsEmpty())
                return;
            foreach (var ndx in _number)
            {
                ref var minNumber = ref _number.Get1(ndx).MinValue;
                ref var maxNumber = ref _number.Get1(ndx).MaxValue;
                Vector3 position = Vector3.zero;
                foreach (var sdx in _stairs)
                {
                    ref var number = ref _stairs.Get2(sdx).Number;
                    position = GetHighStairTransform(number, maxNumber, position, sdx);
                    FindLowStair(number, minNumber, sdx);
                }

                ref var transform = ref _entity.Get<GameObjectRef>().Transform;
                transform.position = position;
                ref var num = ref _entity.Get<Stair>().Number;
                num = ++maxNumber;
                minNumber++;
            }
        }

        private void FindLowStair(int number, int minNumber, int sdx)
        {
            if (number == minNumber)
                _entity = _stairs.GetEntity(sdx);
        }

        private Vector3 GetHighStairTransform(int number, int maxNumber, Vector3 position, int sdx)
        {
            if (number == maxNumber)
            {
                position = _stairs.Get1(sdx).Transform.position + new Vector3(0, 1, -1);
            }

            return position;
        }
    }
}