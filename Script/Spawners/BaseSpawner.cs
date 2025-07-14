using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public abstract class BaseSpawner<T> : MonoBehaviour where T : Component
{
    [SerializeField] protected T Prefab;
    [SerializeField] private float _delay = 1f;
    [SerializeField] private int _poolCapacity = 5;
    [SerializeField] private int _poolMaxSize = 5;

    protected WaitForSeconds WaitForSeconds;
    protected ObjectPool<T> Pool;

    public int ActiveObjectsCount => Pool?.CountActive ?? 0;

    public event Action ObjectsCountCreated;
    public event Action ObjectsCountSpawned;
    public event Action<int> ActiveObjectsChanged;

    private void Awake()
    {
        Pool= new ObjectPool<T>(
            createFunc: () => CreatePrefabInstance(),
            actionOnGet: (obj) => OnGetObject(obj),
            actionOnRelease: (obj) => TurningOffObject(obj),
            actionOnDestroy: (obj) => Destroy(obj.gameObject),
            collectionCheck: true,
            defaultCapacity: _poolCapacity,
            maxSize: _poolMaxSize
            );

        WaitForSeconds = new WaitForSeconds(_delay);
    }

    private T CreatePrefabInstance()
    {
        ObjectsCountCreated?.Invoke();
        return Instantiate(Prefab);
    }

    private void OnGetObject(T obj)
    {
        ObjectsCountSpawned?.Invoke();
        CreateObject(obj);
        ActiveObjectsChanged?.Invoke(ActiveObjectsCount);
    }

    protected abstract void CreateObject(T obj);

    protected abstract void TurningOffObject(T obj);

    public IEnumerator SpawnObject()
    {
        while (true)
        {
            if (Pool.CountActive<_poolMaxSize)
                Pool.Get();

            yield return WaitForSeconds;
        }
    }

    public void PutObjectInPool(T obj)
    {
        Pool.Release(obj);
        ActiveObjectsChanged?.Invoke(ActiveObjectsCount);
    }
}
