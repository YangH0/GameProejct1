using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monster : MonoBehaviour
{
    protected Player player;
    protected TraitAttack traitAttack;
    [SerializeField]
    protected NavMeshAgent agent;

    [SerializeField]
    protected GameObject boneDummy;

    protected Animator anim;
    private BoxCollider col;

    [SerializeField]
    protected float speed;
    public float maxHp;
    protected float hp = 0;
    public float damage;
    private float getDamageMulti = 1;
    public float maxAttackTime;
    //protected float curAttackTime = 0;
    public float exp;

    private bool bIsCoolTime;

    private Vector3 dummyPosition;
    private Vector3 dummyRotation;


    //디버프 수치

    protected virtual void Awake()
    {
        agent.speed = speed;
        hp = maxHp;
        anim = GetComponent<Animator>();
        col = GetComponent<BoxCollider>();
        player = FindObjectOfType<Player>();
        traitAttack = FindObjectOfType<TraitAttack>();

        dummyPosition = boneDummy.transform.localPosition;
        dummyRotation = boneDummy.transform.rotation.eulerAngles;

    }

    protected virtual void OnEnable()
    {
        SpawnSetting();
    }
    
    protected virtual void Update()
    {
        agent.SetDestination(player.transform.position);
        //curAttackTime += Time.deltaTime;
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
            if (bIsCoolTime)
                return;
            StartCoroutine(AttackCoolTime());
            player.GetDamage(damage);
            //curAttackTime = 0;
        }
    }

    private IEnumerator AttackCoolTime()
    {
        bIsCoolTime = true;
        yield return new WaitForSeconds(maxAttackTime);
        bIsCoolTime = false;
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
        
        gameObject.SetActive(true);
        agent.isStopped = false;
        col.enabled = true;
        hp = maxHp;
    }

    protected virtual IEnumerator DieSetting()
    {
        
        agent.isStopped = true;
        traitAttack.DeleteMonster(this.gameObject);
        //anim.SetBool("bIsDie", true);
        col.enabled = false;
        yield return new WaitForSeconds(2f);
        boneDummy.gameObject.transform.rotation = Quaternion.Euler(dummyRotation);
        boneDummy.gameObject.transform.localPosition = dummyPosition;
        //anim.SetBool("bIsDie", false);
        gameObject.SetActive(false);
    }

    private IEnumerator IceDebuff()
    {
        agent.speed = speed;
        agent.speed *= 0.3f;
        Debug.Log("얼음 디버프--슬로우");
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
        Debug.Log("화상 데미지!!"+hp);
        yield return new WaitForSeconds(1f);
        StartCoroutine(FireDamage());
    }
    private IEnumerator WindDebuff()
    {
        Debug.Log("바람 디버프 -- 추가데미지");
        getDamageMulti = 1.3f;
        yield return new WaitForSeconds(2f);
        getDamageMulti = 1;
    }
    private IEnumerator ElecDebuff()
    {
        Debug.Log("전기 디버프 -- 경직");
        agent.speed = speed;
        agent.speed *= 0;
        yield return new WaitForSeconds(0.5f);
        agent.speed = speed;
    }

}
