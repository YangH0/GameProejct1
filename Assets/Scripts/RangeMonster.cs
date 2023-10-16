using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeMonster : Monster
{
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject bulletPosition;
    [SerializeField] BoxCollider rangeCol;

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
        GameObject obj = Instantiate(bullet, bulletPosition.transform.position, 
            Quaternion.LookRotation(player.transform.position - transform.position));
        obj.transform.rotation = Quaternion.Euler(new Vector3(0, obj.transform.rotation.eulerAngles.y, 0));
        curRangeAttackTime = 0;
    }

    protected override void SpawnSetting()
    {
        rangeCol.enabled = true;
        base.SpawnSetting();
    }

    protected override IEnumerator DieSetting()
    {
        rangeCol.enabled = false;
        return base.DieSetting();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            bInRange = true;
            agent.speed = 0.3f;
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
