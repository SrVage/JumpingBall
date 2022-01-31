using System;
using Code.Abstractions.Interfaces;
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
        private readonly IRaitingService _raitingService;

        public ShowScoresSystem(IRaitingService raitingService)
        {
            _raitingService = raitingService;
        }

        public void Run()
        {
            foreach (var tdx in _text)
            {
                ref var text = ref _text.Get1(tdx).Value;
                foreach (var sdx in _scores)
                {
                    ref var scores = ref _scores.Get1(sdx).MaxValue;
                    ref var offset = ref _scores.Get1(sdx).OffsetValue;
                    text.text = $"Your scores: {scores-offset}";
                    _raitingService.AddRecord(scores-offset);
                    ref var entity = ref _scores.GetEntity(sdx);
                    entity.Destroy();
                }
            }
        }
    }
}