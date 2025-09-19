using Leopotam.EcsLite;
using UnityEngine;

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

            if (collisionComponent.IsUnit == true)
            {
                Collider2D legCollider = Physics2D.OverlapPoint(transformComponent.transform.position + Vector3.down * 1, LayerMask.GetMask("Objects"));
                Collider2D bodyCollider = Physics2D.OverlapPoint(transformComponent.transform.position + Vector3.right * 0.7f, LayerMask.GetMask("Objects"));


                Vector3 pointPosition = transformComponent.transform.position + Vector3.right * 0.7f;
                Debug.DrawLine(pointPosition - Vector3.up * 0.1f, pointPosition + Vector3.up * 0.1f, Color.red);
                Debug.DrawLine(pointPosition - Vector3.left * 0.1f, pointPosition + Vector3.left * 0.1f, Color.red);



                LegCheckCollision(legCollider, ref collisionComponent);
                BodyCheckCollision(bodyCollider, ref collisionComponent);
            }

        }
    }


    private void LegCheckCollision(Collider2D? Collider, ref CollisionComponent collisionComponent)
    {
        if (Collider != null)
        {
            switch (Collider.tag) //свитч тк будут враги, шипы и т.п
            {
                case "Ground":
                    collisionComponent.IsGrounded = true;
                    break;
                case "Enemy":
                    break;
            }
        }
        else // значит не косаеться ничего, все сделать false
        {
            collisionComponent.IsGrounded = false;
        }
    }
    
    private void BodyCheckCollision(Collider2D? Collider, ref CollisionComponent collisionComponent)
    {
        if (Collider != null)
        {
            switch (Collider.tag) //свитч тк будут враги, шипы и т.п
            {
                case "PlatformEdge":
                    Debug.Log("i can do this");
                    collisionComponent.IsCanUp = true;
                    break;
                case "Enemy":
                    break;
            }
        }
        else // значит не косаеться ничего, все сделать false
        {
            collisionComponent.IsCanUp = false;
        }
    }

}
