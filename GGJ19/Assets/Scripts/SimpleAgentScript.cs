using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SimpleAgentScript : MonoBehaviour {
	public GameObject currentTarget;
	public GameObject finistTarget;
	public GameObject inviteTarget;
	NavMeshAgent agent;

	void Awake() {
		agent = GetComponent<NavMeshAgent>();
		currentTarget = null;
	}

	void Update() {
		if (currentTarget != null)
			agent.SetDestination(currentTarget.transform.position);
	}

	public void MoveToFinish() {
		currentTarget = finistTarget;
	}

	public void MoveToInvite() {
		currentTarget = inviteTarget;
	}

	public void Stop() {
		currentTarget = null;
	}
}
