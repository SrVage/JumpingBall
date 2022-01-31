using Code.Components;
using Leopotam.Ecs;

namespace Code.Gameplay.Systems
{
    public sealed class InitStairSystem:IEcsRunSystem
    {
        private readonly EcsFilter<GeneralStairsNumber, Init> _stairsNumber = null;
        private readonly EcsFilter<Stair> _stairs = null;
        public void Run()
        {
            foreach (var ndx in _stairsNumber)
            {
                ref var minNumber = ref _stairsNumber.Get1(ndx).MinValue;
                ref var maxNumber = ref _stairsNumber.Get1(ndx).MaxValue;
                foreach (var sdx in _stairs)
                {
                    ref var stairNumber = ref _stairs.Get1(sdx).Number;
                    if (stairNumber > maxNumber)
                        maxNumber = stairNumber;
                    if (stairNumber < minNumber)
                        minNumber = stairNumber;
                }

                ref var offset = ref _stairsNumber.Get1(ndx).OffsetValue;
                offset = maxNumber;
                ref var entity = ref _stairsNumber.GetEntity(ndx);
                entity.Del<Init>();
            }
        }
    }
}