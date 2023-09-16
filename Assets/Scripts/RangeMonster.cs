using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeMonster : Monster
{
    [SerializeField] GameObject bullet;

    private bool bInRange;

    public float maxRangeAttackTime;
    private float curRangeAttackTime = 0;

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Update()
    {
        base.Update();
        curRangeAttackTime += Time.deltaTime;
        RangeAttack();
    }

    private void RangeAttack()
    {
        if (curRangeAttackTime < maxRangeAttackTime || !bInRange)
            return;
        Instantiate(bullet, transform.position, Quaternion.LookRotation(player.transform.position - transform.position));
        curRangeAttackTime = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            bInRange = true;
            agent.speed = 0;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            bInRange = false;
            agent.speed = speed;
        }
    }
}
