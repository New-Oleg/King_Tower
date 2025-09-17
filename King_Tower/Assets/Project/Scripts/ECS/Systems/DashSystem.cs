using Leopotam.EcsLite;
using UnityEditor.Timeline.Actions;
using UnityEngine;

public class DashSystem : IEcsInitSystem, IEcsRunSystem
{
    private EcsFilter _dashComponentFilter;
    private EcsPool<DashComponnent> _dashPool;
    private EcsPool<InputComponent> _inputPool;
    private EcsPool<RigitbodyComponent> _rigitbodyPool;
    public void Init(EcsSystems systems)
    {
        var world = systems.GetWorld();
        _dashComponentFilter = world.Filter<DashComponnent>().Inc<InputComponent>().Inc<RigitbodyComponent>().End();
        _dashPool = world.GetPool<DashComponnent>();
        _inputPool = world.GetPool<InputComponent>();
        _rigitbodyPool = world.GetPool<RigitbodyComponent>();
    }

    public void Run(EcsSystems systems)
    {
        foreach (int entity in _dashComponentFilter)
        {
            ref var dash = ref _dashPool.Get(entity);
            ref var input = ref _inputPool.Get(entity);
            ref var rigdbody = ref _rigitbodyPool.Get(entity);

            dash.DashCurentTime -= Time.deltaTime;

            if (input.DashPressed && dash.DashCurentTime <= 0)
            {
                dash.DashCurentTime = dash.DashUptime;
                rigdbody.rb.AddForce((input.direction.x >= 0 ? Vector2.right : Vector2.left) * dash.DashForce, ForceMode2D.Impulse);
            }
        }
    }
}
