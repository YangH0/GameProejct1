using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public float damage;
    public int debuffType =0;

    [SerializeField] GameObject bullet;
    [SerializeField] GameObject hit;

    private void OnEnable()
    {
        hit.gameObject.SetActive(false);
        bullet.SetActive(true);
    }

    void FixedUpdate()
    {
        transform.Translate(Vector3.forward* speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Monster")
        {
            Monster monster = other.GetComponent<Monster>();
            monster.GetDamage(damage, debuffType);

            StartCoroutine(hitFX());
        }
        else if (other.gameObject.tag == "Tile" || other.gameObject.tag == "Obstacle")
        {
            StartCoroutine(hitFX());
        }
    }

    private IEnumerator hitFX()
    {
        bullet.SetActive(false);
        hit.SetActive(true);
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
    }
}
