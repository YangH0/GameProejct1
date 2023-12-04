using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TraitUpdate : MonoBehaviour
{
    [SerializeField]
    Player player;

    [SerializeField]
    TraitAttack traitAttack;

    [SerializeField] public Image[] coolTimeImage;

    public int traitCount = 0;

    private string traitName;
    private int curLevel;
    private float damageValue;
    private float coolTimeValue;
    private float rangeValue;
    private int numValue;
    private int pierce;
    int iNum;
    string text;
    string UIName;


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
                player.SetHpSlide();
                break;
            case "SpeedUp":
                player.walkSpeed += damageValue;
                player.runSpeed += damageValue*1.5f;
                break;
            case "ExpUp":
                player.expMulti += damageValue;
                break;

            case "IceBomb":
                if (curLevel == 0)
                {
                    traitAttack.StartCoroutine(traitAttack.IceBomb());
                    traitAttack.SetCoolTimeUI(0,traitCount++);
                }
                else
                {
                    UpdateValue(0);
                }
                break;
            case "IceSpear":
                if (curLevel == 0)
                {
                    traitAttack.StartCoroutine(traitAttack.IceSpear());
                    traitAttack.SetCoolTimeUI(1, traitCount++);
                }
                else
                {
                    UpdateValue(1);
                }
                break;
            case "IceField":
                if (curLevel == 0)
                {
                    traitAttack.StartCoroutine(traitAttack.IceField());
                }
                else
                {
                    UpdateValue(2);
                }
                break;
            case "FireWall":
                if (curLevel == 0)
                {
                    traitAttack.StartCoroutine(traitAttack.FireWall());
                    traitAttack.SetCoolTimeUI(3, traitCount++);
                }
                else
                {
                    UpdateValue(3);
                }
                break;
            case "Meteor":
                if (curLevel == 0)
                {
                    traitAttack.StartCoroutine(traitAttack.Meteor());
                    traitAttack.SetCoolTimeUI(4, traitCount++);
                }
                else
                {
                    UpdateValue(4);
                }
                break;
            case "FireField":
                if (curLevel == 0)
                {
                    traitAttack.StartCoroutine(traitAttack.FireField());
                }
                else
                {
                    UpdateValue(5);
                    traitAttack.StartCoroutine(traitAttack.FireField());
                }
                break;
            case "Wisp":
                if (curLevel == 0)
                {
                    traitAttack.StartCoroutine(traitAttack.Wisp());
                    traitAttack.SetCoolTimeUI(6, traitCount++);
                }
                else
                {
                    UpdateValue(6);

                }
                traitAttack.SpawnWisp(curLevel);
                break;
            case "Shield":
                if (curLevel == 0)
                {
                    player.StartCoroutine(player.Shield());
                    traitAttack.SetCoolTimeUI(7, traitCount++);
                }
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
                {
                    traitAttack.StartCoroutine(traitAttack.Tornado());
                    traitAttack.SetCoolTimeUI(8, traitCount++);
                }
                else
                {
                    UpdateValue(8);

                }
                break;
            case "ElectricField":
                if (curLevel == 0)
                {
                    traitAttack.StartCoroutine(traitAttack.ElectricField());
                }
                else
                {
                    UpdateValue(9);
                    traitAttack.StartCoroutine(traitAttack.ElectricField());
                }
                break;
            case "Thunder":
                if (curLevel == 0)
                {
                    traitAttack.StartCoroutine(traitAttack.Thunder());
                    traitAttack.SetCoolTimeUI(10, traitCount++);
                }
                else
                {
                    UpdateValue(10);

                }
                break;
            case "ThunderSpear":
                if (curLevel == 0) 
                {
                    traitAttack.StartCoroutine(traitAttack.ThunderSpear());
                    traitAttack.SetCoolTimeUI(11, traitCount++);
                }
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

    public string UpdateTraitText()
    {
        explanation = null;
        
        switch (traitName)
        {
            case "AttackDamage":
                explanation += "기본 공격력이 증가합니다" + "\n" +
                player.autoAttackDamage.ToString() + "->" + (player.autoAttackDamage + damageValue).ToString();
                UIName = "공격력 증가";
                break;
            case "AttackSpeed":
                explanation += "기본 공격속도가 증가합니다" + "\n" +
                player.maxAttackTime.ToString() + "초 -> " + 
                ((float)((int)(player.maxAttackTime * 10) - (int)(coolTimeValue * 10)) * 0.1f).ToString() + "초";
                UIName = "공격 속도 증가";
                break;
            case "HpUp":
                explanation += "최대체력이 증가합니다" + "\n" +
                player.maxHp.ToString() + "->" + (player.maxHp + damageValue).ToString();
                UIName = "체력 증가";
                break;
            case "SpeedUp":
                explanation += "이동속도가 증가합니다" + "\n" +
                player.walkSpeed.ToString() + "->" + (player.walkSpeed + damageValue).ToString();
                UIName = "이동 속도 증가";
                break;
            case "ExpUp":
                explanation += "경험치 획득량이 증가합니다" + "\n" +
                player.expMulti.ToString() + "% -> " + (player.expMulti + damageValue).ToString() +"%";
                UIName = "경험치 증가";
                break;

            case "IceBomb":
                if (curLevel == 0)
                    explanation += "닿으면 폭발하는 얼음폭탄을 가까운 적에게 발사합니다";
                else
                {
                    explanation += AddExpl(0, 0) + AddExpl(0, 1) + AddExpl(0, 2);
                }
                UIName = "얼음폭탄";
                break;
            case "IceSpear":
                if (curLevel == 0)
                    explanation += "가까운 적에게 관통하는 얼음 창을 발사합니다";
                else
                {
                    explanation += AddExpl(1, 0) + AddExpl(1, 1) + AddExpl(1, 4);
                }
                UIName = "얼음창";
                break;
            case "IceField":
                if (curLevel == 0)
                    explanation += "이동할 때 적을 느리게하는 얼음 안개를 남깁니다";
                else
                {
                    explanation += AddExpl(2, 0) + AddExpl(2, 1);
                }
                UIName = "냉기이동";
                break;
            case "FireWall":
                if (curLevel == 0)
                    explanation += "가까운 적의 발 밑에서 불기둥이 생성됩니다";
                else
                {
                    explanation += AddExpl(3, 0) + AddExpl(3, 1) + AddExpl(3, 2);
                }
                UIName = "불기둥";
                break;
            case "Meteor":
                if (curLevel == 0)
                    explanation += "가까운 적에게 유성우가 떨어집니다";
                else
                {
                    explanation += AddExpl(4, 0) + AddExpl(4, 1) + AddExpl(4, 3);
                }
                UIName = "유성우";
                break;
            case "FireField":
                if (curLevel == 0)
                    explanation += "플레이어 주위로 뜨거운 열기가 생깁니다";
                else
                {
                    explanation += AddExpl(5, 0) + AddExpl(5, 1) + AddExpl(5, 2);
                }
                UIName = "열기";
                break;
            case "Wisp":
                if (curLevel == 0)
                    explanation += "가까운 적을 공격하는 위습이 생깁니다";
                else
                {
                    explanation += AddExpl(6, 0) + "위습 수 : " + curLevel.ToString() + "->" + (curLevel+1).ToString();
                }
                UIName = "위습";
                break;
            case "Shield":
                if (curLevel == 0)
                    explanation += "적의 공격을 막는 보호막이 생깁니다";
                else
                {
                    explanation += "충격파 데미지 : " + player.shieldDamage.ToString() + "->" + (player.shieldDamage + damageValue).ToString() + "\n" +
                        "쿨타임 : " + player.shieldCoolTime.ToString() + "초 -> " +
                        ((float)((int)(player.shieldCoolTime * 10) - (int)(coolTimeValue * 10)) * 0.1f).ToString() + "초";
                }
                UIName = "보호막";
                break;
            case "Tornado":
                if (curLevel == 0)
                    explanation += "적을 관통하는 회오리를 발사합니다";
                else
                {
                    explanation += AddExpl(8, 0) + AddExpl(8, 1) + AddExpl(8, 2);
                }
                UIName = "회오리";
                break;
            case "ElectricField":
                if (curLevel == 0)
                    explanation += "플레이어 주위로 전기장이 생깁니다";
                else
                {
                    explanation += AddExpl(9, 0) + AddExpl(9, 2);
                }
                UIName = "전기장";
                break;
            case "Thunder":
                if (curLevel == 0)
                    explanation += "근처 무작위 적에게 벼락이 떨어집니다";
                else
                {
                    explanation += AddExpl(10, 0) + AddExpl(10, 1) + AddExpl(10, 3);
                }
                UIName = "벼락";
                break;
            case "ThunderSpear":
                if (curLevel == 0)
                    explanation += "적에게 닿으면 폭발하는 전기창을 던집니다";
                else
                {
                    explanation += AddExpl(11, 0) + AddExpl(11, 1) + AddExpl(11, 2);
                }
                UIName = "번개창";
                break;


            case "IceMagic": // 얼음 기본공격
                explanation += "기본공격이 가까운 적들을 베는 얼음 검으로 변합니다";
                UIName = "얼음 마법사";
                break;
            case "FireMagic": // 불 기본공격
                explanation += "기본공격이 폭발하는 파이어볼로 변합니다";
                UIName = "불 마법사";
                break;
            case "WindMagic": // 바람 기본공격
                explanation += "기본 공격이 적을 관통하는 바람 칼날로 변합니다";
                UIName = "바람 마법사";
                break;
            case "ElecMagic": // 전기 기본공격
                explanation += "기본 공격이 적을 경직시키는 전기 구체로 변합니다";
                UIName = "전기 마법사";
                break;

        }
        return UIName;

    }

    private string AddExpl(int traitnum,int type)
    {
        
        switch (type)
        {
            case 0:
                text = "데미지 : " + traitAttack.traitInfo[traitnum].damage.ToString() + "->" + (traitAttack.traitInfo[traitnum].damage + damageValue).ToString() + "\n";
                break;
            case 1:
                text = "쿨타임 : " + traitAttack.traitInfo[traitnum].coolTime.ToString() + "초 -> " + 
                    ((float)((int)(traitAttack.traitInfo[traitnum].coolTime * 10)- (int)(coolTimeValue * 10))*0.1f).ToString() + "초" + "\n";
                break;
            case 2:
                text = "범위 : " + (traitAttack.traitInfo[traitnum].range + 100).ToString() + "%->" + (traitAttack.traitInfo[traitnum].range + rangeValue + 100).ToString() + "%" + "\n";
                break;
            case 3:
                text = "개수 : " + traitAttack.traitInfo[traitnum].num.ToString() + "->" + (traitAttack.traitInfo[traitnum].num + numValue).ToString() + "\n";
                break;
            case 4:
                text = "관통 수 : " + traitAttack.traitInfo[traitnum].pierce.ToString() + "->" + (traitAttack.traitInfo[traitnum].pierce + pierce).ToString() + "\n";
                break;
        }
        return text;
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
