using Code.Abstractions;
using Code.Components;
using Leopotam.Ecs;

namespace Code.Gameplay.Systems
{
    public sealed class TimeSystem:IEcsRunSystem
    {
        private readonly EcsFilter<Time> _time = null;
        public void Run()
        {
            foreach (var tdx in _time)
            {
                ref var time = ref _time.Get1(tdx).Value;
                time -= UnityEngine.Time.deltaTime;
                if (time <= 0)
                {
                    ref var entity = ref _time.GetEntity(tdx);
                    if (entity.Has<GameObjectRef>())
                        entity.Del<Time>();
                    else
                        _time.GetEntity(tdx).Destroy();
                }
            }
        }
    }
}