using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    [Header("Lighting")]
    [SerializeField] private Light _dayLight;
    [SerializeField] private Light _nightLight;
    [SerializeField] private AnimationCurve _dayLightIntensityCurve;
    [SerializeField] private AnimationCurve _nightLightIntensityCurve;

    [SerializeField] private float _oneDayCycleSpeed;

    private void Update()
    {
        gameObject.transform.Rotate(Vector3.right, (360f / _oneDayCycleSpeed) * Time.deltaTime);

        _dayLight.intensity = _dayLightIntensityCurve.Evaluate(gameObject.transform.rotation.eulerAngles.x);

        //if (gameObject.transform.rotation.eulerAngles.x >= 0 && gameObject.transform.rotation.eulerAngles.x <= 180f)
        //{
        //    _dayLight.SetActive(true);
        //    _nightLight.SetActive(false);
        //}
        //else
        //{
        //    _dayLight.SetActive(false);
        //    _nightLight.SetActive(true);
        //}
    }
}
