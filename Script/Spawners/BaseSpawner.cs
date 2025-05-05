using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public abstract class BaseSpawner<T> : MonoBehaviour where T : Component
{
    [SerializeField] protected T _prefab;
    [SerializeField] private float _delay = 1f;
    [SerializeField] private int _poolCapacity = 5;
    [SerializeField] private int _poolMaxSize = 5;

    protected WaitForSeconds _waitForSeconds;
    protected ObjectPool<T> _pool;

    public int CreatedObjectsCount {  get; private set; }
    public int SpawnedObjectsCount { get; private set; }
    public int ActiveObjectsCount => _pool?.CountActive ?? 0;


    private void Awake()
    {
        CreatedObjectsCount = 0;
        SpawnedObjectsCount = 0;

        _pool= new ObjectPool<T>(
            createFunc: () => { CreatedObjectsCount++; return Instantiate(_prefab); },
            actionOnGet: (obj) => { SpawnedObjectsCount++; CreateObject(obj); },
            actionOnRelease: (obj) => TurningOffObject(obj),
            actionOnDestroy: (obj) => Destroy(obj.gameObject),
            collectionCheck: true,
            defaultCapacity: _poolCapacity,
            maxSize: _poolMaxSize
            );

        _waitForSeconds = new WaitForSeconds(_delay);
    }

    protected abstract void CreateObject(T obj);

    protected abstract void TurningOffObject(T obj);

    public IEnumerator SpawnObject()
    {
        while (true)
        {
            if (_pool.CountActive<_poolMaxSize)
                _pool.Get();

            yield return _waitForSeconds;
        }
    }

    public void PutObjectInPool(T obj)
    {
        _pool.Release(obj);
    }
}
