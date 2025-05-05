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

        _createdText.text = $"�������: {_spawner.CreatedObjectsCount}";
        _spawnedText.text = $"����������: {_spawner.SpawnedObjectsCount}";
        _activeText.text = $"��������: {_spawner.ActiveObjectsCount}";
    }
}
