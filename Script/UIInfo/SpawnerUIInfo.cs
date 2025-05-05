using TMPro;
using UnityEngine;

public class SpawnerUIInfo : MonoBehaviour
{
    [SerializeField] private BombSpawner _spawner;

    [SerializeField] private TextMeshProUGUI _createdText;
    [SerializeField] private TextMeshProUGUI _spawnedText;
    [SerializeField] private TextMeshProUGUI _activeText;

    private void Update()
    {
        if (_spawner == null) return;

        _createdText.text = $"Создано: {_spawner.CreatedObjectsCount}";
        _spawnedText.text = $"Заспавнено: {_spawner.SpawnedObjectsCount}";
        _activeText.text = $"Активные: {_spawner.ActiveObjectsCount}";
    }
}
