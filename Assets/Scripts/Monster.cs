using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monster : MonoBehaviour
{
    [SerializeField]
    NavMeshAgent agent;

    [SerializeField]
    Transform target;

    void Awake()
    {
        
    }

    void Update()
    {
        agent.SetDestination(target.position);
    }
}
