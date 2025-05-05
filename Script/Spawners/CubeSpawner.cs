using UnityEngine;

public class CubeSpawner : BaseSpawner<Cube>
{
    [SerializeField] private BombSpawner _bombSpawner;

    private BoxCollider _platformCollider;
    private void Start()
    {
        _platformCollider = GetComponent<BoxCollider>();
        StartCoroutine(SpawnObject());
    }

    protected override void CreateObject(Cube obj)
    {
        obj.LifeTimeOver+=PutObjectInPool;

        int HalfDivider = 2;
        Vector3 platformSize = _platformCollider.size;

        float randomX = Random.Range(-platformSize.x / HalfDivider, platformSize.x / HalfDivider);
        float randomZ = Random.Range(-platformSize.z / HalfDivider, platformSize.z / HalfDivider);
        float positionY = -2f;
        Vector3 randomPosition = new Vector3(randomX, positionY, randomZ);

        obj.transform.position = transform.position+randomPosition;
        obj.gameObject.SetActive(true);
    }

    protected override void TurningOffObject(Cube obj)
    {
        obj.LifeTimeOver -= PutObjectInPool;
        obj.gameObject.SetActive(false);

        StartCoroutine(_bombSpawner.SpawnObject());
        _bombSpawner.SpawnPosition=obj.transform.position;
    }
}