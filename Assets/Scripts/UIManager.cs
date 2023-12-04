using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using Cinemachine;

public class UIManager : MonoBehaviour
{
    [SerializeField] TraitUpdate traitUpdate;
    [SerializeField] TraitData[] traitData;
    [SerializeField] TextMeshProUGUI[] traitName;
    [SerializeField] Image[] traitImage;
    [SerializeField] TextMeshProUGUI[] levelChange;
    [SerializeField] TextMeshProUGUI[] explanation;
    [SerializeField] Slider mouseSlider;
    [SerializeField] Slider FOVSlider;
    [SerializeField] Slider hpSlider;
    [SerializeField] Slider expSlider;

    [SerializeField] GameObject[] UI_Trait;
    [SerializeField] Image[] UI_traitImage;
    [SerializeField] TextMeshProUGUI[] UI_Level;

    [SerializeField] CinemachineFreeLook freelook;

    [SerializeField] GameObject traitSet;
    [SerializeField] GameObject crossLine;
    [SerializeField] GameObject SettingMenu;

    [SerializeField] GameObject[] coolTimeUI;

    [SerializeField] GameObject gameOverUI;
    [SerializeField] TextMeshProUGUI gameOverTimerUI;
    [SerializeField] TextMeshProUGUI timerUI;

    private List<TraitData> traitList = new List<TraitData>();
    private List<TraitData> traitSelectionList = new List<TraitData>();
    private List<TraitData> ownTraitList = new List<TraitData>();

    private int randomNum;
    private int traitCount = 0;
    private int magicNum = -1;
    private int curTime = 0;

    public bool bIsTrait;

