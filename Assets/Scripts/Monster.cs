using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monster : MonoBehaviour
{
    [SerializeField]
    protected Player player;
    [SerializeField]
    protected NavMeshAgent agent;

    [SerializeField]
    protected float speed;
    public float hp;
    public float damage;
    public float maxAttackTime;
    private float curAttackTime = 0;

    protected virtual void Awake()
    {
        agent.speed = speed;
    }

    protected virtual void Update()
    {
        agent.SetDestination(player.transform.position);
        curAttackTime += Time.deltaTime;
    }

    public void GetDamage()
    {
        hp -= player.autoAttackDamage;
        Debug.Log(hp);
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
