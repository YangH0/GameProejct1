using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class TitleUIManager : MonoBehaviour
{
    public GameObject startButton;
    public GameObject ExitButton;
    public GameObject SettingButton;
    public Image cutScene;
    public Sprite[] cutSceneImage;
    [SerializeField] Slider mouseSlider;
    [SerializeField] Slider FOVSlider;

    [SerializeField] TraitData[] traitData;
    [SerializeField] GameObject traitInfoUI;
    [SerializeField] Image traitInfoImage;
    [SerializeField] TextMeshProUGUI traitInfoName;
    [SerializeField] TextMeshProUGUI traitInfoExp;



    private int curCurSceneNum = 0;
    private bool bIsCutScene;

    private void Awake()
    {
        startButton.SetActive(false);
        ExitButton.SetActive(false);
        cutScene.gameObject.SetActive(true);
        bIsCutScene = true;
        curCurSceneNum = 0;
        cutScene.sprite = cutSceneImage[curCurSceneNum];

        mouseSlider.value = PlayerPrefs.GetFloat("MouseSensitivity");
        FOVSlider.value = PlayerPrefs.GetFloat("FOV");
    }

    private void Update()
    {
        SetCutScene();
    }

    private void SetCutScene()
    {
        if (Input.anyKeyDown && bIsCutScene)
        {
            if(curCurSceneNum+1 >= 4)
            {
                bIsCutScene = false;
                startButton.SetActive(true);
                ExitButton.SetActive(true);
                cutScene.gameObject.SetActive(false);
                return;
            }
            cutScene.sprite = cutSceneImage[++curCurSceneNum];
        }
    }

    public void SetMouseSensitivity()
    {
        PlayerPrefs.SetFloat("MouseSensitivity", mouseSlider.value);
    }
    public void SetFOV()
    {
        PlayerPrefs.SetFloat("FOV", FOVSlider.value);
    }

    public void LoadInGameScene()
    {
        SceneManager.LoadScene("InGameScene");
    }

    public void SetSettingMenu()
    {
        if (SettingButton.activeSelf)
        {
            SettingButton.SetActive(false);
        }
        else
        {
            SettingButton.SetActive(true);
        }
    }

    public void OnTraitInfo(string name, string k_name, string explain)
    {
        traitInfoUI.SetActive(true);
        switch (name)
        {
            case "AttackDamage":
                SetTraitInfo(0, k_name, explain);
                break;
            case "AttackSpeed":
                SetTraitInfo(1, k_name, explain);
                break;
            case "HpUp":
                SetTraitInfo(2, k_name, explain);
                break;
            case "SpeedUp":
                SetTraitInfo(3, k_name, explain);
                break;
            case "ExpUp":
                SetTraitInfo(4, k_name, explain);
                break;

            case "IceBomb":
                break;
            case "IceSpear":
                break;
            case "IceField":
               
                break;
            case "FireWall":
               
                break;
            case "Meteor":
               
                break;
            case "FireField":
               
                break;
            case "Wisp":
                
                break;
            case "Shield":
                
                break;
            case "Tornado":
                break;
            case "ElectricField":
                
                break;
            case "Thunder":
                
                break;
            case "ThunderSpear":
                
                break;


            case "IceMagic": 
                break;
            case "FireMagic": 
                break;
            case "WindMagic": 
                break;
            case "ElecMagic": 
                break;

        }
    }
    public void SetTraitInfo(int num,string k_name,string explain)
    {
        traitInfoImage.sprite = traitData[num].Image;
        traitInfoName.text = k_name;
        traitInfoExp.text = explain;
    }

    public void OffTraitInfo()
    {
        traitInfoUI.SetActive(false);
    }

    public void GameExit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
