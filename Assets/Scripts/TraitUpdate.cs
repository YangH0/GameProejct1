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


            case "FireMagic": // �� �⺻����
                player.ChangeAutoAttack(1);
                break;
            case "IceMagic": // ���� �⺻����
                player.ChangeAutoAttack(2);
                break;
            case "WindMagic": // �ٶ� �⺻����
                player.ChangeAutoAttack(3);
                break;
            case "ElecMagic": // �ٶ� �⺻����
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
                explanation += "�⺻ ���ݷ��� �����մϴ�" + "\n" +
                player.autoAttackDamage.ToString() + "->" + (player.autoAttackDamage + damageValue).ToString();
                UIName = "���ݷ� ����";
                break;
            case "AttackSpeed":
                explanation += "�⺻ ���ݼӵ��� �����մϴ�" + "\n" +
                player.maxAttackTime.ToString() + "�� -> " + 
                ((float)((int)(player.maxAttackTime * 10) - (int)(coolTimeValue * 10)) * 0.1f).ToString() + "��";
                UIName = "���� �ӵ� ����";
                break;
            case "HpUp":
                explanation += "�ִ�ü���� �����մϴ�" + "\n" +
                player.maxHp.ToString() + "->" + (player.maxHp + damageValue).ToString();
                UIName = "ü�� ����";
                break;
            case "SpeedUp":
                explanation += "�̵��ӵ��� �����մϴ�" + "\n" +
                player.walkSpeed.ToString() + "->" + (player.walkSpeed + damageValue).ToString();
                UIName = "�̵� �ӵ� ����";
                break;
            case "ExpUp":
                explanation += "����ġ ȹ�淮�� �����մϴ�" + "\n" +
                player.expMulti.ToString() + "% -> " + (player.expMulti + damageValue).ToString() +"%";
                UIName = "����ġ ����";
                break;

            case "IceBomb":
                if (curLevel == 0)
                    explanation += "������ �����ϴ� ������ź�� ����� ������ �߻��մϴ�";
                else
                {
                    explanation += AddExpl(0, 0) + AddExpl(0, 1) + AddExpl(0, 2);
                }
                UIName = "������ź";
                break;
            case "IceSpear":
                if (curLevel == 0)
                    explanation += "����� ������ �����ϴ� ���� â�� �߻��մϴ�";
                else
                {
                    explanation += AddExpl(1, 0) + AddExpl(1, 1) + AddExpl(1, 4);
                }
                UIName = "����â";
                break;
            case "IceField":
                if (curLevel == 0)
                    explanation += "�̵��� �� ���� �������ϴ� ���� �Ȱ��� ����ϴ�";
                else
                {
                    explanation += AddExpl(2, 0) + AddExpl(2, 1);
                }
                UIName = "�ñ��̵�";
                break;
            case "FireWall":
                if (curLevel == 0)
                    explanation += "����� ���� �� �ؿ��� �ұ���� �����˴ϴ�";
                else
                {
                    explanation += AddExpl(3, 0) + AddExpl(3, 1) + AddExpl(3, 2);
                }
                UIName = "�ұ��";
                break;
            case "Meteor":
                if (curLevel == 0)
                    explanation += "����� ������ �����찡 �������ϴ�";
                else
                {
                    explanation += AddExpl(4, 0) + AddExpl(4, 1) + AddExpl(4, 3);
                }
                UIName = "������";
                break;
            case "FireField":
                if (curLevel == 0)
                    explanation += "�÷��̾� ������ �߰ſ� ���Ⱑ ����ϴ�";
                else
                {
                    explanation += AddExpl(5, 0) + AddExpl(5, 1) + AddExpl(5, 2);
                }
                UIName = "����";
                break;
            case "Wisp":
                if (curLevel == 0)
                    explanation += "����� ���� �����ϴ� ������ ����ϴ�";
                else
                {
                    explanation += AddExpl(6, 0) + "���� �� : " + curLevel.ToString() + "->" + (curLevel+1).ToString();
                }
                UIName = "����";
                break;
            case "Shield":
                if (curLevel == 0)
                    explanation += "���� ������ ���� ��ȣ���� ����ϴ�";
                else
                {
                    explanation += "����� ������ : " + player.shieldDamage.ToString() + "->" + (player.shieldDamage + damageValue).ToString() + "\n" +
                        "��Ÿ�� : " + player.shieldCoolTime.ToString() + "�� -> " +
                        ((float)((int)(player.shieldCoolTime * 10) - (int)(coolTimeValue * 10)) * 0.1f).ToString() + "��";
                }
                UIName = "��ȣ��";
                break;
            case "Tornado":
                if (curLevel == 0)
                    explanation += "���� �����ϴ� ȸ������ �߻��մϴ�";
                else
                {
                    explanation += AddExpl(8, 0) + AddExpl(8, 1) + AddExpl(8, 2);
                }
                UIName = "ȸ����";
                break;
            case "ElectricField":
                if (curLevel == 0)
                    explanation += "�÷��̾� ������ �������� ����ϴ�";
                else
                {
                    explanation += AddExpl(9, 0) + AddExpl(9, 2);
                }
                UIName = "������";
                break;
            case "Thunder":
                if (curLevel == 0)
                    explanation += "��ó ������ ������ ������ �������ϴ�";
                else
                {
                    explanation += AddExpl(10, 0) + AddExpl(10, 1) + AddExpl(10, 3);
                }
                UIName = "����";
                break;
            case "ThunderSpear":
                if (curLevel == 0)
                    explanation += "������ ������ �����ϴ� ����â�� �����ϴ�";
                else
                {
                    explanation += AddExpl(11, 0) + AddExpl(11, 1) + AddExpl(11, 2);
                }
                UIName = "����â";
                break;


            case "IceMagic": // ���� �⺻����
                explanation += "�⺻������ ����� ������ ���� ���� ������ ���մϴ�";
                UIName = "���� ������";
                break;
            case "FireMagic": // �� �⺻����
                explanation += "�⺻������ �����ϴ� ���̾�� ���մϴ�";
                UIName = "�� ������";
                break;
            case "WindMagic": // �ٶ� �⺻����
                explanation += "�⺻ ������ ���� �����ϴ� �ٶ� Į���� ���մϴ�";
                UIName = "�ٶ� ������";
                break;
            case "ElecMagic": // ���� �⺻����
                explanation += "�⺻ ������ ���� ������Ű�� ���� ��ü�� ���մϴ�";
                UIName = "���� ������";
                break;

        }
        return UIName;

    }

    private string AddExpl(int traitnum,int type)
    {
        
        switch (type)
        {
            case 0:
                text = "������ : " + traitAttack.traitInfo[traitnum].damage.ToString() + "->" + (traitAttack.traitInfo[traitnum].damage + damageValue).ToString() + "\n";
                break;
            case 1:
                text = "��Ÿ�� : " + traitAttack.traitInfo[traitnum].coolTime.ToString() + "�� -> " + 
                    ((float)((int)(traitAttack.traitInfo[traitnum].coolTime * 10)- (int)(coolTimeValue * 10))*0.1f).ToString() + "��" + "\n";
                break;
            case 2:
                text = "���� : " + (traitAttack.traitInfo[traitnum].range + 100).ToString() + "%->" + (traitAttack.traitInfo[traitnum].range + rangeValue + 100).ToString() + "%" + "\n";
                break;
            case 3:
                text = "���� : " + traitAttack.traitInfo[traitnum].num.ToString() + "->" + (traitAttack.traitInfo[traitnum].num + numValue).ToString() + "\n";
                break;
            case 4:
                text = "���� �� : " + traitAttack.traitInfo[traitnum].pierce.ToString() + "->" + (traitAttack.traitInfo[traitnum].pierce + pierce).ToString() + "\n";
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
