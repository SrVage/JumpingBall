using Code.Abstractions.Interfaces;
using Code.StatesSwitcher;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

namespace Code.UI
{
    public class RaitingScreen : MonoBehaviour
    {
        private IRaitingService _raitingService;
        [SerializeField] private TextMeshProUGUI _firstPlace;
        [SerializeField] private TextMeshProUGUI _secondPlace;
        [SerializeField] private TextMeshProUGUI _thirdPlace;
        [SerializeField] private Button _retry;

        public void Initial(IRaitingService raitingService)
        {
            _raitingService = raitingService;
            ShowRaiting();
            _retry.onClick.AddListener(Retry);
        }

        private void Retry()
        {
            _retry.onClick.RemoveAllListeners();
            ChangeGameState.Change(GameStates.StartState);
        }

        private void ShowRaiting()
        {
            foreach (var record in _raitingService.GetRaitingTable)
            {
                switch (record.Key)
                {
                    case "First":
                        _firstPlace.text = $"First place {record.Value} scores";
                        break;
                    case "Second":
                        _secondPlace.text = $"Second place {record.Value} scores";
                        break;
                    case "Third":
                        _thirdPlace.text = $"Third place {record.Value} scores";
                        break;
                }
            }
        }
    }
}