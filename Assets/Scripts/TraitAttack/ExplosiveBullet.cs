using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBullet : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] Explosive explosive;
    public float speed;
    public float damage;
    public bool bIsHit;
    public int debuffType;

    private void Start()
    {
        bullet.SetActive(true);
        explosive.gameObject.SetActive(false);
        bIsHit = false;
    }

    private void OnEnable()
    {
        bullet.SetActive(true);
        explosive.gameObject.SetActive(false);
        bIsHit = false;
    }

    void FixedUpdate()
    {
        if(!bIsHit)
            transform.Translate(Vector3.forward * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Monster"|| other.gameObject.tag == "Tile" || other.gameObject.tag == "Obstacle")
        {
            if(other.gameObject.tag == "Monster")
            {
                Monster monster = other.GetComponent<Monster>();
                monster.GetDamage(damage, debuffType);
            }
            if (!bIsHit)
            {
                bullet.SetActive(false);
                explosive.gameObject.SetActive(true);
                bIsHit = true;
                StartCoroutine(ActiveFalse());
            }
        }
    }

    private IEnumerator ActiveFalse()
    {
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
    }
}
