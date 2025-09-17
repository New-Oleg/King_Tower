using Leopotam.EcsLite;
using UnityEngine;

public class JumpSystem : IEcsInitSystem, IEcsRunSystem
{
    private EcsFilter _filter;
    private EcsPool<JumpComponent> _jumpPool;
    private EcsPool<InputComponent> _inputPool;
    private EcsPool<CollisionComponent> _collisionPool;

    public void Init(EcsSystems systems)
    {
        var world = systems.GetWorld();
        _filter = world.Filter<JumpComponent>().Inc<InputComponent>().Inc<CollisionComponent>().End();

        _jumpPool = world.GetPool<JumpComponent>();
        _inputPool = world.GetPool<InputComponent>();
        _collisionPool = world.GetPool<CollisionComponent>();
    }

    public void Run(EcsSystems systems)
    {
        foreach (int entity in _filter)
        {
            ref var jump = ref _jumpPool.Get(entity);
            ref var input = ref _inputPool.Get(entity);
            ref var collision = ref _collisionPool.Get(entity);

            Debug.Log(collision.IsGrounded);
            if (input.jumpPressed && collision.IsGrounded)
            {
                jump.Rigidbody2D.AddForce(Vector2.up * jump.JumpForce, ForceMode2D.Impulse);
            }
        }
    }
}