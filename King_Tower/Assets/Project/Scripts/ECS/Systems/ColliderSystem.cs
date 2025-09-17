using Leopotam.EcsLite;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class ColliderSystem : IEcsInitSystem, IEcsRunSystem
{
    private EcsFilter _collisionComponentFilter;
    private EcsPool<CollisionComponent> _collisionPool;
    private EcsPool<TransformComponent> _transformPool;
    public void Init(EcsSystems systems)
    {
        var world = systems.GetWorld();
        _collisionComponentFilter = world.Filter<CollisionComponent>().Inc<TransformComponent>().End();
        _collisionPool = world.GetPool<CollisionComponent>();
        _transformPool = world.GetPool<TransformComponent>();
    }
    public void Run(EcsSystems systems)
    {
        foreach (int entity in _collisionComponentFilter)
        {
            ref var collisionComponent = ref _collisionPool.Get(entity);
            ref var transformComponent = ref _transformPool.Get(entity);

            var tachingCollider = Physics2D.OverlapPoint(transformComponent.transform.position + Vector3.down * 1);


            Debug.Log(tachingCollider);

            if (tachingCollider != null){
                switch (tachingCollider.tag) //свитч тк будут враги, шипы и т.п
                {
                    case "Ground":
                        collisionComponent.IsGrounded = true;
                        break;
                }
            }
            else
            {
                collisionComponent.IsGrounded = false;
            }
        }
    }
}
