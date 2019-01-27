using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialogInitializer : MonoBehaviour {
	static bool inDialog = false;
    [SerializeField] AudioClip Clip;
	public List<UnityEvent> dialogStart = new List<UnityEvent>();
	public List<UnityEvent> dialogEnd = new List<UnityEvent>();
	public List<UnityEvent> inviteHome = new List<UnityEvent>();

	bool wasInDIalog;
	bool wasInvited;

	void Start() {
		wasInvited = wasInDIalog = false;
	}

	void OnMouseDown() {
        AudioSource.PlayClipAtPoint(Clip, this.gameObject.transform.position);
        StartDialog();
	}

	public void StartDialog(){
		if (!inDialog && !wasInDIalog) {
			wasInDIalog = inDialog = true;

			DialogManager dm = GameObject.FindGameObjectWithTag("DialogManager").GetComponent<DialogManager>();

			foreach (var ev in dialogStart)
				ev.Invoke();

			dm.StartDialog(gameObject);
		}
	}

	public void EndDialog() {
		inDialog = false;
		foreach (var ev in dialogEnd)
			ev.Invoke();
	}

	public void InviteToHome(){
		if (wasInvited)
			return;
		wasInvited = true;

		foreach (var ev in inviteHome)
			ev.Invoke();
	}
}
