using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeTrait : MonoBehaviour
{
    public float damage;
    public float range;
    public int debuffType;
    public float setFalseTime;

    private Vector3 defaultRange;

    private void Awake()
    {
        defaultRange = transform.localScale;
    }

    private void OnEnable()
    {
        StartCoroutine(SetFalse());
        Debug.Log((defaultRange * (range * 0.01f + 1)));
        gameObject.transform.localScale = (defaultRange * (range * 0.01f + 1));
    }

    public void UpdateScale()
    {
        gameObject.transform.localScale = (defaultRange * (range * 0.01f + 1));

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Monster")
        {
            Monster monster = other.GetComponent<Monster>();
            monster.GetDamage(damage, debuffType);
        }
    }

    IEnumerator SetFalse()
    {
        yield return new WaitForSeconds(setFalseTime);
        gameObject.SetActive(false);
    }
}
