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
    private float coolTimeValue;
    private float rangeValue;
    private int numValue;
    private int pierce;
    int iNum;



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
        switch (traitName)
        {
            case "AttackDamage":
                player.autoAttackDamage += damageValue;
                iNum = (int)(player.maxAttackTime * 10);
                iNum -= (int)(coolTimeValue * 10);
                player.maxAttackTime = (float)iNum * 0.1f;
                break;
            case "AttackSpeed":
                iNum = (int)(player.maxAttackTime * 10);
                iNum -= (int)(coolTimeValue * 10);
                player.maxAttackTime = (float)iNum * 0.1f;
                break;
            case "HpUp":
                player.maxHp += damageValue;
                player.hp += damageValue;
                break;
            case "SpeedUp":
                player.walkSpeed += damageValue;
                player.runSpeed += damageValue;
                break;
            case "ExpUp":
                player.expMulti += damageValue;
                break;

            case "IceBomb":
                if (curLevel == 0)
                    traitAttack.StartCoroutine(traitAttack.IceBomb());
                else
                {
                    UpdateValue(0);
                }
                break;
            case "IceSpear":
                if (curLevel == 0)
                    traitAttack.StartCoroutine(traitAttack.IceSpear());
                else
                {
                    UpdateValue(1);
                }
                break;
            case "IceField":
                if (curLevel == 0)
                    traitAttack.StartCoroutine(traitAttack.IceField());
                else
                {
                    UpdateValue(2);
                }
                break;
            case "FireWall":
                if (curLevel == 0)
                    traitAttack.StartCoroutine(traitAttack.FireWall());
                else
                {
                    UpdateValue(3);
                }
                break;
            case "Meteor":
                if (curLevel == 0)
                    traitAttack.StartCoroutine(traitAttack.Meteor());
                else
                {
                    UpdateValue(4);
                }
                break;
            case "FireField":
                if (curLevel == 0)
                    traitAttack.StartCoroutine(traitAttack.FireField());
                else
                {
                    UpdateValue(5);
                    traitAttack.StartCoroutine(traitAttack.FireField());
                }
                break;
            case "Wisp":
                if (curLevel == 0)
                    traitAttack.StartCoroutine(traitAttack.Wisp());
                else
                {
                    UpdateValue(6);

                }
                traitAttack.SpawnWisp(curLevel);
                break;
            case "Shield":
                if (curLevel == 0)
                    player.StartCoroutine(player.Shield());
                else
                {
                    iNum = (int)(player.shieldCoolTime * 10);
                    iNum -= (int)(coolTimeValue * 10);
                    player.shieldCoolTime = (float)iNum * 0.1f;
                    player.shieldDamage += damageValue;
                }
                break;
            case "Tornado":
                if (curLevel == 0)
                    traitAttack.StartCoroutine(traitAttack.Tornado());
                else
                {
                    UpdateValue(8);

                }
                break;
            case "ElectricField":
                if (curLevel == 0)
                    traitAttack.StartCoroutine(traitAttack.ElectricField());
                else
                {
                    UpdateValue(9);
                    traitAttack.StartCoroutine(traitAttack.ElectricField());
                }
                break;
            case "Thunder":
                if (curLevel == 0)
                    traitAttack.StartCoroutine(traitAttack.Thunder());
                else
                {
                    UpdateValue(10);

                }
                break;
            case "ThunderSpear":
                if (curLevel == 0)
                    traitAttack.StartCoroutine(traitAttack.ThunderSpear());
                else
                {
                    UpdateValue(11);

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

    private void UpdateValue(int i)
    {
        traitAttack.traitInfo[i].damage += damageValue;
        traitAttack.traitInfo[i].num += numValue;
        traitAttack.traitInfo[i].range += rangeValue;
        iNum = (int)(traitAttack.traitInfo[i].coolTime * 10);
        iNum -= (int)(coolTimeValue * 10);
        traitAttack.traitInfo[i].coolTime = (float)iNum * 0.1f;
        traitAttack.traitInfo[i].pierce += pierce;
    }


}
