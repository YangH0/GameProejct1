using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBullet : MonoBehaviour
{
    public float damage = 20;
    public float speed;

    void FixedUpdate()
    {
        transform.Translate(Vector3.forward * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Player player = other.GetComponent<Player>();
            player.GetDamage(damage);

            Destroy(this.gameObject);
        }
        else if(other.gameObject.tag == "Tile" || other.gameObject.tag == "Obstacle")
        {
            Destroy(this.gameObject);
        }
    }

}