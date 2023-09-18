using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraitUpdate : MonoBehaviour
{
    [SerializeField]
    Player player;

    [SerializeField]
    TraitAttack traitAttack;

    private string traitName;
    private int curLevel;
    private float damageValue;
    private float numValue;
    private float coolTimeValue;
    private float rangeValue;


    public string explanation;

    public void UpdateTraitData(TraitData traitData)
    {
        traitName = traitData.traitName;
        curLevel = traitData.curLevel;
        damageValue = traitData.damageValue;
        numValue = traitData.numValue;
        coolTimeValue = traitData.coolTimeValue;
        rangeValue = traitData.rangeValue;
    }


    public void UpdateTraitValue()
    {
        int iNum;
        switch (traitName)
        {
            case "AutoAttack1":
                player.autoAttackDamage += damageValue;
                iNum = (int)(player.maxAttackTime * 10);
                iNum -= (int)(coolTimeValue * 10);
                player.maxAttackTime = (float)iNum * 0.1f;
                break;
            case "AutoAttack2":
                player.autoAttackDamage += damageValue;
                iNum = (int)(player.maxAttackTime * 10);
                iNum -= (int)(coolTimeValue * 10);
                player.maxAttackTime = (float)iNum * 0.1f;
                break;
            case "AutoAttack3":
                player.autoAttackDamage += damageValue;
                iNum = (int)(player.maxAttackTime * 10);
                iNum -= (int)(coolTimeValue * 10);
                player.maxAttackTime = (float)iNum * 0.1f;
                break;
            case "AutoAttack4":
                player.autoAttackDamage += damageValue;
                iNum = (int)(player.maxAttackTime * 10);
                iNum -= (int)(coolTimeValue * 10);
                player.maxAttackTime = (float)iNum * 0.1f;
                break;
        }
    }

    public void UpdateTraitText()
    {
        explanation = null;
        switch (traitName)
        {
            case "AutoAttack1":
                explanation += player.autoAttackDamage.ToString() + "->" + (player.autoAttackDamage + damageValue).ToString() +"\n";
                explanation += player.maxAttackTime.ToString() + "->" + (player.maxAttackTime-coolTimeValue).ToString();
                break;
            case "AutoAttack2":
                explanation += player.autoAttackDamage.ToString() + "->" + (player.autoAttackDamage + damageValue).ToString() + "\n";
                explanation += player.maxAttackTime.ToString() + "->" + (player.maxAttackTime - coolTimeValue).ToString();
                break;
            case "AutoAttack3":
                explanation += player.autoAttackDamage.ToString() + "->" + (player.autoAttackDamage + damageValue).ToString() + "\n";
                explanation += player.maxAttackTime.ToString() + "->" + (player.maxAttackTime - coolTimeValue).ToString();
                break;
            case "AutoAttack4":
                explanation += player.autoAttackDamage.ToString() + "->" + (player.autoAttackDamage + damageValue).ToString() + "\n";
                explanation += player.maxAttackTime.ToString() + "->" + (player.maxAttackTime - coolTimeValue).ToString();
                break;
        }

    }


}