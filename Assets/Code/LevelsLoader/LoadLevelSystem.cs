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
        private ChangeLevelService _changeLevelService;
        private AsyncOperationHandle<GameObject> _level;

        public void Init()
        {
            _changeLevelService = new ChangeLevelService();
        }

        public void Run()
        {
            if (_signal.IsEmpty()) return;
            if (_level.IsValid())
            {
                Addressables.ReleaseInstance(_level);
                _changeLevelService.ChangeLevel();
            }
            _level = Addressables.InstantiateAsync(_levels.Levels[_changeLevelService.CurrentLevel%_levels.Levels.Count]);
        }
    }
}