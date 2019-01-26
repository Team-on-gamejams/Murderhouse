using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogInitializer : MonoBehaviour {
	static bool inDialog = false;

	bool wasInDIalog;

	void Start() {
		wasInDIalog = false;
	}

	void OnMouseDown() {
		if(!inDialog && !wasInDIalog) {
			wasInDIalog = inDialog = true;

			DialogManager dm = GameObject.FindGameObjectWithTag("DialogManager").GetComponent<DialogManager>();
			dm.StartDialog(gameObject);
		}
	}

	public void EndDialog(){
		inDialog = false;
	}
}
