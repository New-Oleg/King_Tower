using Leopotam.EcsLite;
using UnityEngine;

public class Starter : MonoBehaviour
{
    [SerializeField]
    private DataBase data;

    private EcsWorld _world;
    private EcsSystems _systems;

    void Start()
    {
        _world = new EcsWorld();
        _systems = new EcsSystems(_world);

        _systems.Add(new GameInitSystem(data));
        _systems.Add(new PlayerInputSystem());
        _systems.Add(new MoveSystem());
        _systems.Add(new JumpSystem());
        _systems.Add(new ColliderSystem());

        _systems.Init();
    }

    // Update is called once per frame
    void Update()
    {
        _systems.Run();
    }

    private void OnDestroy()
    {
        _world.Destroy();
        _systems.Destroy();
    }
}
