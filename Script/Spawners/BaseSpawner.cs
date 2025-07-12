using System;
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

    public event Action ObjectsCountCreated;
    public event Action ObjectsCountSpawned;

    public int ActiveObjectsCount => _pool?.CountActive ?? 0;


    private void Awake()
    {
        _pool= new ObjectPool<T>(
            createFunc: () => CreatePrefabInstance(),
            actionOnGet: (obj) => OnGetObject(obj),
            actionOnRelease: (obj) => TurningOffObject(obj),
            actionOnDestroy: (obj) => Destroy(obj.gameObject),
            collectionCheck: true,
            defaultCapacity: _poolCapacity,
            maxSize: _poolMaxSize
            );

        _waitForSeconds = new WaitForSeconds(_delay);
    }

    private T CreatePrefabInstance()
    {
        ObjectsCountCreated?.Invoke();
        return Instantiate(_prefab);
    }

    private void OnGetObject(T obj)
    {
        ObjectsCountSpawned?.Invoke();
        CreateObject(obj);
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
