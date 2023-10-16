using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monster : MonoBehaviour
{
    [SerializeField]
    protected Player player;
    [SerializeField]
    private TraitAttack traitAttack;
    [SerializeField]
    protected NavMeshAgent agent;

    private Animator anim;
    private BoxCollider col;

    [SerializeField]
    protected float speed;
    public float maxHp;
    private float hp = 0;
    public float damage;
    private float getDamageMulti = 1;
    public float maxAttackTime;
    protected float curAttackTime = 0;
    public float exp;

    //디버프 수치

    protected virtual void Awake()
    {
        agent.speed = speed;
        hp = maxHp;
        anim = GetComponent<Animator>();
        col = GetComponent<BoxCollider>();
    }

    protected virtual void Update()
    {
        agent.SetDestination(player.transform.position);
        curAttackTime += Time.deltaTime;
    }

    public void GetDamage(float damage, int debuffType = 2)
    {
        hp -= damage * getDamageMulti;
        //Debug.Log(gameObject.name + " Hit!!  " + "HP:" + hp);
        Debuff(debuffType);
        if (hp <= 0)
            Die();
    }

    protected void Die()
    {
        player.GetExp(exp);
        StartCoroutine(DieSetting());
    }

    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if (curAttackTime < maxAttackTime)
                return;
            player.GetDamage(damage);
            curAttackTime = 0;
        }
    }

    private void Debuff(int debuffType)
    {
        switch (debuffType) // 1 - Ice   2 - Fire   3 - Wind  4 - Elec
        {
            case 1:
                StopCoroutine(IceDebuff());
                StartCoroutine(IceDebuff());
                break;
            case 2:
                StopCoroutine(FireDebuff());
                StopCoroutine(FireDamage());
                StartCoroutine(FireDebuff());
                break;
            case 3:
                StopCoroutine(WindDebuff());
                StartCoroutine(WindDebuff());
                break;
            case 4:
                StopCoroutine(ElecDebuff());
                StartCoroutine(ElecDebuff());
                break;

        }

    }

    protected virtual void SpawnSetting()
    {
        anim.SetBool("bIsDie", false);
        col.enabled = true;
        agent.speed = speed;
        gameObject.SetActive(true);
    }

    protected virtual IEnumerator DieSetting()
    {
        traitAttack.DeleteMonster(this.gameObject);
        anim.SetBool("bIsDie", true);
        agent.speed = 0;
        col.enabled = false;
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
    }

    private IEnumerator IceDebuff()
    {
        agent.speed = speed;
        agent.speed *= 0.3f;
        //Debug.Log("얼음 디버프--슬로우");
        yield return new WaitForSeconds(3f);
        agent.speed = speed;
    }
    private IEnumerator FireDebuff()
    {
        StartCoroutine(FireDamage());
        yield return new WaitForSeconds(2f);
        StopCoroutine(FireDamage());
    }
    private IEnumerator FireDamage()
    {
        hp -= 2;
        if (hp <= 0)
            Die();
        //Debug.Log("화상 데미지!!"+hp);
        yield return new WaitForSeconds(1f);
        StartCoroutine(FireDamage());
    }
    private IEnumerator WindDebuff()
    {
        //Debug.Log("바람 디버프 -- 추가데미지");
        getDamageMulti = 1.3f;
        yield return new WaitForSeconds(2f);
        getDamageMulti = 1;
    }
    private IEnumerator ElecDebuff()
    {
        //Debug.Log("전기 디버프 -- 경직");
        agent.speed = speed;
        agent.speed *= 0;
        yield return new WaitForSeconds(0.5f);
        agent.speed = speed;
    }

}
