using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataController : MonoBehaviour {


	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.X)) {
			SavingSystem.savingSystem.Delete ();
		}
		if (Input.GetKeyDown (KeyCode.S)) {
			SavingSystem.savingSystem.Save ();
		}
		if (Input.GetKeyDown (KeyCode.L)) {
			SavingSystem.savingSystem.Load ();
		}
	}
}
