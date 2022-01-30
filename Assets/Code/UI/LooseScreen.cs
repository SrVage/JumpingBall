using Code.MonoBehavioursComponent;
using Code.StatesSwitcher;
using Leopotam.Ecs;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using TextMesh = Code.UI.Components.TextMesh;

namespace Code.UI
{
    public class LooseScreen:UIEntity
    {
        [SerializeField] private Button _nextLevelButton;
        [SerializeField] private TextMeshProUGUI _scores;
        private EcsEntity _entity;
        public override void Initial(EcsWorld world)
        {
            base.Initial(world);
            _entity = _world.NewEntity();
                _entity.Get<TextMesh>().Value = _scores;
            _nextLevelButton.onClick.AddListener(NextLevel);
        }

        private void NextLevel()
        {
            _nextLevelButton.onClick.RemoveAllListeners();
            _entity.Destroy();
            ChangeGameState.Change(GameStates.StartState);
        }
    }
}