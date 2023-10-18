using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherSetting : MonoBehaviour
{
    [SerializeField] Material[] skyboxMaterials;
    [SerializeField] GameObject[] weatherParticle;
    [SerializeField] TraitAttack traitAttack;

    private int curType = 10;
    private int preType = 10;

    public float value;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            RenderSettings.skybox = skyboxMaterials[0];
            SetParticle(0);
            UpdateBuff(0);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            RenderSettings.skybox = skyboxMaterials[1];
            SetParticle(1);
            UpdateBuff(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            RenderSettings.skybox = skyboxMaterials[2];
            SetParticle(2);
            UpdateBuff(2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            RenderSettings.skybox = skyboxMaterials[3];
            SetParticle(3);
            UpdateBuff(3);
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

    void UpdateBuff(int m_curType)
    {
        preType = curType;
        curType = m_curType;
        switch (curType)
        {
            case 0:
                break;
        }
        
        switch (preType)
        {
            case 0:
                break;
        }
    }
}
