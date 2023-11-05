using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPlatypus : Monster
{
    public GameObject pattern1_HitRange;
    public GameObject pattern1_Stone;
    public GameObject pattern1_pos;

    public GameObject pattern2_HitRange;
    public GameObject pattern2_HitCollider;


    private Vector3 targetPosition;

    private bool bIsPattern = false;

    public float pattern1_Damage;
    public float pattern2_Damage;

    private int curPattern = 0;

    protected override void Awake()
    {
        base.Awake();
    }
    protected override void Update()
    {
        //curAttackTime += Time.deltaTime;

        if (bIsPattern)
            return;

        agent.SetDestination(player.transform.position);
        RotateToPlayer();
    }

    protected override void SpawnSetting()
    {
        base.SpawnSetting();
        StartCoroutine(SetPattern());

    }

    public void StopPattern()
    {
        Debug.Log("Stop!!!!!!!!!");
        StopCoroutine(Pattern_1());
        StopCoroutine(Pattern_2());
        StopCoroutine(SetPattern());
        pattern1_HitRange.SetActive(false);

        pattern2_HitRange.SetActive(false);
        pattern2_HitCollider.SetActive(false);
    }

    private IEnumerator SetPattern()
    {
        if (hp <= 0)
            StopCoroutine(SetPattern());
        curPattern = 0;
        agent.speed = speed;
        agent.acceleration = 8f;
        yield return new WaitForSeconds(2f);
        int num = Random.Range(1, 3);
        //num = 2;
        switch (num)
        {
            case 1:
                StartCoroutine(Pattern_1());
                break;
            case 2:
                StartCoroutine(Pattern_2());
                break;
        }
    }

    private IEnumerator Pattern_1()
    {
        // Throw 패턴 시작
        curPattern = 1;
        anim.SetInteger("Pattern", curPattern);

        yield return new WaitForSeconds(0.5f);
        GameObject obj = Instantiate(pattern1_Stone, pattern1_pos.transform.position,
            Quaternion.LookRotation(player.transform.position - transform.position));

        yield return new WaitForSeconds(1.0f);
        anim.SetInteger("Pattern", 0);
        StartCoroutine(SetPattern());
    }
    private IEnumerator Pattern_2()
    {
        bIsPattern = true;
        curPattern = 2;
        anim.SetInteger("Pattern", curPattern);
        pattern2_HitRange.SetActive(true);
        targetPosition = new Vector3(player.transform.position.x, pattern2_HitRange.transform.position.y, player.transform.position.z);
        pattern2_HitRange.transform.position = targetPosition;

        yield return new WaitForSeconds(0.7f);
        pattern2_HitRange.SetActive(false);
        pattern2_HitCollider.SetActive(true);
        transform.position = new Vector3(targetPosition.x, transform.position.y, targetPosition.z);
        yield return new WaitForSeconds(0.1f);
        pattern2_HitCollider.SetActive(false);
        yield return new WaitForSeconds(1.5f);
        bIsPattern = false;
        anim.SetInteger("Pattern", 0);
        StartCoroutine(SetPattern());
    }

    private void RotateToPlayer()
    {
        Vector3 target = (player.transform.position - gameObject.transform.position).normalized;
        transform.rotation = Quaternion.LookRotation(new Vector3(target.x, 0, target.z));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Player player = other.GetComponent<Player>();
            switch (curPattern)
            {
                case 2:
                    player.GetDamage(pattern2_Damage);
                    break;
            }

        }
    }

}
