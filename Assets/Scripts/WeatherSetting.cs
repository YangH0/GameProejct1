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

    private void Start()
    {
        RenderSettings.skybox = skyboxMaterials[0];
        SetParticle(0);
        UpdateBuff(0);
        StartCoroutine(StartTimer());
    }

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
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            RenderSettings.skybox = skyboxMaterials[3];
            SetParticle(4);
            UpdateBuff(4);
        }
    }

    private void SetWeather(int num)
    {
        switch (num)
        {
            case 0:
                RenderSettings.skybox = skyboxMaterials[0];
                SetParticle(0);
                UpdateBuff(0);
                break;
            case 1:
                RenderSettings.skybox = skyboxMaterials[1];
                SetParticle(1);
                UpdateBuff(1);
                break;
            case 2:
                RenderSettings.skybox = skyboxMaterials[2];
                SetParticle(2);
                UpdateBuff(2);
                break;
            case 3:
                RenderSettings.skybox = skyboxMaterials[3];
                SetParticle(3);
                UpdateBuff(3);
                break;
            case 4:
                RenderSettings.skybox = skyboxMaterials[4];
                SetParticle(4);
                UpdateBuff(4);
                break;
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

    private IEnumerator StartTimer()
    {
        yield return new WaitForSeconds(10f);
        int num = Random.Range(0, 5);
        SetWeather(num);
        StartCoroutine(StartTimer());
    }
}
