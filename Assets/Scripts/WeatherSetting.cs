using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherSetting : MonoBehaviour
{
    [SerializeField] Material[] skyboxMaterials;
    [SerializeField] GameObject[] weatherParticle;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            RenderSettings.skybox = skyboxMaterials[0];
            for (int i = 0; i < weatherParticle.Length; i++)
            {
                weatherParticle[i].SetActive(false);
            }
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            RenderSettings.skybox = skyboxMaterials[1];
            SetParticle(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            RenderSettings.skybox = skyboxMaterials[2];
            SetParticle(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            RenderSettings.skybox = skyboxMaterials[3];
            SetParticle(2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
        }
    }

    void SetParticle(int num)
    {
        for(int i = 0; i < weatherParticle.Length; i++)
        {
            weatherParticle[i].SetActive(false);
        }
        weatherParticle[num].SetActive(true);
    }
}
