using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherParticle : MonoBehaviour
{
    [SerializeField] GameObject player;

    void Update()
    {
        transform.position = new Vector3(player.transform.position.x, 0, player.transform.position.z);
    }
}
