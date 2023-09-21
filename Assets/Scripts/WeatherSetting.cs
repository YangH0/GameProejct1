using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherSetting : MonoBehaviour
{
    [SerializeField] Material[] materials;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            RenderSettings.skybox = materials[0];
        else if(Input.GetKeyDown(KeyCode.Alpha2))
            RenderSettings.skybox = materials[1];
        else if (Input.GetKeyDown(KeyCode.Alpha3))
            RenderSettings.skybox = materials[2];
        else if (Input.GetKeyDown(KeyCode.Alpha4))
            RenderSettings.skybox = materials[3];
        else if (Input.GetKeyDown(KeyCode.Alpha4))
            RenderSettings.skybox = materials[4];
    }
}
