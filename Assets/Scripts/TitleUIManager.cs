using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;

public class TitleUIManager : MonoBehaviour
{
    public GameObject startButton;
    public GameObject ExitButton;
    public GameObject SettingButton;
    public Image cutScene;
    public Sprite[] cutSceneImage;
    [SerializeField] Slider mouseSlider;
    [SerializeField] Slider FOVSlider;
    [SerializeField] Slider MVolumeSlider;
    [SerializeField] Slider BGMSlider;
    [SerializeField] Slider SFXSlider;

    public AudioMixer masterMixer;

    [SerializeField] TraitData[] traitData;
    [SerializeField] GameObject traitInfoUI;
    [SerializeField] Image traitInfoImage;
    [SerializeField] TextMeshProUGUI traitInfoName;
    [SerializeField] TextMeshProUGUI traitInfoExp;

    [SerializeField] GameObject scoreBoard;

    [SerializeField] TextMeshProUGUI[] scoreText;


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

        
    }

    private void Start()
    {
        mouseSlider.value = PlayerPrefs.GetFloat("MouseSensitivity");
        FOVSlider.value = PlayerPrefs.GetFloat("FOV");
        MVolumeSlider.value = PlayerPrefs.GetFloat("MasterVolume");
        BGMSlider.value = PlayerPrefs.GetFloat("BGMVolume");
        SFXSlider.value = PlayerPrefs.GetFloat("SFXVolume");
        masterMixer.SetFloat("MasterVolume", MVolumeSlider.value);
        masterMixer.SetFloat("BGMVolume", BGMSlider.value);
        masterMixer.SetFloat("SFXVolume", SFXSlider.value);
    }

    private void Update()
    {
        SetCutScene();
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SetSettingMenu();
        }
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

    public void SetMasterVolume()
    {
        masterMixer.SetFloat("MasterVolume", MVolumeSlider.value);
        PlayerPrefs.SetFloat("MasterVolume", MVolumeSlider.value);
    }

    public void SetBGMVolume()
    {
        masterMixer.SetFloat("BGMVolume", BGMSlider.value);
        PlayerPrefs.SetFloat("BGMVolume", BGMSlider.value);
    }

    public void SetSFXVolume()
    {
        masterMixer.SetFloat("SFXVolume", SFXSlider.value);
        PlayerPrefs.SetFloat("SFXVolume", SFXSlider.value);
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
            scoreBoard.SetActive(false);
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
                SetTraitInfo(5, k_name, explain);
                break;
            case "IceSpear":
                SetTraitInfo(6, k_name, explain);
                break;
            case "IceField":
                SetTraitInfo(7, k_name, explain);
                break;
            case "FireWall":
                SetTraitInfo(8, k_name, explain);
                break;
            case "Meteor":
                SetTraitInfo(9, k_name, explain);
                break;
            case "FireField":
                SetTraitInfo(10, k_name, explain);
                break;
            case "Wisp":
                SetTraitInfo(11, k_name, explain);
                break;
            case "Shield":
                SetTraitInfo(12, k_name, explain);
                break;
            case "Tornado":
                SetTraitInfo(13, k_name, explain);
                break;
            case "ElectricField":
                SetTraitInfo(14, k_name, explain);
                break;
            case "Thunder":
                SetTraitInfo(15, k_name, explain);
                break;
            case "ThunderSpear":
                SetTraitInfo(16, k_name, explain);
                break;


            case "IceMagic":
                SetTraitInfo(17, k_name, explain);
                break;
            case "FireMagic":
                SetTraitInfo(18, k_name, explain);
                break;
            case "WindMagic":
                SetTraitInfo(19, k_name, explain);
                break;
            case "ElecMagic":
                SetTraitInfo(20, k_name, explain);
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

    public void SetScoreBoard()
    {
        if (scoreBoard.activeSelf)
        {
            scoreBoard.SetActive(false);
        }
        else
        {
            scoreBoard.SetActive(true);
            SettingButton.SetActive(false);
            for(int i = 0; i < 6; i++)
            {
                if (PlayerPrefs.HasKey("Score" + i.ToString()))
                {
                    int curTime = PlayerPrefs.GetInt("Score" + i.ToString());
                    int minutes = Mathf.FloorToInt(curTime / 60);
                    int seconds = Mathf.FloorToInt(curTime % 60);
                    scoreText[i].text = string.Format("{0:00}:{1:00}", minutes, seconds);
                }
            }
        }
        
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
