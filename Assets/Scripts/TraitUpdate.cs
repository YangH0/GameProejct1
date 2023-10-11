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
    private int pierce;


    public string explanation;

    public void UpdateTraitData(TraitData traitData)
    {
        traitName = traitData.traitName;
        curLevel = traitData.curLevel;
        damageValue = traitData.damageValue;
        numValue = traitData.numValue;
        coolTimeValue = traitData.coolTimeValue;
        rangeValue = traitData.rangeValue;
        pierce = traitData.pierce;
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
            case "ManaElemental":
                traitAttack.ManaDamage += damageValue;
                break;
            case "Test1": // 투사체 관통X
                if (curLevel == 0)
                    traitAttack.StartCoroutine(traitAttack.CTestAttack1());
                else
                {
                    traitAttack.traitInfo[0].damage += damageValue;
                    Debug.Log(traitAttack.traitInfo[0].damage);
                    iNum = (int)(traitAttack.traitInfo[0].coolTime * 10);
                    iNum -= (int)(coolTimeValue * 10);
                    traitAttack.traitInfo[0].coolTime = (float)iNum * 0.1f;
                    Debug.Log(traitAttack.traitInfo[0].coolTime);

                }
                break;
            case "Test2": // 범위 공격
                if (curLevel == 0)
                    traitAttack.StartCoroutine(traitAttack.CTestAttack2());
                else
                {
                    traitAttack.traitInfo[1].damage += damageValue;
                    iNum = (int)(traitAttack.traitInfo[1].coolTime * 10);
                    iNum -= (int)(coolTimeValue * 10);
                    traitAttack.traitInfo[1].coolTime = (float)iNum * 0.1f;
                }
                break;
            case "Test3": // 관통
                if (curLevel == 0)
                    traitAttack.StartCoroutine(traitAttack.CTestAttack3());
                else
                {
                    traitAttack.traitInfo[2].damage += damageValue;
                    iNum = (int)(traitAttack.traitInfo[2].coolTime * 10);
                    iNum -= (int)(coolTimeValue * 10);
                    traitAttack.traitInfo[2].coolTime = (float)iNum * 0.1f;
                    traitAttack.traitInfo[2].pierce += pierce;
                }
                break;
            case "FireMagic": // 불 기본공격
                player.ChangeAutoAttack(1);
                break;
            case "IceMagic": // 얼음 기본공격
                player.ChangeAutoAttack(2);
                break;
            case "WindMagic": // 바람 기본공격
                player.ChangeAutoAttack(3);
                break;
            case "ElecMagic": // 바람 기본공격
                player.ChangeAutoAttack(4);
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
            case "ManaElemental":
                explanation += traitAttack.ManaDamage.ToString() + "->" + (traitAttack.ManaDamage + damageValue).ToString() + "\n";
                break;
            case "Test1":
                explanation += traitAttack.traitInfo[0].damage.ToString() + "->" + (traitAttack.traitInfo[0].damage + damageValue).ToString() + "\n";
                explanation += traitAttack.traitInfo[0].coolTime.ToString() + "->" + (traitAttack.traitInfo[0].coolTime - coolTimeValue).ToString();
                break;
            case "Test2":
                explanation += traitAttack.traitInfo[1].damage.ToString() + "->" + (traitAttack.traitInfo[1].damage + damageValue).ToString() + "\n";
                explanation += traitAttack.traitInfo[1].coolTime.ToString() + "->" + (traitAttack.traitInfo[1].coolTime - coolTimeValue).ToString();
                break;
            case "Test3":
                explanation += traitAttack.traitInfo[2].damage.ToString() + "->" + (traitAttack.traitInfo[2].damage + damageValue).ToString() + "\n";
                explanation += traitAttack.traitInfo[2].coolTime.ToString() + "->" + (traitAttack.traitInfo[2].coolTime - coolTimeValue).ToString();
                explanation += traitAttack.traitInfo[2].pierce.ToString() + "->" + (traitAttack.traitInfo[2].pierce +pierce).ToString();
                break;
        }

    }


}
