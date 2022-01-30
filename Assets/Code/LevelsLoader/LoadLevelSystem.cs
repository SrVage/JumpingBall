using Code.Abstractions;
using Code.Abstractions.Interfaces;
using Code.Components;
using Code.Configs;
using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Code.LevelsLoader
{
    public class LoadLevelSystem:IEcsRunSystem, IEcsInitSystem
    {
        private readonly LevelList _levels;
        private readonly EcsFilter<LoadLevelSignal> _signal;
        private readonly EcsWorld _world;
        private IChangeLevelService _changeLevelService;
        private AsyncOperationHandle<GameObject> _level;

        public void Init()
        {
            _changeLevelService = new ChangeLevelService();
        }

        public async void Run()
        {
            if (_signal.IsEmpty()) return;
            if (_level.IsValid())
            {
                Addressables.ReleaseInstance(_level);
                _changeLevelService.ChangeLevel();
            }
            _level = Addressables.InstantiateAsync(_levels.Levels[_changeLevelService.CurrentLevel%_levels.Levels.Count]);
            await _level.Task;
            InitializeLevelObject(_level.Result);
            var entity = _world.NewEntity();
                entity.Get<GeneralStairsNumber>();
                entity.Get<Init>();
        }
        
        private void InitializeLevelObject(GameObject level)
        {
            var levelObjects = level.GetComponentsInChildren<MonoBehavioursEntity>();
            foreach (var levelObject in levelObjects)
            {
                levelObject.Initial(_world.NewEntity(), _world);
            }
        }
    }
}