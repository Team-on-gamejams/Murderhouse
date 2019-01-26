using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanSpawner : MonoBehaviour
{

    public GameObject ToSpawn;
    public Transform SpawnPlace;
    public float interval = 10;
    public float time;

    void Start()
    {
       // ToSpawn = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        time += Time.deltaTime;
        if(time >= interval)
        {
            Instantiate(ToSpawn, SpawnPlace.position, Quaternion.identity);
            time = 0;
        }
    }
}
