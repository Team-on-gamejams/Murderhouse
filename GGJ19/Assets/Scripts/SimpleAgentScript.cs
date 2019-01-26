using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SimpleAgentScript : MonoBehaviour
{
    public NavMeshAgent agent;
    public GameObject target;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("MyFinishObjct");
    }

    void Update()
    {
        agent.SetDestination(target.transform.position);
    }
}
