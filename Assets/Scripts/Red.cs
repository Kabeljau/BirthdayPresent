using UnityEngine;
using System.Collections;

public class Red : ColorVortex {

	void Start(){
		ownColor = ColorState.red;
	}

	public override void checkColorState(){
		if (colState == ColorState.red) {
			Debug.Log ("activates red Stuff");
		} else {
			Debug.Log ("deactivates red stuff");
		}
	}
}
