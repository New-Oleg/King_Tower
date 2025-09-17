using Leopotam.EcsLite;
using UnityEngine;

public class PlayerInputSystem : IEcsInitSystem, IEcsRunSystem
{
    private EcsFilter _inputComponentFilter;
    private EcsPool<InputComponent> _pool;

    public void Init(EcsSystems systems)
    {
        var world = systems.GetWorld();
        _inputComponentFilter = world.Filter<InputComponent>().End();
        _pool = world.GetPool<InputComponent>();
    }

    public void Run(EcsSystems systems)
    {
        float x = Input.GetAxis("Horizontal"); 

        foreach (int inputComponent in _inputComponentFilter) 
        {
            ref var input = ref _pool.Get(inputComponent);

            input.direction = new Vector2(x, 0f);

            input.jumpPressed = Input.GetKeyDown(input.inputButtons.JumpButton);

            input.DashPressed = Input.GetKeyDown(input.inputButtons.DashButton);
        }
    }
}
