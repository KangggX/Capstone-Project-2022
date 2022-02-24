using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class LightingManager : MonoBehaviour
{
    [Expandable] [SerializeField] private Light _directionalLight;
    [Expandable] [SerializeField] private LightingPreset _lightPreset;
    [SerializeField, Range(0, 24)] private float _timeOfDay;

    private void OnValidate()
    {
        if (_directionalLight != null)
        {
            return;
        }

        if (RenderSettings.sun != null)
        {
            _directionalLight = RenderSettings.sun;
        }
        else
        {
            Light[] lights = FindObjectsOfType<Light>();

            foreach (Light light in lights)
            {
                if (light.type == LightType.Directional)
                {
                    _directionalLight = light;
                    return;
                }
            }
        }
    }

    private void Update()
    {
        if (Application.isPlaying)
        {
            _timeOfDay += Time.deltaTime;
            _timeOfDay %= 24;
            Debug.Log(_timeOfDay);
            UpdateLighting(_timeOfDay / 24);
        }
        else
        {
            UpdateLighting(_timeOfDay / 24);
        }
    }

    private void UpdateLighting(float timePercent)
    {
        RenderSettings.ambientLight = _lightPreset.AmbientColor.Evaluate(timePercent);

        if (_directionalLight != null)
        {
            _directionalLight.color = _lightPreset.DirectionalColor.Evaluate(timePercent);
            _directionalLight.transform.localRotation = Quaternion.Euler(new Vector3((timePercent * 360f) - 90f, 0, 0));
        }
    }
}
