using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBullet : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] Explosive explosive;
    public float speed;
    public float damage;
    public float range;
    public bool bIsHit;
    public int debuffType;

    private Vector3 defaultRange;

    private void Awake()
    {
        bullet.SetActive(true);
        explosive.gameObject.SetActive(false);
        bIsHit = false;

        defaultRange = explosive.gameObject.transform.localScale;
    }

    private void OnEnable()
    {
        bullet.SetActive(true);
        explosive.gameObject.SetActive(false);
        bIsHit = false;

        AudioSource audioSource = GetComponent<AudioSource>();
        if (audioSource.clip != null)
            audioSource.Play();

    }

    public void UpdateScale()
    {
        explosive.gameObject.transform.localScale = (defaultRange * (range * 0.01f + 1));

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
                explosive.damage = damage;
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
