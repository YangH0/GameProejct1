using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct TraitStruct
{
    public float damage;
    public float coolTime;
    public float range;
    public int pierce;
    public List<GameObject> traitList;
}

public class TraitAttack : MonoBehaviour
{
    public float ManaDamage;
    public int traitNum;

    private Vector3 nearVector;
    private GameObject nearObject;

    [SerializeField] private GameObject trait1;
    [SerializeField] private GameObject trait2;
    [SerializeField] private GameObject trait3;

    public List<GameObject> monsters = new List<GameObject>();

    public TraitStruct[] traitInfo;

    private void Awake()
    {
        traitInfo = new TraitStruct[traitNum];
        for(int i = 0; i < traitInfo.Length; i++)
        {
            traitInfo[i].traitList = new List<GameObject>();
        }
        for (int i = 0; i < traitInfo.Length; i++)
        {
            traitInfo[i].damage = 5;
            traitInfo[i].coolTime = 2;
            traitInfo[i].pierce = 2;
        }
    }

    public IEnumerator CTestAttack1() // 투사체 관통X 공격 샘플 (위습)
    {
        FindNearlest();

        if (monsters.Count != 0)
        {
            GameObject newObj = null;
            for (int i = 0; i < traitInfo[0].traitList.Count; i++)
            {
                if (!traitInfo[0].traitList[i].activeSelf)
                {
                    traitInfo[0].traitList[i].SetActive(true);
                    newObj = traitInfo[0].traitList[i];
                    break;
                }
            }
            if (newObj == null)
            {
                newObj = Instantiate(trait1);
                traitInfo[0].traitList.Add(newObj);
            }

            newObj.transform.position = transform.position;
            newObj.transform.rotation = Quaternion.LookRotation(nearObject.transform.position - transform.position);

            Bullet bul = newObj.GetComponent<Bullet>();
            bul.damage = traitInfo[0].damage;
        }
            yield return new WaitForSeconds(traitInfo[0].coolTime);
            StartCoroutine(CTestAttack1());
    }

    public IEnumerator CTestAttack3() // 투사체 관통X 공격 샘플 (위습)
    {
        FindNearlest();

        if (monsters.Count != 0)
        {
            GameObject newObj = null;
            for (int i = 0; i < traitInfo[2].traitList.Count; i++)
            {
                if (!traitInfo[2].traitList[i].activeSelf)
                {
                    traitInfo[2].traitList[i].SetActive(true);
                    newObj = traitInfo[2].traitList[i];
                    break;
                }
            }
            if (newObj == null)
            {
                newObj = Instantiate(trait3);
                traitInfo[2].traitList.Add(newObj);
            }

            newObj.transform.position = transform.position;
            newObj.transform.rotation = Quaternion.LookRotation(nearObject.transform.position - transform.position);

            PiercingBullet bul = newObj.GetComponent<PiercingBullet>();
            bul.damage = traitInfo[2].damage;
            bul.pierce = traitInfo[2].pierce;
        }
            yield return new WaitForSeconds(traitInfo[2].coolTime);
            StartCoroutine(CTestAttack3());
    }

    public IEnumerator CTestAttack2() // 범위 공격 샘플 (불기둥)
    {
        FindNearlest();
        
        if (monsters.Count != 0)
        {
            GameObject newObj = null;
            for (int i = 0; i < traitInfo[1].traitList.Count; i++)
            {
                if (!traitInfo[1].traitList[i].activeSelf)
                {
                    traitInfo[1].traitList[i].SetActive(true);
                    newObj = traitInfo[1].traitList[i];
                    break;
                }
            }
            if (newObj == null)
            {
                newObj = Instantiate(trait2);
                traitInfo[1].traitList.Add(newObj);
            }

            newObj.transform.position = new Vector3(nearObject.transform.position.x,1, nearObject.transform.position.z);
            //newObj.transform.rotation = Quaternion.LookRotation(nearObject.transform.position - transform.position);

            RangeTrait bul = newObj.GetComponent<RangeTrait>();
            bul.damage = traitInfo[1].damage;
        }
            yield return new WaitForSeconds(traitInfo[1].coolTime);
            StartCoroutine(CTestAttack2());
    }

    private void FindNearlest()
    {
        nearVector = new Vector3(100, 100, 100);
        nearObject = null;
        
        for (int i = 0; i < monsters.Count; i++)
        {
            if (Vector3.Distance(transform.position, monsters[i].transform.position) < Vector3.Distance(transform.position, nearVector))
            {
                nearVector = monsters[i].transform.position;
                nearObject = monsters[i];
            }
        }
    }

    public void DeleteMonster(GameObject obj)
    {
        monsters.Remove(obj);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Monster"))
            monsters.Add(other.gameObject);
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Monster"))
            monsters.Remove(other.gameObject);   
    }

}
