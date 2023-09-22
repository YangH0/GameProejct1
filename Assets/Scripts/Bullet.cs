using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;

    void FixedUpdate()
    {
        transform.Translate(Vector3.forward* speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Monster")
        {
            Monster monster = other.GetComponent<Monster>();
            monster.GetDamage();
            
            Destroy(this.gameObject);
        }
        else if (other.gameObject.tag == "Tile" || other.gameObject.tag == "Obstacle")
        {
            Destroy(this.gameObject);
        }
    }
}
