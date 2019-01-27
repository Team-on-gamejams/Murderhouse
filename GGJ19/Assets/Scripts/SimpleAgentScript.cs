using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SimpleAgentScript : MonoBehaviour {
	public GameObject currentTarget;
	public GameObject finistTarget;
	public GameObject inviteTarget;
	NavMeshAgent agent;
	Animator anim;

	void Awake() {
		agent = GetComponent<NavMeshAgent>();
		anim = GetComponentInChildren<Animator>();
		currentTarget = null;
	}

	void Update() {
		if (currentTarget != null) {
			anim.SetBool("IsWalking", true);
			agent.SetDestination(currentTarget.transform.position);
		}
		else
			agent.SetDestination(transform.position);
	}

	public void MoveToFinish() {
		anim.SetBool("IsWalking", true);
		currentTarget = finistTarget;
	}

	public void MoveToInvite() {
		anim.SetBool("IsWalking", true);
		currentTarget = inviteTarget;
	}

	public void Stop() {
		anim.SetBool("IsWalking", false);
		currentTarget = null;
	}
}
