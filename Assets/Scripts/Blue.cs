using UnityEngine;
using System.Collections;

public class Blue : ColorVortex {

	void Awake(){
		base.Awake ();
		
	}
	// Use this for initialization
	void Start () {
		ownColor = ColorState.blue;
	}

}
