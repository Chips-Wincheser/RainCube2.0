using TMPro;
using UnityEngine;

public class BaseViewInfo<T> : MonoBehaviour where T : Component
{
    [SerializeField] protected BaseSpawner<T> _baseSpawner;

    [SerializeField] private TextMeshProUGUI _createdText;
    [SerializeField] private TextMeshProUGUI _spawnedText;
    [SerializeField] private TextMeshProUGUI _activeText;

    protected int _createdObjectsCount = 0;
    protected int _spawnedObjectsCount = 0;

    private void OnEnable()
    {
        _baseSpawner.ObjectsCountCreated+=OnObjectCreated;
        _baseSpawner.ObjectsCountSpawned+=OnObjectSpawned;
    }

    private void OnDisable()
    {
        _baseSpawner.ObjectsCountCreated-=OnObjectCreated;
        _baseSpawner.ObjectsCountSpawned-=OnObjectSpawned;
    }

    private void Update()
    {
        if (_baseSpawner == null) return;

        _createdText.text = $"Создано: {_createdObjectsCount}";
        _spawnedText.text = $"Заспавнено: {_spawnedObjectsCount}";
        _activeText.text = $"Активные: {_baseSpawner.ActiveObjectsCount}";
    }

    private void OnObjectCreated()
    {
        _createdObjectsCount++;
    }

    private void OnObjectSpawned()
    {
        _spawnedObjectsCount++;
    }
}
