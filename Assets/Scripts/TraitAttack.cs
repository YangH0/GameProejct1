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


    public IEnumerator CTestAttack1()
    {
        FindNearlest();

        if (nearObject != null)
        {
            Instantiate(trait1, transform.position, Quaternion.LookRotation(nearObject.transform.position - transform.position));
        }
            yield return new WaitForSeconds(testCoolTime);
            StartCoroutine(CTestAttack1());
    }

    public IEnumerator CTestAttack2()
    {
        FindNearlest();

        if (nearObject != null)
        {
            Instantiate(trait2, transform.position, Quaternion.LookRotation(nearObject.transform.position - transform.position));
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
