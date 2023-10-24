using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossUnicorn : Monster
{
    public GameObject pattern1_HitRange;
    public GameObject pattern1_Laser;

    public GameObject pattern2_HitRange;
    public GameObject pattern2_HitCollider;

    public GameObject pattern3_HitRange;
    public GameObject pattern3_HitRange2;
    public GameObject pattern3_HitRange3;
    public GameObject pattern3_HitCollider;


    private Vector3 targetPosition;
    private Vector3 targetPosition2;
    private Vector3 targetPosition3;

    private bool bIsPattern = false;

    public float pattern1_Damage;
    public float pattern2_Damage;
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
        pattern1_Laser.SetActive(false);

        pattern2_HitRange.SetActive(false);
        pattern2_HitCollider.SetActive(false);

        pattern3_HitRange.SetActive(false);
        pattern3_HitRange2.SetActive(false);
        pattern3_HitRange3.SetActive(false);
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
        yield return new WaitForSeconds(2f);
        int num = Random.Range(1, 4);
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
        curPattern = 1;
        anim.SetInteger("Pattern", curPattern);
        agent.speed = 0;
        agent.isStopped = true;
        agent.acceleration = 8f;
        Debug.Log("Laser_Ready");
        pattern1_HitRange.SetActive(true);

        yield return new WaitForSeconds(0.5f);

        Debug.Log("Laser_Start");
        pattern1_HitRange.SetActive(false);
        pattern1_Laser.SetActive(true);

        yield return new WaitForSeconds(3.0f);
        agent.isStopped = false;
        pattern1_Laser.SetActive(false);
        yield return new WaitForSeconds(0.5f);
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
        targetPosition =transform.position +  (player.transform.position-transform.position)*1.2f;

        yield return new WaitForSeconds(1f);
        Debug.Log("Rush_Start");
        agent.isStopped = false;
        bIsPattern = true;
        pattern2_HitCollider.SetActive(true);
        pattern2_HitRange.SetActive(false);
        agent.SetDestination(targetPosition);
        agent.speed = 1000f;
        agent.acceleration = 500f;

        yield return new WaitForSeconds(0.5f);
        bIsPattern = false;
        pattern2_HitCollider.SetActive(false);
        anim.SetInteger("Pattern", 0);
        StartCoroutine(SetPattern());
    }
    private IEnumerator Pattern_3()
    {
        curPattern = 3; // 준비 단계
        anim.SetInteger("Pattern", curPattern);
        agent.speed = 0;
        agent.angularSpeed = 0;
        agent.isStopped = true;

        targetPosition = player.transform.position;
        pattern3_HitRange.SetActive(true);
        pattern3_HitRange.transform.position = new Vector3(targetPosition.x, 0.15f, targetPosition.z); // 첫번째 위치 세팅

        yield return new WaitForSeconds(0.3f);// 두번째 위치 세팅
        targetPosition2 = player.transform.position;
        pattern3_HitRange2.SetActive(true);
        pattern3_HitRange2.transform.position = new Vector3(targetPosition2.x, 0.15f, targetPosition2.z);

        yield return new WaitForSeconds(0.3f);// 세번째 위치 세팅
        targetPosition3 = player.transform.position;
        pattern3_HitRange3.SetActive(true);
        pattern3_HitRange3.transform.position = new Vector3(targetPosition3.x, 0.15f, targetPosition3.z);

        yield return new WaitForSeconds(0.3f);// 첫번째 위치로 이동
        agent.isStopped = false;
        bIsPattern = true;
        pattern3_HitRange.SetActive(false);
        pattern3_HitRange2.SetActive(false);
        pattern3_HitRange3.SetActive(false);
        agent.SetDestination(targetPosition);
        agent.speed = 1000f;
        agent.acceleration = 500f;

        yield return new WaitForSeconds(0.3f); // 이동 후 히트 콜라이더 실행
        pattern3_HitCollider.SetActive(true);

        yield return new WaitForSeconds(0.3f); // 두번째 위치로 이동
        agent.isStopped = false;
        bIsPattern = true;
        pattern3_HitCollider.SetActive(false);
        agent.SetDestination(targetPosition2);
        agent.speed = 1000f;
        agent.acceleration = 500f;

        yield return new WaitForSeconds(0.3f); // 이동 후 히트 콜라이더 실행
        pattern3_HitCollider.SetActive(true);
        yield return new WaitForSeconds(0.3f); // 세번째 위치로 이동
        agent.isStopped = false;
        bIsPattern = true;
        pattern3_HitCollider.SetActive(false);
        agent.SetDestination(targetPosition3);
        agent.speed = 1000f;
        agent.acceleration = 500f;

        yield return new WaitForSeconds(0.3f); // 이동 후 히트 콜라이더 실행
        pattern3_HitCollider.SetActive(true);
        yield return new WaitForSeconds(0.3f); //
        pattern3_HitCollider.SetActive(false);

        yield return new WaitForSeconds(1f); // 패턴 끝
        bIsPattern = false;
        pattern2_HitCollider.SetActive(false);
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
                    player.GetDamage(pattern2_Damage);
                    break;
                case 3:
                    player.GetDamage(pattern3_Damage);
                    break;
            }
            
        }
    }

}
