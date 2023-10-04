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

    [SerializeField]
    protected float speed;
    public float maxHp;
    private float hp = 0;
    public float damage;
    public float maxAttackTime;
    private float curAttackTime = 0;
    public float exp;

    protected virtual void Awake()
    {
        agent.speed = speed;
        hp = maxHp;
    }

    protected virtual void Update()
    {
        agent.SetDestination(player.transform.position);
        curAttackTime += Time.deltaTime;
        Debug.Log(traitAttack.ManaDamage);
    }

    public void GetDamage(float damage)
    {
        hp -= damage;
        Debug.Log(hp);
        if (hp <= 0)
            Die();
    }

    protected void Die()
    {
        player.GetExp(exp);
        //gameObject.transform.position = new Vector3(100, 100, 0);
        traitAttack.DeleteMonster(this.gameObject);
        gameObject.SetActive(false);
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

}
