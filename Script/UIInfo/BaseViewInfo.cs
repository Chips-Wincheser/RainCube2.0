using TMPro;
using UnityEngine;

public class BaseViewInfo<T> : MonoBehaviour where T : Component
{
    [SerializeField] protected BaseSpawner<T> BaseSpawner;

    [SerializeField] private TextMeshProUGUI _createdText;
    [SerializeField] private TextMeshProUGUI _spawnedText;
    [SerializeField] private TextMeshProUGUI _activeText;

    protected int CreatedObjectsCount = 0;
    protected int SpawnedObjectsCount = 0;

    private void OnEnable()
    {
        BaseSpawner.ObjectsCountCreated+=OnObjectCreated;
        BaseSpawner.ObjectsCountSpawned+=OnObjectSpawned;
        BaseSpawner.ActiveObjectsChanged+=OnObjectActive;
    }

    private void OnDisable()
    {
        BaseSpawner.ObjectsCountCreated-=OnObjectCreated;
        BaseSpawner.ObjectsCountSpawned-=OnObjectSpawned;
        BaseSpawner.ActiveObjectsChanged-=OnObjectActive;
    }

    private void OnObjectCreated()
    {
        CreatedObjectsCount++;
        _createdText.text = $"Создано: {CreatedObjectsCount}";
    }

    private void OnObjectSpawned()
    {
        SpawnedObjectsCount++;
        _spawnedText.text = $"Заспавнено: {SpawnedObjectsCount}";
    }

    private void OnObjectActive(int count)
    {
        _activeText.text = $"Активные: {count}";
    }
}
