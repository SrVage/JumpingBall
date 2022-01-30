using Code.Components;
using Code.Configs;
using Code.Gameplay.Systems;
using Code.LevelsLoader;
using Code.StatesSwitcher;
using Code.StatesSwitcher.Events;
using Code.StatesSwitcher.States;
using Code.UI.Systems;
using Leopotam.Ecs;
using UnityEngine;

namespace Code.Gameplay {
    sealed class EcsStartup : MonoBehaviour {
        EcsWorld _world;
        EcsSystems _systems;

        [SerializeField] private LevelList _levels;
        [SerializeField] private UIScreen _uiScreen;
        [SerializeField] private SceneObjects _sceneObjects;
        void Start () {
            // void can be switched to IEnumerator for support coroutines.
            
            _world = new EcsWorld ();
            _systems = new EcsSystems (_world);
            ChangeGameState.World = _world;
#if UNITY_EDITOR
            Leopotam.Ecs.UnityIntegration.EcsWorldObserver.Create (_world);
            Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Create (_systems);
#endif
            _systems
                // register your systems here, for example:
                .Add(new GameInitial())
                .Add (new ChangeStateSystem ())
                .Add(new TriggerHandlerSystem())
                .Add(new StateMachine())
                .Add(new LoadLevelSystem())
                .Add(new ChangeScreenSystem())
                .Add(new BindCameraSystem())
                .Add(new InitStairSystem())
                .Add(new InputSystem())
                .Add(new InputHandlerSystem())
                .Add(new PlayerJumpSystem())
                .Add(new MoveStairSystem())
                .Add(new CreateEnemySystem())
                .Add(new TimeSystem())
                .Add(new EnemyJumpSystem())
                .Add(new DestroyEnemySystem())
                .Add(new LooseSystem())
                .Add(new ShowScoresSystem())

                // .Add (new TestSystem2 ())
                
                // register one-frame components (order is important), for example:
                .OneFrame<ChangeState> ()
                .OneFrame<LoadLevelSignal> ()
                .OneFrame<TapToStart>()
                .OneFrame<ChangeStair>()
                .OneFrame<Jump>()
                .OneFrame<SideJump>()
                
                // inject service instances here (order doesn't important), for example:
                .Inject (_levels)
                .Inject(_uiScreen)
                .Inject(_sceneObjects)
                // .Inject (new NavMeshSupport ())
                .Init ();
        }
        
        void Update () {
            _systems?.Run ();
        }

        void OnDestroy () {
            if (_systems != null) {
                _systems.Destroy ();
                _systems = null;
                _world.Destroy ();
                _world = null;
            }
        }
    }
}