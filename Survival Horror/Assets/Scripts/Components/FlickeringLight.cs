using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class FlickeringLight : MonoBehaviour
{
    [Header("Light")]
    [SerializeField] private float _maxInterval = 1f;
    [SerializeField] private float _minIntensity = 0.5f;
    [SerializeField] private float _maxIntensity = 2f;

    [Header("Lens Flare")]
    [SerializeField] private float _minLensFlareIntensity = 0.2f;
    [SerializeField] private float _maxLensFlareIntensity = 1f;
    [SerializeField] private float _minLensFlareScale = 0.8f;
    [SerializeField] private float _maxLensFlareScale = 1.4f;

    private Light _lightComponent;

    private float _targetIntensity;
    private float _lastIntensity;
    private float _interval;
    private float _timer;

    private LensFlareComponentSRP _lensFlare;  

    private void Start()
    {
        if (!TryGetComponent<Light>(out _lightComponent))
        {
            Debug.LogWarning("failed to find the light component");
        }

        TryGetComponent<LensFlareComponentSRP>(out _lensFlare);
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

        // normalize to use as parameter on Lerp
        float normalizedLightIntensity = (_lightComponent.intensity - _minIntensity) / (_maxIntensity - _minIntensity);

        _lensFlare.intensity = Mathf.Lerp(_minLensFlareIntensity, _maxLensFlareIntensity, normalizedLightIntensity);
        _lensFlare.scale = Mathf.Lerp(_minLensFlareScale, _maxLensFlareScale, normalizedLightIntensity);
    }

    // converts a number between two ranges to another number in a different range
    // for exemple, converts a number from 2 to 8 to a number from 0 to 1
    float ConvertRange(float value, float minOriginal, float maxOriginal, float minTarget, float maxTarget)
    {
        return (value - minOriginal) / (maxOriginal - minOriginal) * (maxTarget - minTarget) + minTarget;
    }
}
