using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiercingBullet : MonoBehaviour
{
    public float speed;
    public float damage;
    public int pierce;
    private int count =0;
    public int debuffType;

    void FixedUpdate()
    {
        transform.Translate(Vector3.forward * speed);
    }

    private void OnEnable()
    {
        count = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Monster")
        {
            Monster monster = other.GetComponent<Monster>();
            monster.GetDamage(damage, debuffType);
            count++;
            if (count >= pierce)
                gameObject.SetActive(false);

        }
        else if (other.gameObject.tag == "Tile" || other.gameObject.tag == "Obstacle")
        {
            gameObject.SetActive(false);
        }
    }
}
