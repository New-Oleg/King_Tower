using Leopotam.EcsLite;
using Unity.VisualScripting;
using UnityEngine;

public class GameInitSystem : IEcsInitSystem
{
    private DataBase _data;

    private EcsWorld _world = null;

    public GameInitSystem(DataBase dataBase)
    {
        _data = dataBase;
    }

    public void Init(EcsSystems systems)
    {
        _world = systems.GetWorld();

        var player = _world.NewEntity();
        //получаю пулы компонентов
        var poolInput = _world.GetPool<InputComponent>();
        var poolMove = _world.GetPool<MoveComponent>();
        var poolJump = _world.GetPool<JumpComponent>();
        var poolCollisionn = _world.GetPool<CollisionComponent>();
        var poolTransform = _world.GetPool<TransformComponent>();
        //получаю компоненты
        ref var playerInput = ref poolInput.Add(player);
        ref var playerMove = ref poolMove.Add(player);
        ref var playerJump = ref poolJump.Add(player);
        ref var playerCollision = ref poolCollisionn.Add(player);
        ref var playerTransform = ref poolTransform.Add(player);


        GameObject SpavnetPlayer = GameObject.Instantiate(_data.player.PlayePref,new Vector2(-3.04999995f, -2.5150001f), Quaternion.identity);
        playerTransform.transform = SpavnetPlayer.transform;
        playerMove.speed = _data.player.PlayerSpeed;
        playerJump.JumpForce = _data.player.PlayerJumpForce;
        playerJump.Rigidbody2D = SpavnetPlayer.GetComponent<Rigidbody2D>();

    }
}
