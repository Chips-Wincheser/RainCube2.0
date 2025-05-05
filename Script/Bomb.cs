using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Explosion))]
public class Bomb : MonoBehaviour
{
    [SerializeField]private Renderer _bombRenderer;

    private Color _startColor;

    private Explosion _explosion;

    private int _liveTime;
    private int _minLiveTime=2;
    private int _maxLiveTime=5;

    public event Action<Bomb> LifeTimeOver;

    private void Awake()
    {
        _startColor = _bombRenderer.material.color;
        _explosion=gameObject.GetComponent<Explosion>();
    }

    private void OnEnable()
    {
        _bombRenderer.material.color = _startColor;

        _liveTime = UnityEngine.Random.Range(_minLiveTime, _maxLiveTime);

        StartCoroutine(FadeOut(_liveTime));
    }

    private IEnumerator FadeOut(float liveTime)
    {
        float elapsedTime = 0f;

        while (elapsedTime < liveTime)
        {
            elapsedTime+=Time.deltaTime;

            float alpha = Mathf.Lerp(_startColor.a, 0f, elapsedTime / liveTime);

            Color newColor = _startColor;
            newColor.a = alpha;

            _bombRenderer.material.color = newColor;

            yield return null;
        }

        _explosion.Explode();
        LifeTimeOver?.Invoke(this);
        gameObject.SetActive(false);
    }
}
