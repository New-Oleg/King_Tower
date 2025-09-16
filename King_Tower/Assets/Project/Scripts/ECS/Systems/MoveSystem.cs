using Leopotam.EcsLite;
using UnityEngine;

public class MoveSystem : IEcsInitSystem, IEcsRunSystem
{
    private EcsFilter _MoveComponentFilter;
    private EcsPool<MoveComponent> _movePool;
    private EcsPool<InputComponent> _inputPool;
    private EcsPool<TransformComponent> _transformPool;
    public void Init(EcsSystems systems)
    {
        var world = systems.GetWorld();
        _MoveComponentFilter = world.Filter<MoveComponent>().Inc<InputComponent>().Inc<TransformComponent>().End();
        _movePool = world.GetPool<MoveComponent>();
        _inputPool = world.GetPool<InputComponent>();
        _transformPool = world.GetPool<TransformComponent>();
    }

    public void Run(EcsSystems systems)
    {
        foreach (int entity in _MoveComponentFilter)
        {
            ref var movableComponent = ref _movePool.Get(entity);
            ref var inputComponent = ref _inputPool.Get(entity);
            ref var transformComponent = ref _transformPool.Get(entity);

            transformComponent.transform.position += (Vector3)(inputComponent.direction * movableComponent.speed * Time.deltaTime);
        }
    }
}
