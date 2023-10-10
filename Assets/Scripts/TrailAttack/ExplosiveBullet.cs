using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBullet : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject particle;
    [SerializeField] Explosive explosive;
    public float speed;
    public float damage;
    public bool bIsHit;

    private void Start()
    {
        bullet.SetActive(true);
        particle.SetActive(false);
        bIsHit = false;
        explosive.damage = damage;
    }

    private void OnEnable()
    {
        bullet.SetActive(true);
        explosive.gameObject.SetActive(false);
        bIsHit = false;
        explosive.damage = damage;
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
