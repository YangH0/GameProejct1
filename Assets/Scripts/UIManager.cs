using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] TraitUpdate traitUpdate;
    [SerializeField] TraitData[] traitData;
    [SerializeField] TextMeshProUGUI[] traitName;
    [SerializeField] Image[] traitImage;
    [SerializeField] TextMeshProUGUI[] levelChange;
    [SerializeField] TextMeshProUGUI[] explanation;

    [SerializeField] GameObject traitSet;
    [SerializeField] GameObject crossLine;

    private List<TraitData> traitList = new List<TraitData>();
    private List<TraitData> traitSelectionList = new List<TraitData>();

    private int randomNum;

    private void Start()
    {
        for (int i = 0; i < traitData.Length; i++)
        {
            traitData[i].curLevel = 0;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            UpdateTraitSelection();
            traitSet.SetActive(true);
            crossLine.SetActive(false);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public void UpdateTraitSelection()
    {
        traitList.Clear();
        traitSelectionList.Clear();
        for(int i = 0; i < traitData.Length; i++)
        {
            if(traitData[i].curLevel < traitData[i].maxLevel)
            {
                traitList.Add(traitData[i]);
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
        traitUpdate.UpdateTraitData(traitSelectionList[num]);
        traitUpdate.UpdateTraitValue();
        traitSelectionList[num].curLevel++;
        traitSet.SetActive(false);
        crossLine.SetActive(true);
    }
}
