using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public struct TraitStruct
{
    public float damage;
    public float coolTime;
    public float range;
    public int num;
    public int pierce;
    public List<GameObject> traitList;
    public int uiNum;
}

public class TraitAttack : MonoBehaviour
{
    public int traitNum;

    private Vector3 nearVector;
    private GameObject nearObject;
                                        //얼음 오브젝트
    [SerializeField] private GameObject o_IceBomb;
    [SerializeField] private GameObject o_IceSpear;
    [SerializeField] private GameObject o_IceField;
                                        //불 오브젝트
    [SerializeField] private GameObject o_FireWall;
    [SerializeField] private GameObject o_Meteor;
    [SerializeField] private GameObject o_FireField;
                                        //바람 오브젝트
    [SerializeField] private GameObject o_Wisp;
    [SerializeField] private GameObject o_Shield_NULL;
    [SerializeField] private GameObject o_Tornado;
                                        //전기 오브젝트
    [SerializeField] private GameObject o_ElectricField;
    [SerializeField] private GameObject o_Thunder;
    [SerializeField] private GameObject o_ThunderSpear;

    [SerializeField] private GameObject[] WispObj;

    [SerializeField] private float[] default_Damage;
    [SerializeField] private float[] default_CoolTime;
    [SerializeField] private float[] default_Range;
    [SerializeField] private int[] default_Num;
    [SerializeField] private int[] default_Pierce;

