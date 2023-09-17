using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "TraitData",menuName = "Scriptable Object/Trait Data", order = int.MaxValue)]
public class TraitData : ScriptableObject
{
    public string traitName;
    public Image Image;
    public int curLevel = 0;
    public int maxLevel;
    public float damageValue;
    public float numValue;
    public float coolTimeValue;
    public float rangeValue;

}
