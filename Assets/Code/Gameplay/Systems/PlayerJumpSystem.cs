using System;
using Code.Abstractions;
using Code.Components;
using DG.Tweening;
using Leopotam.Ecs;
using UnityEngine;

namespace Code.Gameplay.Systems
{
    public sealed class PlayerJumpSystem:IEcsRunSystem
    {
        private readonly EcsFilter<Jump> _jumpUp = null;
        private readonly EcsFilter<SideJump> _jumpSide = null;
        private readonly EcsFilter<GameObjectRef, Physic, PlayerTag>.Exclude<IsMoving> _player = null;
        private readonly EcsWorld _world = null;

        public void Run()
        {
            if (!_jumpUp.IsEmpty())
            {
                Jump(Side.Up);
            }

            foreach (var jdx in _jumpSide)
            {
                ref var side = ref _jumpSide.Get1(jdx).Value;
                Jump(side);
            }
        }

        private void Jump(Side side)
        {
            foreach (var pdx in _player)
            {
                ref var transform = ref _player.Get1(pdx).Transform;
                DisablePhysic(pdx);
                var entity = _player.GetEntity(pdx);
                entity.Get<IsMoving>();
                transform.DOJump(transform.position + CalculateDirectionVector(side), 1f, 1, 0.5f).OnComplete(()
                    =>
                {
                    if (side == Side.Up)
                        _world.NewEntity().Get<ChangeStair>();
                    entity.Del<IsMoving>();
                    entity.Get<Physic>().Value.isKinematic = false;
                });
            }
        }

        private void DisablePhysic(int pdx)
        {
            ref var physic = ref _player.Get2(pdx).Value;
            physic.isKinematic = true;
        }

        private static Vector3 CalculateDirectionVector(Side side)
        {
            switch (side)
            {
                case Side.Left:
                    return Vector3.left;
                case Side.Right:
                    return Vector3.right;
                case Side.Up:
                    return new Vector3(0, 1, -1);
                default:
                    return Vector3.zero;
            }
        }
    }
}