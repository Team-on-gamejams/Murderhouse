using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanSpawner : MonoBehaviour {
	public GameObject ToSpawn;
	public GameObject SpawnPlace;
	public GameObject FinishPlace;
	public GameObject InvitePlace;
	public float interval = 10;
	public float time;

	void Update() {
		time += Time.deltaTime;
		if (time >= interval) {
			GameObject gm = Instantiate(ToSpawn, SpawnPlace.transform.position, Quaternion.identity);
			SimpleAgentScript agent = gm.GetComponent<SimpleAgentScript>();
			agent.finistTarget = FinishPlace;
			agent.inviteTarget = InvitePlace;

			agent.MoveToFinish();

			time = 0;
		}
	}
}
