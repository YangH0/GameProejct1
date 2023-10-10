using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosive : MonoBehaviour
{
    public float damage;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Monster")
        {
            Monster monster = other.GetComponent<Monster>();
            monster.GetDamage(damage);
        }
    }
}
