using Leopotam.EcsLite;
using UnityEngine;

public class JumpSystem : IEcsInitSystem, IEcsRunSystem
{
    private EcsFilter _jumpComponentFilter;
    private EcsPool<JumpComponent> _jumpPool;
    private EcsPool<InputComponent> _inputPool;
    private EcsPool<CollisionComponent> _collisionPool;

    public void Init(EcsSystems systems)
    {
        var world = systems.GetWorld();
        _jumpComponentFilter = world.Filter<JumpComponent>().Inc<InputComponent>().Inc<CollisionComponent>().End();
        _jumpPool = world.GetPool<JumpComponent>();
        _inputPool = world.GetPool<InputComponent>();
        _collisionPool = world.GetPool<CollisionComponent>();
    }

    public void Run(EcsSystems systems)
    {
        foreach (int entity in _jumpComponentFilter)
        {
            ref var jumpComponent = ref _jumpPool.Get(entity);
            ref var inputComponent = ref _inputPool.Get(entity);
            ref var collisionComponent = ref _collisionPool.Get(entity);

            Debug.Log((inputComponent.jumpPressed == true) + " : " +( collisionComponent.IsGroundet == true));
            if (inputComponent.jumpPressed == true && collisionComponent.IsGroundet == true)
            {
                jumpComponent.Rigidbody2D.AddForce(new Vector2(0, jumpComponent.JumpForce * Time.deltaTime), ForceMode2D.Impulse);
            }
        }   
    }
}
