using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosive : MonoBehaviour
{
    public int debuffType;
    public float damage = 5;

    private void OnEnable()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        if (audioSource.clip != null)
            audioSource.Play();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Monster")
        {
            Monster monster = other.GetComponent<Monster>();
            monster.GetDamage(damage, debuffType);
        }
    }
    
}
