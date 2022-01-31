using System;
using Code.Abstractions.Interfaces;
using Code.Configs;
using Code.MonoBehavioursComponent;
using Code.StatesSwitcher;
using Code.StatesSwitcher.States;
using Code.UI.Components;
using Leopotam.Ecs;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Code.UI.Systems
{
    public class ChangeScreenSystem:IEcsRunSystem, IEcsInitSystem
    {
        private readonly UIScreen _uiScreen;
        private readonly EcsFilter<UIScreens> _uiScreens;
        private readonly EcsFilter<ChangeState> _state;
        private readonly EcsWorld _world;
        private readonly IRaitingService _raitingService;
        private Transform _canvas;

        public ChangeScreenSystem(IRaitingService raitingService)
        {
            _raitingService = raitingService;
        }

        public void Run()
        {
            if (_state.IsEmpty()) return;
            foreach (var state in _state)
            {
                ref var st = ref _state.Get1(state).States;
                switch (st)
                {
                    case GameStates.StartState:
                        StartScreen(_uiScreen.TTSScreen, true);
                        break;
                    case GameStates.PlayState:
                        StartScreen(_uiScreen.GameplayScreen, true);
                        break;
                    case GameStates.WinState:
                        break;
                    case GameStates.LoseState:
                        StartScreen(_uiScreen.LooseScreen, true);
                        break;
                    case GameStates.RaitingStates:
                        StartScreen(_uiScreen.RaitingScreen, true);
                        break;
                    case GameStates.NextLevelStates:
                        break;
                    case GameStates.RestartStates:
                        break;

                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        private void StartScreen(GameObject screenPrefab, bool destroyCurrent)
        {
            var screen = GameObject.Instantiate(screenPrefab, _canvas);
            if (screen.TryGetComponent<UIEntity>(out var UI))
                UI.Initial(_world);
            if (screen.TryGetComponent<RaitingScreen>(out var raiting))
                raiting.Initial(_raitingService);
            foreach (var ui in _uiScreens)
            {
                ref var scree = ref _uiScreens.Get1(ui).Screens;
                scree = Screens.Win;
                ref var go = ref _uiScreens.Get1(ui).Screen;
                if (go != null&&destroyCurrent)
                    GameObject.Destroy(go);
                go = screen;
            }
        }

        public void Init()
        {
            _world.NewEntity().Get<UIScreens>();
            _canvas = Object.FindObjectOfType<Canvas>().transform.GetChild(0).transform;
        }
    }
}