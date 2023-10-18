using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
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

    [SerializeField] CinemachineFreeLook freelook;

    [SerializeField] GameObject traitSet;
    [SerializeField] GameObject crossLine;
    [SerializeField] GameObject SettingMenu;

    private List<TraitData> traitList = new List<TraitData>();
    private List<TraitData> traitSelectionList = new List<TraitData>();
    private List<TraitData> ownTraitList = new List<TraitData>();

    private int randomNum;
    private int traitCount = 0;

    private void Start()
    {
        for (int i = 0; i < traitData.Length; i++)
        {
            traitData[i].curLevel = 0;
        }
        Time.timeScale = 1;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!SettingMenu.activeSelf)
                SetSettingMenu(true);
            else
                SetSettingMenu(false);
        }
    }

    public void UpdateTraitSelection()
    {
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
            traitUpdate.UpdateTraitText();
            traitName[i].text = traitSelectionList[i].traitName;
            traitImage[i] = traitSelectionList[i].Image;
            levelChange[i].text = traitSelectionList[i].curLevel.ToString() + "->" + (traitSelectionList[i].curLevel+1).ToString();
            explanation[i].text = traitUpdate.explanation;
        }
    }

    public void SelectTrait(int num)
    {
        Time.timeScale = 1;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        traitUpdate.UpdateTraitData(traitSelectionList[num]);
        traitUpdate.UpdateTraitValue();
        if(traitSelectionList[num].traitName == "IceMagic" || traitSelectionList[num].traitName == "FireMagic"
            || traitSelectionList[num].traitName == "WindMagic" || traitSelectionList[num].traitName == "ElecMagic")
        {
            traitData[0].curLevel = 1; traitData[1].curLevel = 1; traitData[2].curLevel = 1; traitData[3].curLevel = 1;
        }
        else
        {
            if(traitSelectionList[num].curLevel == 0 && traitSelectionList[num].traitType == 0)
            {
                ownTraitList.Add(traitSelectionList[num]);
                traitCount++;
            }
        }
        for (int i = 0; i < ownTraitList.Count; i++)
        {
            Debug.Log(ownTraitList.Count);
            Debug.Log(ownTraitList[i].traitName);
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
    }
    public void SetFOV()
    {
        freelook.m_Lens.FieldOfView= FOVSlider.value+30;
    }
    public void LoadTitleScene()
    {
        SceneManager.LoadScene("TitleScene");
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

}
