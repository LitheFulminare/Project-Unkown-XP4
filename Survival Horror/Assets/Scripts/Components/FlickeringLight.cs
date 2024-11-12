using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickeringLight : MonoBehaviour
{
    private Light _lightComponent;

    [SerializeField] private float _maxInterval = 1f;
    [SerializeField] private float _minIntensity = 0.5f;
    [SerializeField] private float _maxIntensity = 2f;

    private float _targetIntensity;
    private float _lastIntensity;
    private float _interval;
    private float _timer;

    private void Start()
    {
        if (!TryGetComponent<Light>(out _lightComponent))
        {
            Debug.LogWarning("failed to find the light component");
        }
    }

    void Update()
    {
        if (PlayerVars.playerBlocked) return;

        _timer += Time.deltaTime;

        if (_timer > _interval)
        {
            _lastIntensity = _lightComponent.intensity;
            _targetIntensity = Random.Range(_minIntensity, _maxIntensity);
            _timer = 0;
            _interval = Random.Range(0, _maxInterval);
        }

        _lightComponent.intensity = Mathf.Lerp(_lastIntensity, _targetIntensity, _timer / _interval);
    }
}
