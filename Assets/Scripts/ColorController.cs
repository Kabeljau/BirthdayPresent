using UnityEngine;
using System.Collections;

public enum ColorState{
	red, 
	green, 
	blue, 
	noCol
}

public class ColorController : MonoBehaviour {

	//this is the scrit that sits on everything that can change the colorState- so far only these particle system vortex things.


	public delegate void colorChanged();
	public static event colorChanged OnColorChanged;

	public static ColorState colState;

	//is set in the inspector and depicts which colorstate will be activated by this object
	public ColorState ownColor;

	private Collider2D col2d;

	void Awake(){
		colState = ColorState.noCol;

		col2d = GetComponent<Collider2D> ();
		col2d.isTrigger = true;
	}


	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Player") {
			colState = ownColor;
			if(OnColorChanged != null){
				OnColorChanged();
			}
			Debug.Log (colState);
		}
	}

}