    public TextMeshProUGUI[] coolTimeUI;

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
            traitInfo[i].damage = default_Damage[i];
            traitInfo[i].coolTime = default_CoolTime[i];
            traitInfo[i].range = default_Range[i];
            traitInfo[i].num = default_Num[i];
            traitInfo[i].pierce = default_Pierce[i];
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
            bul.range = traitInfo[0].range;
            bul.UpdateScale();

        }
        StartCoroutine(CoolTimeStart(0));
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
        StartCoroutine(CoolTimeStart(1));
        yield return new WaitForSeconds(traitInfo[1].coolTime);
        StartCoroutine(IceSpear());
    }
    public IEnumerator IceField() // 냉기 이동
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
                newObj = Instantiate(o_IceField);
                traitInfo[2].traitList.Add(newObj);
            }

            newObj.transform.position = transform.position;
            //newObj.transform.rotation = Quaternion.LookRotation(nearObject.transform.position - transform.position);

            RangeTrait bul = newObj.GetComponent<RangeTrait>();
            bul.damage = traitInfo[2].damage;
        }
        StartCoroutine(CoolTimeStart(2));
        yield return new WaitForSeconds(traitInfo[2].coolTime);
        StartCoroutine(IceField());
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
            bul.range = traitInfo[3].range;
            bul.UpdateScale();
        }
        StartCoroutine(CoolTimeStart(3));
        yield return new WaitForSeconds(traitInfo[3].coolTime);
        StartCoroutine(FireWall());
    }
    public IEnumerator Meteor() // 유성우
    {
        FindNearlest();

        if (monsters.Count != 0)
        {
            for(int i = 0; i < traitInfo[4].num; i++)
            {
                GameObject newObj = null;
                for (int k = 0; k < traitInfo[4].traitList.Count; k++)
                {
                    if (!traitInfo[4].traitList[k].activeSelf)
                    {
                        traitInfo[4].traitList[k].SetActive(true);
                        newObj = traitInfo[4].traitList[k];
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
                yield return new WaitForSeconds(0.2f);
            }

        }
        StartCoroutine(CoolTimeStart(4));
        yield return new WaitForSeconds(traitInfo[4].coolTime);
        StartCoroutine(Meteor());
    }
    public IEnumerator FireField() // 열기
    {
        o_FireField.SetActive(true);
        TraitField field = o_FireField.GetComponent<TraitField>();

        field.damage = traitInfo[5].damage;
        field.range = traitInfo[5].range;
        field.UpdateScale();

        yield break;
    }
    ////////////////
    // 바람 스킬 //
    public void SpawnWisp(int num)
    {
        WispObj[num].SetActive(true);
    }

    public IEnumerator Wisp() // 위습
    {
        for (int k = 0; k < traitInfo[6].num; k++)
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

                newObj.transform.position = WispObj[k].transform.position;
                newObj.transform.rotation = Quaternion.LookRotation(nearObject.transform.position - WispObj[k].transform.position);

                Bullet bul = newObj.GetComponent<Bullet>();
                bul.damage = traitInfo[6].damage;
                yield return new WaitForSeconds(0.1f);
            }
        }
        StartCoroutine(CoolTimeStart(6));
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

            newObj.transform.position = new Vector3(transform.position.x, 0.94f, transform.position.z);
            newObj.transform.rotation = Quaternion.LookRotation(nearObject.transform.position - transform.position);
            newObj.transform.rotation = Quaternion.Euler(new Vector3(0, newObj.transform.rotation.eulerAngles.y, 0));

            Tornado bul = newObj.GetComponent<Tornado>();
            bul.damage = traitInfo[8].damage;
            bul.range = traitInfo[8].range;
            bul.UpdateScale();
        }
        StartCoroutine(CoolTimeStart(8));
        yield return new WaitForSeconds(traitInfo[8].coolTime);
        StartCoroutine(Tornado());
    }
    ////////////////
    // 전기 스킬 //
    public IEnumerator ElectricField() // 전기장
    {
        o_ElectricField.SetActive(true);
        TraitField field = o_ElectricField.GetComponent<TraitField>();

        field.damage = traitInfo[9].damage;
        field.range = traitInfo[9].range;
        field.UpdateScale();

        yield break;
    }

    public IEnumerator Thunder() //벼락
    {
        for (int k = 0; k < traitInfo[10].num; k++) 
        {
            if (monsters.Count != 0)
            {
                GameObject newObj = null;

                int randomNum = Random.Range(0, monsters.Count);
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

                newObj.transform.position = new Vector3(monsters[randomNum].transform.position.x, 1, monsters[randomNum].transform.position.z);
                //newObj.transform.rotation = Quaternion.LookRotation(nearObject.transform.position - transform.position);

                RangeTrait bul = newObj.GetComponent<RangeTrait>();
                bul.damage = traitInfo[10].damage;
                yield return new WaitForSeconds(0.2f);
            }
        }
        StartCoroutine(CoolTimeStart(10));
        yield return new WaitForSeconds(traitInfo[10].coolTime);
        StartCoroutine(Thunder());
    }
    public IEnumerator ThunderSpear() // 전기창
    {
        FindNearlest();

        if (monsters.Count != 0)
        {
            GameObject newObj = null;
            for (int i = 0; i < traitInfo[11].traitList.Count; i++)
            {
                if (!traitInfo[11].traitList[i].activeSelf)
                {
                    traitInfo[11].traitList[i].SetActive(true);
                    newObj = traitInfo[11].traitList[i];
                    break;
                }
            }
            if (newObj == null)
            {
                newObj = Instantiate(o_ThunderSpear);
                traitInfo[11].traitList.Add(newObj);
            }

            newObj.transform.position = transform.position;
            newObj.transform.rotation = Quaternion.LookRotation(nearObject.transform.position - transform.position);

            ExplosiveBullet bul = newObj.GetComponent<ExplosiveBullet>();
            bul.damage = traitInfo[11].damage;
            bul.range = traitInfo[11].range;
        }
        StartCoroutine(CoolTimeStart(11));
        yield return new WaitForSeconds(traitInfo[11].coolTime);
        StartCoroutine(ThunderSpear());
    }

    private IEnumerator CoolTimeStart(int num)
    {
        float remainingTime = traitInfo[num].coolTime;
        while (remainingTime > 0.5)
        {
            remainingTime -= Time.deltaTime;
            coolTimeUI[traitInfo[num].uiNum].text = Mathf.FloorToInt(remainingTime).ToString();
            yield return null;
        }

        yield break;
    }
    public IEnumerator ShieldCoolTimeStart(float time)
    {
        float remainingTime = time;
        while (remainingTime > 0.5)
        {
            remainingTime -= Time.deltaTime;
            coolTimeUI[traitInfo[7].uiNum].text = Mathf.FloorToInt(remainingTime).ToString();
            yield return null;
        }

        yield break;
    }

    public void SetCoolTimeUI(int p_traitNum, int p_count)
    {
        traitInfo[p_traitNum].uiNum = p_count;
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
