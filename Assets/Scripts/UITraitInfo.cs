using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UITraitInfo : MonoBehaviour, IPointerEnterHandler , IPointerExitHandler
{
    [SerializeField] TitleUIManager uiManager;
    public string traitName;
    public string name;
    public string explain;

    public void OnPointerEnter(PointerEventData eventData)
    {
        uiManager.OnTraitInfo(traitName,name, explain);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        uiManager.OffTraitInfo();
    }
}
