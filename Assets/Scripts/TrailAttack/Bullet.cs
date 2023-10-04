using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] Player player;
    public float speed;
    public float damage;

    void FixedUpdate()
    {
        transform.Translate(Vector3.forward* speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Monster")
        {
            Monster monster = other.GetComponent<Monster>();
            monster.GetDamage(damage);

            gameObject.SetActive(false);
        }
        else if (other.gameObject.tag == "Tile" || other.gameObject.tag == "Obstacle")
        {
            gameObject.SetActive(false);
        }
    }
}
