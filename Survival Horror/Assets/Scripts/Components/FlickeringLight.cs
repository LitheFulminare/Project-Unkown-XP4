using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickeringLight : MonoBehaviour
{
    private Light myLight;
    public float maxInterval = 1f;

    float targetIntensity;
    float lastIntensity;
    float interval;
    float timer;

    public float maxDisplacement = 0.25f;
    public float minIntensity = 0.5f;
    public float maxIntensity = 2f;
    Vector3 targetPosition;
    Vector3 lastPosition;
    Vector3 origin;

    private void Start()
    {
        myLight = GetComponent<Light>();
        if (myLight == null) Debug.LogWarning("Could not find Light component");
        origin = transform.position;
        lastPosition = origin;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer > interval)
        {
            lastIntensity = myLight.intensity;
            targetIntensity = Random.Range(minIntensity, maxIntensity);
            timer = 0;
            interval = Random.Range(0, maxInterval);

            targetPosition = origin + Random.insideUnitSphere * maxDisplacement;
            lastPosition = myLight.transform.position;
        }

        myLight.intensity = Mathf.Lerp(lastIntensity, targetIntensity, timer / interval);
        myLight.transform.position = Vector3.Lerp(lastPosition, targetPosition, timer / interval);
    }
}
