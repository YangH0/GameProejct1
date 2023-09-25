using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraitAttack : MonoBehaviour
{
    public float ManaDamage;
    public float testDamage;
    public float testCoolTime;
    public float test2Damage;
    public float test2CoolTime;

    private Vector3 nearVector;
    private GameObject nearObject;

    [SerializeField] private GameObject trait1;
    [SerializeField] private GameObject trait2;
    [SerializeField] private TraitData[] traitData;

    public List<GameObject> monsters = new List<GameObject>();
    List<GameObject> traitList1 = new List<GameObject>();
    List<GameObject> traitList2 = new List<GameObject>();


    public IEnumerator CTestAttack1()
    {
        FindNearlest();

        if (nearObject != null)
        {
            GameObject newObj = null;
            for (int i = 0; i < traitList1.Count; i++)
            {
                if (!traitList1[i].activeSelf)
                {
                    traitList1[i].SetActive(true);
                    newObj = traitList1[i];
                    break;
                }
            }
            if (newObj == null)
            {
                newObj = Instantiate(trait1);
                traitList1.Add(newObj);
            }

            newObj.transform.position = transform.position;
            newObj.transform.rotation = Quaternion.LookRotation(nearObject.transform.position - transform.position);

            Bullet bul = newObj.GetComponent<Bullet>();
            bul.damage = testDamage;
        }
            yield return new WaitForSeconds(testCoolTime);
            StartCoroutine(CTestAttack1());
    }

    public IEnumerator CTestAttack2()
    {
        FindNearlest();

        if (nearObject != null)
        {
            GameObject newObj = null;
            for (int i = 0; i < traitList2.Count; i++)
            {
                if (!traitList2[i].activeSelf)
                {
                    traitList2[i].SetActive(true);
                    newObj = traitList2[i];
                    break;
                }
            }
            if (newObj == null)
            {
                newObj = Instantiate(trait2);
                traitList2.Add(newObj);
            }

            newObj.transform.position = new Vector3(nearObject.transform.position.x,1, nearObject.transform.position.z);
            //newObj.transform.rotation = Quaternion.LookRotation(nearObject.transform.position - transform.position);

            RangeTrait bul = newObj.GetComponent<RangeTrait>();
            bul.damage = test2Damage;
        }
            yield return new WaitForSeconds(test2CoolTime);
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

    public float TraitDamage(string traitname)
    {
        switch (traitname)
        {
            case "Test1":
                return testDamage;
            case "Test2":
                return test2Damage;
        }
        return 0;
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
