using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    [SerializeField] GameObject rabbit;
    [SerializeField] GameObject llama;
    [SerializeField] GameObject boar;
    [SerializeField] GameObject lion;
    [SerializeField] GameObject elephant;
    [SerializeField] GameObject Unicorn;

    private List<GameObject> rabbitPool = new List<GameObject>();
    private List<GameObject> llamaPool = new List<GameObject>();
    private List<GameObject> boarPool = new List<GameObject>();
    private List<GameObject> lionPool = new List<GameObject>();
    private List<GameObject> elephantPool = new List<GameObject>();

    public List<GameObject> monsters = new List<GameObject>();

    public int curTime = 0;

    private void Start()
    {
        StartCoroutine(StartTimer());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            GameObject newObj = Instantiate(Unicorn);
            newObj.transform.position = new Vector3(0, 5, 0);
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            curTime++;
            Debug.Log(curTime);
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

    private void ChangeMonsterSpawn(int num)
    {
        switch (num)
        {
            case 0: // 0 ~ 3 분
                StartCoroutine(SpawnMonster(1, 1,1 , 3));

                break;
            case 3: // 3 ~ 5 분
                StartCoroutine(SpawnMonster(1, 1, 2, 4));
                StartCoroutine(SpawnMonster(2, 5, 1, 2));
                StartCoroutine(SpawnMonster(3, 6, 1, 1));
                break;
            case 5: // 5 ~ 7 분
                StartCoroutine(SpawnMonster(1, 1, 2, 4));
                StartCoroutine(SpawnMonster(2, 4, 2, 3));
                StartCoroutine(SpawnMonster(3, 5, 1, 3));
                StartCoroutine(SpawnMonster(4, 6, 1, 2));
                break;
            case 7: // 7 ~  분
                StartCoroutine(SpawnMonster(1, 1, 4, 5));
                StartCoroutine(SpawnMonster(2, 3, 2, 4));
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



}
