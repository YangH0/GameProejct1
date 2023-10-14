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
                                        //얼음 오브젝트
    [SerializeField] private GameObject o_IceBomb;
    [SerializeField] private GameObject o_IceSpear;
                                        //불 오브젝트
    [SerializeField] private GameObject o_FireWall;
    [SerializeField] private GameObject o_Meteor;
                                        //바람 오브젝트
    [SerializeField] private GameObject o_Wisp;
    [SerializeField] private GameObject o_Tornado;
                                        //전기 오브젝트
    [SerializeField] private GameObject o_Thunder;
    

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

    ////////////////
    // 얼음 스킬 //
    public IEnumerator IceBomb() // 얼음 폭탄
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
                newObj = Instantiate(o_IceBomb);
                traitInfo[0].traitList.Add(newObj);
            }

            newObj.transform.position = transform.position;
            newObj.transform.rotation = Quaternion.LookRotation(nearObject.transform.position - transform.position);

            ExplosiveBullet bul = newObj.GetComponent<ExplosiveBullet>();
            bul.damage = traitInfo[0].damage;
        }
        yield return new WaitForSeconds(traitInfo[0].coolTime);
        StartCoroutine(IceBomb());
    }
    public IEnumerator IceSpear() // 얼음 창
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
                newObj = Instantiate(o_IceSpear);
                traitInfo[1].traitList.Add(newObj);
            }

            newObj.transform.position = transform.position;
            newObj.transform.rotation = Quaternion.LookRotation(nearObject.transform.position - transform.position);

            PiercingBullet bul = newObj.GetComponent<PiercingBullet>();
            bul.damage = traitInfo[1].damage;
            bul.pierce = traitInfo[1].pierce;
        }
        yield return new WaitForSeconds(traitInfo[1].coolTime);
        StartCoroutine(IceSpear());
    }
    
    ////////////////
    // 화염 스킬 //
    public IEnumerator FireWall() //불기둥
    {
        FindNearlest();

        if (monsters.Count != 0)
        {
            GameObject newObj = null;
            for (int i = 0; i < traitInfo[3].traitList.Count; i++)
            {
                if (!traitInfo[3].traitList[i].activeSelf)
                {
                    traitInfo[3].traitList[i].SetActive(true);
                    newObj = traitInfo[3].traitList[i];
                    break;
                }
            }
            if (newObj == null)
            {
                newObj = Instantiate(o_FireWall);
                traitInfo[3].traitList.Add(newObj);
            }

            newObj.transform.position = new Vector3(nearObject.transform.position.x, 1, nearObject.transform.position.z);
            //newObj.transform.rotation = Quaternion.LookRotation(nearObject.transform.position - transform.position);

            RangeTrait bul = newObj.GetComponent<RangeTrait>();
            bul.damage = traitInfo[3].damage;
        }
        yield return new WaitForSeconds(traitInfo[3].coolTime);
        StartCoroutine(FireWall());
    }
    public IEnumerator Meteor() // 유성우
    {
        FindNearlest();

        if (monsters.Count != 0)
        {
            GameObject newObj = null;
            for (int i = 0; i < traitInfo[4].traitList.Count; i++)
            {
                if (!traitInfo[4].traitList[i].activeSelf)
                {
                    traitInfo[4].traitList[i].SetActive(true);
                    newObj = traitInfo[4].traitList[i];
                    break;
                }
            }
            if (newObj == null)
            {
                newObj = Instantiate(o_Meteor);
                traitInfo[4].traitList.Add(newObj);
            }

            newObj.transform.position = new Vector3(transform.position.x, transform.position.y + 15, transform.position.z);
            newObj.transform.rotation = Quaternion.LookRotation(nearObject.transform.position - newObj.transform.position);

            ExplosiveBullet bul = newObj.GetComponent<ExplosiveBullet>();
            bul.damage = traitInfo[4].damage;
        }
        yield return new WaitForSeconds(traitInfo[4].coolTime);
        StartCoroutine(Meteor());
    }
    ////////////////
    // 바람 스킬 //
    public IEnumerator Wisp() // 위습
    {
        FindNearlest();

        if (monsters.Count != 0)
        {
            GameObject newObj = null;
            for (int i = 0; i < traitInfo[6].traitList.Count; i++)
            {
                if (!traitInfo[6].traitList[i].activeSelf)
                {
                    traitInfo[6].traitList[i].SetActive(true);
                    newObj = traitInfo[6].traitList[i];
                    break;
                }
            }
            if (newObj == null)
            {
                newObj = Instantiate(o_Wisp);
                traitInfo[6].traitList.Add(newObj);
            }

            newObj.transform.position = transform.position;
            newObj.transform.rotation = Quaternion.LookRotation(nearObject.transform.position - transform.position);

            Bullet bul = newObj.GetComponent<Bullet>();
            bul.damage = traitInfo[6].damage;
        }
            yield return new WaitForSeconds(traitInfo[6].coolTime);
            StartCoroutine(Wisp());
    }
    public IEnumerator Tornado() // 회오리
    {
        FindNearlest();

        if (monsters.Count != 0)
        {
            GameObject newObj = null;
            for (int i = 0; i < traitInfo[8].traitList.Count; i++)
            {
                if (!traitInfo[8].traitList[i].activeSelf)
                {
                    traitInfo[8].traitList[i].SetActive(true);
                    newObj = traitInfo[8].traitList[i];
                    break;
                }
            }
            if (newObj == null)
            {
                newObj = Instantiate(o_Tornado);
                traitInfo[8].traitList.Add(newObj);
            }

            newObj.transform.position = transform.position;
            newObj.transform.rotation = Quaternion.LookRotation(nearObject.transform.position - transform.position);

            PiercingBullet bul = newObj.GetComponent<PiercingBullet>();
            bul.damage = traitInfo[8].damage;
            bul.pierce = traitInfo[8].pierce;
        }
        yield return new WaitForSeconds(traitInfo[8].coolTime);
        StartCoroutine(Tornado());
    }
    ////////////////
    // 전기 스킬 //
    public IEnumerator Thunder() //벼락
    {
        FindNearlest();

        if (monsters.Count != 0)
        {
            GameObject newObj = null;
            for (int i = 0; i < traitInfo[10].traitList.Count; i++)
            {
                if (!traitInfo[10].traitList[i].activeSelf)
                {
                    traitInfo[10].traitList[i].SetActive(true);
                    newObj = traitInfo[10].traitList[i];
                    break;
                }
            }
            if (newObj == null)
            {
                newObj = Instantiate(o_Thunder);
                traitInfo[10].traitList.Add(newObj);
            }

            newObj.transform.position = new Vector3(nearObject.transform.position.x, 5, nearObject.transform.position.z);
            //newObj.transform.rotation = Quaternion.LookRotation(nearObject.transform.position - transform.position);

            RangeTrait bul = newObj.GetComponent<RangeTrait>();
            bul.damage = traitInfo[10].damage;
        }
        yield return new WaitForSeconds(traitInfo[10].coolTime);
        StartCoroutine(Thunder());
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
