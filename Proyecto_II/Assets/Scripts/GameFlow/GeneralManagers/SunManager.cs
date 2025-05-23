using System;
using TMPro;
using UnityEngine;

/*
 * NOMBRE CLASE: SunManager
 * AUTOR: Sara Yue Madruga Mart�n, Jone Sainz Egea
 * FECHA: 22/04/2025
 * DESCRIPCI�N: Gestiona la rotaci�n de la luz direccional (sol) para simular sistema de d�a y noche.
 * VERSI�N: 1.0.
 *              1.1. Funciona con el skybox
 */

public class SunManager : MonoBehaviour
{
    [SerializeField] private Light sun;
    //[SerializeField] private Light moon;
    [SerializeField] private float sunriseHour;
    [SerializeField] private float sunsetHour;
    [SerializeField] private float timeMultiplier;
    [SerializeField] private float startHour;
    //[SerializeField] private TextMeshProUGUI textTime;
    private DateTime currentTime;
    private TimeSpan sunriseTime;
    private TimeSpan sunsetTime;

    [SerializeField] private Color dayAmbientLight;
    [SerializeField] private Color nightAmbientLight;
    [SerializeField] private AnimationCurve lightCurve;
    [SerializeField] private float maxSunIntensity;
    [SerializeField] private float maxMoonIntensity;
    private float lightIntensityMultiplier = 1f;

    // Skybox
    [SerializeField] private Material blendedSkybox;
    [SerializeField] private float skyboxBlendSpeed = 0.5f;
    private float targetBlend = 0f;

    // Start is called before the first frame update
    void Start()
    {
        // Debug.Log(sun.transform.rotation.eulerAngles);
        RenderSettings.skybox = blendedSkybox;

        currentTime = DateTime.Now.Date + TimeSpan.FromHours(startHour);
        sunriseTime = TimeSpan.FromHours(sunriseHour);
        sunsetTime = TimeSpan.FromHours(sunsetHour);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTimeDay();
        RotateSun();
        UpdateLightSettings();
    }

    private void UpdateTimeDay()
    {
        currentTime = currentTime.AddSeconds(Time.deltaTime * timeMultiplier);

        //textTime.text = currentTime.ToString("HH:mm");
    }

    private void RotateSun()
    {
        float sunLightRot;

        if (currentTime.TimeOfDay > sunriseTime && currentTime.TimeOfDay < sunsetTime)
        {
            TimeSpan sunriseToSunsetDuration = CalculateTimeDifference(sunriseTime, sunsetTime);
            TimeSpan timeSinceSunrise = CalculateTimeDifference(sunriseTime, currentTime.TimeOfDay);

            double percentage = timeSinceSunrise.TotalMinutes / sunriseToSunsetDuration.TotalMinutes;

            sunLightRot = Mathf.Lerp(0, 180, (float)percentage);
        }
        else
        {
            TimeSpan sunsetToSunriseDuration = CalculateTimeDifference(sunsetTime, sunriseTime);
            TimeSpan timeSinceSunset = CalculateTimeDifference(sunsetTime, currentTime.TimeOfDay);

            double percentage = timeSinceSunset.TotalMinutes / sunsetToSunriseDuration.TotalMinutes;

            sunLightRot = Mathf.Lerp(180, 360, (float)percentage);
        }

        sun.transform.rotation = Quaternion.AngleAxis(sunLightRot, Vector3.right);
    }

    private void UpdateLightSettings()
    {
        float dotProduct = Vector3.Dot(sun.transform.forward, Vector3.down);
        float curveValue = lightCurve.Evaluate(dotProduct);

        // Aplicar el multiplicador a la intensidad base
        sun.intensity = Mathf.Lerp(0, maxSunIntensity, curveValue) * lightIntensityMultiplier;
        //moon.intensity = Mathf.Lerp(0, maxMoonIntensity, curveValue) * lightIntensityMultiplier;

        RenderSettings.ambientLight = Color.Lerp(nightAmbientLight, dayAmbientLight, curveValue) * lightIntensityMultiplier;

        // Blend din�mico del cielo
        targetBlend = 1f - curveValue; // D�a a Noche
        float currentBlend = blendedSkybox.GetFloat("_Blend");
        float newBlend = Mathf.Lerp(currentBlend, targetBlend, Time.deltaTime * skyboxBlendSpeed);
        blendedSkybox.SetFloat("_Blend", newBlend);
    }

    public void SetLightIntensityMultiplier(float multiplier)
    {
        lightIntensityMultiplier = multiplier;
    }

    private TimeSpan CalculateTimeDifference(TimeSpan fromTime, TimeSpan toTime)
    {
        TimeSpan diff = toTime - fromTime;

        if (diff.TotalSeconds < 0)
            diff += TimeSpan.FromHours(24);

        return diff;
    }
}