    private void Awake()
    {
        bIsTrait = false;
        for (int i = 0; i < traitData.Length; i++)
        {
            traitData[i].curLevel = 0;
        }
        Time.timeScale = 1;
        StartCoroutine(StartTimer());
        mouseSlider.value = PlayerPrefs.GetFloat("MouseSensitivity");
        FOVSlider.value = PlayerPrefs.GetFloat("FOV");
        freelook.m_XAxis.m_MaxSpeed = mouseSlider.value * 150;
        freelook.m_YAxis.m_MaxSpeed = mouseSlider.value * 2;
        freelook.m_Lens.FieldOfView = FOVSlider.value + 30;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !bIsTrait)
        {
            if (!SettingMenu.activeSelf)
            {
                SetSettingMenu(true);
                SetTraitUI();
            }

            else
                SetSettingMenu(false);
        }
    }

    public void UpdateTraitSelection()
    {
        bIsTrait = true;
        Time.timeScale = 0;
        traitList.Clear();
        traitSelectionList.Clear();
        if(traitCount >= 6)
        {
            for (int i = 0; i < ownTraitList.Count; i++)
            {
                if (ownTraitList[i].curLevel < ownTraitList[i].maxLevel)
                {
                    traitList.Add(ownTraitList[i]);
                }
            }
            for(int i = 0; i < traitData.Length; i++)
            {
                if (traitData[i].curLevel < traitData[i].maxLevel && traitData[i].traitType == 1)
                {
                    traitList.Add(traitData[i]);
                }
            }
        }
        else
        {
            for (int i = 0; i < traitData.Length; i++)
            {
                if (traitData[i].curLevel < traitData[i].maxLevel)
                {
                    traitList.Add(traitData[i]);
                }
            }
        }
        
        for(int i = 0; i < 3; i++)
        {
            randomNum = Random.Range(0, traitList.Count);
            traitSelectionList.Add(traitList[randomNum]);
            traitList.RemoveAt(randomNum);
        }

        for(int i = 0; i < 3; i++)
        {
            traitUpdate.UpdateTraitData(traitSelectionList[i]);
            traitName[i].text = traitUpdate.UpdateTraitText();
            traitImage[i].sprite = traitSelectionList[i].Image;
            levelChange[i].text = traitSelectionList[i].curLevel.ToString() + "->" + (traitSelectionList[i].curLevel+1).ToString();
            explanation[i].text = traitUpdate.explanation;
        }
    }

    public void SelectTrait(int num)
    {
        bIsTrait = false;
        Time.timeScale = 1;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        traitUpdate.UpdateTraitData(traitSelectionList[num]);
        traitUpdate.UpdateTraitValue();
        if(traitSelectionList[num].traitName == "IceMagic" || traitSelectionList[num].traitName == "FireMagic"
            || traitSelectionList[num].traitName == "WindMagic" || traitSelectionList[num].traitName == "ElecMagic")
        {
            traitData[0].curLevel = 1; traitData[1].curLevel = 1; traitData[2].curLevel = 1; traitData[3].curLevel = 1;
            switch (traitSelectionList[num].traitName)
            {
                case "IceMagic":
                    magicNum = 0;
                    break;
                case "FireMagic":
                    magicNum = 1;
                    break;
                case "WindMagic":
                    magicNum = 2;
                    break;
                case "ElecMagic":
                    magicNum = 3;
                    break;
            }
        }
        else
        {
            if(traitSelectionList[num].curLevel == 0 && traitSelectionList[num].traitType == 0)
            {
                ownTraitList.Add(traitSelectionList[num]);
                traitCount++;
                if (traitSelectionList[num].name != "IceField" && traitSelectionList[num].name != "FireField" && traitSelectionList[num].name != "ElectricField")
                {
                    traitUpdate.coolTimeImage[traitUpdate.traitCount-1].sprite = traitSelectionList[num].Image;
                    coolTimeUI[traitUpdate.traitCount - 1].SetActive(true);
                }
            }
        }
        
        traitSelectionList[num].curLevel++;
        traitSet.SetActive(false);
        crossLine.SetActive(true);
    }

    public void LevelUp()
    {
        UpdateTraitSelection();
        traitSet.SetActive(true);
        crossLine.SetActive(false);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void SetMouseSensitivity()
    {
        freelook.m_XAxis.m_MaxSpeed = mouseSlider.value*150;
        freelook.m_YAxis.m_MaxSpeed = mouseSlider.value*2;
        PlayerPrefs.SetFloat("MouseSensitivity", mouseSlider.value);
    }
    public void SetFOV()
    {
        freelook.m_Lens.FieldOfView= FOVSlider.value+30;
        PlayerPrefs.SetFloat("FOV", FOVSlider.value);
    }
    public void LoadTitleScene()
    {
        SceneManager.LoadScene("TitleScene");
    }
    public void LoadGameScene()
    {
        SceneManager.LoadScene("inGameScene");
    }

    public void SetGameOverUI()
    {
        Time.timeScale = 0;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        int minutes = Mathf.FloorToInt(curTime / 60);
        int seconds = Mathf.FloorToInt(curTime % 60);
        gameOverTimerUI.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        bIsTrait = true;
        gameOverUI.SetActive(true);

        if(PlayerPrefs.GetInt("Score0") < curTime || !PlayerPrefs.HasKey("Score0"))
        {
            PlayerPrefs.SetInt("Score0", curTime);
        }
        else
        {
            
            for (int i = 5; i > 1; i--)
            {
                if (PlayerPrefs.HasKey("Score" + (i - 1).ToString()))
                    PlayerPrefs.SetInt("Score" + i.ToString(), PlayerPrefs.GetInt("Score" + (i - 1).ToString()));
            }
            PlayerPrefs.SetInt("Score1", curTime);
        }
    }

    public void SetSettingMenu(bool state)
    {
        SettingMenu.SetActive(state);
        if (state)
        {
            Time.timeScale = 0;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Time.timeScale = 1;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    private void SetTraitUI()
    {
        int count = 0;
        Debug.Log(ownTraitList.Count);

        if (magicNum != -1)
        {
            UI_Trait[count].SetActive(true);
            UI_traitImage[count].sprite = traitData[magicNum].Image;
            UI_Level[count++].text = (traitData[magicNum].curLevel-1).ToString();
        }

        for(int i = 0; i < ownTraitList.Count; i++)
        {
            UI_Trait[count].SetActive(true);
            UI_traitImage[count].sprite = ownTraitList[i].Image;
            UI_Level[count++].text = ownTraitList[i].curLevel.ToString();
        }
        for (int i = 0; i < traitData.Length; i++)
        {
            if (traitData[i].curLevel>0 && traitData[i].traitType == 1)
            {
                UI_Trait[count].SetActive(true);
                UI_traitImage[count].sprite = traitData[i].Image;
                UI_Level[count++].text = traitData[i].curLevel.ToString();
            }
        }
    }
    
    public void SetMaxHpUI(float hp)
    {
        hpSlider.maxValue = hp;
    }
    public void SetHpUI(float hp)
    {
        hpSlider.value = hp;
    }
    public void SetMaxExpUI(float exp)
    {
        expSlider.maxValue = exp;
    }
    public void SetExpUI(float exp)
    {
        expSlider.value = exp;
    }

    private IEnumerator StartTimer()
    {
        int minutes = Mathf.FloorToInt(curTime / 60);
        int seconds = Mathf.FloorToInt(curTime % 60);
        timerUI.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        yield return new WaitForSeconds(1f);
        curTime++;
        StartCoroutine(StartTimer());
    }

}
