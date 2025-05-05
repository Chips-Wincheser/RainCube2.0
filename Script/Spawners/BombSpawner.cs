using UnityEngine;

public class BombSpawner : BaseSpawner<Bomb>
{
    private Vector3 _spawnPosition;

    public Vector3 SpawnPosition;

    protected override void CreateObject(Bomb obj)
    {
        obj.LifeTimeOver+=PutObjectInPool;

        _spawnPosition =SpawnPosition;
        obj.transform.position = _spawnPosition;
        obj.gameObject.SetActive(true);
    }

    protected override void TurningOffObject(Bomb obj)
    {
        obj.LifeTimeOver -= PutObjectInPool;

        obj.gameObject.SetActive(false);
    }
}
