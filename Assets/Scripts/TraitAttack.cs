using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraitAttack : MonoBehaviour
{
    public float testMaxTime;
    public float testCurTime;


    void Start()
    {
        
    }

    void Update()
    {
        testCurTime += Time.deltaTime;

    }
}
