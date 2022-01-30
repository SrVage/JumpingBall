using Code.Abstractions;
using Code.Components;
using Leopotam.Ecs;

namespace Code.Gameplay.Systems
{
    public sealed class BindCameraSystem:IEcsRunSystem
    {
        private readonly EcsFilter<GameObjectRef, PlayerTag, BindCameraTag> _player=null;
        private readonly EcsFilter<Camera> _camera=null;
        public void Run()
        {
            if (_player.IsEmpty())
                return;
            foreach (var pdx in _player)
            {
                ref var playerTransform = ref _player.Get1(pdx).Transform;
                ref var playerEntity = ref _player.GetEntity(pdx);
                foreach (var cdx in _camera)
                {
                    ref var camera = ref _camera.Get1(cdx).Value;
                    camera.Follow = playerTransform;
                    //camera.LookAt = playerTransform;
                }
                playerEntity.Del<BindCameraTag>();
            }
        }
    }
}