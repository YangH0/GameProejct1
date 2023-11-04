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

    public GameObject pattern3_HitRange;
    public GameObject pattern3_HitCollider;


    private Vector3 targetPosition;

    private bool bIsPattern = false;

    public float pattern1_Damage;
    public float pattern2_WalkSpeed;
    public float pattern2_RunSpeed;
    public float pattern3_Damage;

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
        StopCoroutine(Pattern_3());
        StopCoroutine(SetPattern());
        pattern1_HitRange.SetActive(false);

        pattern2_HitRange.SetActive(false);
        pattern2_HitCollider.SetActive(false);

        pattern3_HitRange.SetActive(false);
        pattern3_HitCollider.SetActive(false);
    }

    private IEnumerator SetPattern()
    {
        if (hp <= 0)
            StopCoroutine(SetPattern());
        curPattern = 0;
        agent.speed = speed;
        agent.acceleration = 8f;
        agent.angularSpeed = 300;
        yield return new WaitForSeconds(5f);
        int num = Random.Range(1, 4);
        //num = 3;
        switch (num)
        {
            case 1:
                StartCoroutine(Pattern_1());
                break;
            case 2:
                StartCoroutine(Pattern_2());
                break;
            case 3:
                StartCoroutine(Pattern_3());
                break;
        }
    }

    private IEnumerator Pattern_1()
    {
        // Throw 패턴 시작
        curPattern = 1;
        anim.SetInteger("Pattern", curPattern);
        agent.speed = 0;
        agent.isStopped = true;
        agent.acceleration = 8f;

        yield return new WaitForSeconds(0.5f);
        GameObject obj = Instantiate(pattern1_Stone, pattern1_pos.transform.position,
            Quaternion.LookRotation(player.transform.position - transform.position));

        yield return new WaitForSeconds(1.0f);
        agent.isStopped = false;
        anim.SetInteger("Pattern", 0);
        StartCoroutine(SetPattern());
    }
    private IEnumerator Pattern_2()
    {
        Debug.Log("Rush_Ready");
        curPattern = 2;
        anim.SetInteger("Pattern", curPattern);
        agent.speed = 0;
        agent.angularSpeed = 0;
        agent.isStopped = true;
        pattern2_HitRange.SetActive(true);
        pattern2_HitRange.transform.rotation = Quaternion.LookRotation(player.transform.position - gameObject.transform.position);
        pattern2_HitRange.transform.rotation = Quaternion.Euler(new Vector3(0, transform.rotation.eulerAngles.y, 0));
        pattern2_HitRange.transform.position = new Vector3(transform.position.x + ((player.transform.position.x - transform.position.x) / 2),
                                                            1,
                                                            transform.position.z + ((player.transform.position.z - transform.position.z) / 2));
        pattern2_HitRange.transform.localScale = new Vector3(2, 0.8f, (player.transform.position - gameObject.transform.position).magnitude * 1.4f);
        targetPosition = transform.position + (player.transform.position - transform.position) * 1.05f;

        yield return new WaitForSeconds(1f);
        Debug.Log("Rush_Start");
        agent.isStopped = false;
        bIsPattern = true;
        pattern2_HitCollider.SetActive(true);
        pattern2_HitRange.SetActive(false);
        agent.SetDestination(targetPosition);
        agent.speed = 10f;
        agent.acceleration = 3f;

        yield return new WaitForSeconds(5f);
        bIsPattern = false;
        pattern2_HitCollider.SetActive(false);
        anim.SetInteger("Pattern", 0);
        StartCoroutine(SetPattern());
    }
    private IEnumerator Pattern_3()
    {
        curPattern = 3; // 준비 단계
        anim.SetInteger("Pattern", curPattern);
        agent.isStopped = true;
        float tmprun = player.runSpeed;
        float tmpwalk = player.walkSpeed;
        player.runSpeed = pattern2_RunSpeed;
        player.walkSpeed = pattern2_WalkSpeed;
        yield return new WaitForSeconds(5f);
        player.runSpeed = tmprun;
        player.walkSpeed = tmpwalk;
        agent.isStopped = false;
        anim.SetInteger("Pattern", 0);
        StartCoroutine(SetPattern());

        yield break;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Player player = other.GetComponent<Player>();
            switch (curPattern)
            {
                case 1:
                    player.GetDamage(pattern1_Damage);
                    break;
                case 2:
                    break;
                case 3:
                    player.GetDamage(pattern3_Damage);
                    break;
            }

        }
    }

}
