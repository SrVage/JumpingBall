using System;
using Code.Components;
using Code.UI.Components;
using Leopotam.Ecs;
using TMPro;

namespace Code.UI.Systems
{
    public class ShowScoresSystem:IEcsRunSystem
    {
        private readonly EcsFilter<GeneralStairsNumber> _scores = null;
        private readonly EcsFilter<TextMesh> _text = null;
        public void Run()
        {
            foreach (var tdx in _text)
            {
                ref var text = ref _text.Get1(tdx).Value;
                foreach (var sdx in _scores)
                {
                    ref var scores = ref _scores.Get1(sdx).MaxValue;
                    text.text = $"Your scores: {scores}";
                    ref var entity = ref _scores.GetEntity(sdx);
                    entity.Destroy();
                }
            }
        }
    }
}