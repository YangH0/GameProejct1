using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;
using TMPro;

public class MonsterSpawner : MonoBehaviour
{
    [SerializeField] GameObject bossCam;
    [SerializeField] GameObject playerCam;
    [SerializeField] CinemachineBrain brain;

    [SerializeField] GameObject rabbit;
    [SerializeField] GameObject llama;
    [SerializeField] GameObject boar;
    [SerializeField] GameObject lion;
    [SerializeField] GameObject elephant;
    [SerializeField] GameObject Unicorn;
    [SerializeField] GameObject Mammoth;
    [SerializeField] GameObject Platypus;

    private List<GameObject> rabbitPool = new List<GameObject>();
    private List<GameObject> llamaPool = new List<GameObject>();
    private List<GameObject> boarPool = new List<GameObject>();
    private List<GameObject> lionPool = new List<GameObject>();
    private List<GameObject> elephantPool = new List<GameObject>();

    public List<GameObject> monsters = new List<GameObject>();

    public GameObject bossNameUI;
    public TextMeshProUGUI bossName;

    public int curTime = 0;

    private void Start()
    {
        StartCoroutine(StartTimer());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SpawnBoss(1);
            //GameObject newObj = Instantiate(Unicorn);
            //newObj.transform.position = new Vector3(0, 5, 0);
        }
        else if(Input.GetKeyDown(KeyCode.T))
        {
            SpawnBoss(2);
        }
        else if (Input.GetKeyDown(KeyCode.Y))
        {
            SpawnBoss(3);
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            curTime++;
            //Debug.Log(curTime);
        }
    }

    private IEnumerator SpawnMonster(int num, float time , int randomMin, int randomMax)
    {
        int randomNum = Random.Range(randomMin, randomMax + 1);

        if (monsters.Count >= 50)
        {
            yield return new WaitForSeconds(time);
            StartCoroutine(SpawnMonster(num, time, randomMin, randomMax));
            yield break;
        }

        for(int i = 0; i < randomNum; i++)
        {
            switch (num)
            {
                case 1:
                    MonsterPool(rabbit, rabbitPool);
                    break;
                case 2:
                    MonsterPool(boar, boarPool);
                    break;
                case 3:
                    MonsterPool(lion, lionPool);
                    break;
                case 4:
                    MonsterPool(llama, llamaPool);
                    break;
                case 5:
                    MonsterPool(elephant, elephantPool);
                    break;

            }
        }
        
        yield return new WaitForSeconds(time);
        StartCoroutine(SpawnMonster(num,time, randomMin, randomMax));
    }

    private void SpawnBoss(int num)
    {
        GameObject newObj = null;
        switch (num)
        {
            case 1:
                newObj = Instantiate(Unicorn);
                break;
            case 2:
                newObj = Instantiate(Mammoth);
                break;
            case 3:
                newObj = Instantiate(Platypus);
                break;
        }

        newObj.transform.position = new Vector3(0,0,0);
        newObj.transform.rotation = Quaternion.LookRotation(-newObj.transform.position);
        switch (num)
        {
            case 1:
                StartCoroutine(BossCamAnimation(newObj,"유니콘"));
                break;
            case 2:
                StartCoroutine(BossCamAnimation(newObj, "맘모스"));
                break;
            case 3:
                StartCoroutine(BossCamAnimation(newObj, "오리너구리"));
                break;
        }
    }

    private void ChangeMonsterSpawn(int num)
    {
        switch (num)
        {
            case 0: // 0 ~ 3 분
                StartCoroutine(SpawnMonster(1, 1,1 , 2));

                break;
            case 3: // 3 ~ 5 분
                StartCoroutine(SpawnMonster(1, 1, 1, 2));
                StartCoroutine(SpawnMonster(2, 5, 1, 2));
                StartCoroutine(SpawnMonster(3, 6, 1, 1));
                break;
            case 5: // 5 ~ 7 분
                StartCoroutine(SpawnMonster(1, 1, 1, 2));
                StartCoroutine(SpawnMonster(2, 4, 2, 3));
                StartCoroutine(SpawnMonster(3, 5, 1, 3));
                StartCoroutine(SpawnMonster(4, 6, 1, 2));
                break;
            case 7: // 7 ~  분
                StartCoroutine(SpawnMonster(1, 1, 2, 3));
                StartCoroutine(SpawnMonster(2, 3, 2, 3));
                StartCoroutine(SpawnMonster(3, 4, 1, 3));
                StartCoroutine(SpawnMonster(4, 4, 2, 3));
                StartCoroutine(SpawnMonster(5, 6, 1, 3));
                break;
        }
    }

    IEnumerator StartTimer()
    {
        switch (curTime)
        {
            case 0:
                ResetSpawn();
                ChangeMonsterSpawn(0);
                break;
            case 20:
                ResetSpawn();
                ChangeMonsterSpawn(3);
                break;
            case 40:
                ResetSpawn();
                ChangeMonsterSpawn(5);
                break;
            case 60:
                ResetSpawn();
                ChangeMonsterSpawn(7);
                break;
            case 100:
                SpawnBoss(1);
                break;
        }
        yield return new WaitForSeconds(1f);
        curTime++;
        Debug.Log(curTime);
        StartCoroutine(StartTimer());
    }

    private void ResetSpawn()
    {
        for (int i = 0; i < 5; i++)
            StopCoroutine("SpawnMonster");
    }

    public void MonsterPool(GameObject obj,List<GameObject> pool)
    {
        GameObject spawnObj = null;

        for(int i = 0; i < pool.Count; i++)
        {
            if (!pool[i].activeSelf)
            {
                spawnObj = pool[i].gameObject;
                spawnObj.SetActive(true);
                break;
            }
        }
        if (spawnObj == null)
        {
            spawnObj = Instantiate(obj);
            pool.Add(spawnObj);
        }
        monsters.Add(spawnObj);
        spawnObj.transform.position = GetRandomPosition();
    }

    public void DeleteMonsterSpawner(GameObject obj)
    {
        monsters.Remove(obj);
    }

    private Vector3 GetRandomPosition()
    {
        int direction = Random.Range(1, 5);
        Vector3 randomPosition = new Vector3(0,0,0);
        switch (direction)
        {
            case 1:
                randomPosition = new Vector3(Random.Range(-70,51), 1, -58);
                break;
            case 2:
                randomPosition = new Vector3(-72, 1, Random.Range(-54, 70));
                break;
            case 3:
                randomPosition = new Vector3(Random.Range(-70, 51), 1, 70);
                break;
            case 4:
                randomPosition = new Vector3(51, 1, Random.Range(-54, 70));
                break;
        }


        return randomPosition;
    }

    private IEnumerator BossCamAnimation(GameObject boss, string name)
    {
        yield return new WaitForSecondsRealtime(0.1f);
        bossNameUI.SetActive(true);
        bossName.text = name;
        brain.m_IgnoreTimeScale = true;
        bossCam.transform.position = new Vector3(boss.transform.position.x+ boss.transform.forward.x * 15
                                                , 10
                                                , boss.transform.position.z + boss.transform.forward.z * 20);
        bossCam.transform.rotation = Quaternion.LookRotation(boss.transform.position- bossCam.transform.position);
        Time.timeScale = 0f;
        playerCam.SetActive(false);
        yield return new WaitForSecondsRealtime(3f);
        playerCam.SetActive(true);
        bossNameUI.SetActive(false);
        yield return new WaitForSecondsRealtime(2f);
        Time.timeScale = 1f;
        brain.m_IgnoreTimeScale = false;
    }



}
