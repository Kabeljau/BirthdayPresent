using UnityEngine;
using System.Collections;


public enum ColorState{
	red, 
	green, 
	blue, 
	noCol
}
[RequireComponent(typeof(AudioSource))]
public class ColorController : MonoBehaviour {

	//this is the scrit that sits on everything that can change the colorState- so far only these particle system vortex things.


	public delegate void colorChanged();
	public static event colorChanged OnColorChanged;

	public static ColorState colState;

	//is set in the inspector and depicts which colorstate will be activated by this object
	public ColorState ownColor;

	private Collider2D col2d;

	public GameObject particles; 

	private AudioSource audio;

	void Awake(){
		colState = ColorState.noCol;


		col2d = GetComponent<Collider2D> ();
		col2d.isTrigger = true;

		audio = GetComponent<AudioSource> ();
	}


	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Player") {
			if(colState != ownColor){
				if(!audio.isPlaying){
					audio.Play ();
				}
				if(particles != null){
					GameObject p = Instantiate (particles, transform.position, transform.rotation) as GameObject;
					Destroy (p, 3.0f);
				}
			}

			colState = ownColor;
			if(OnColorChanged != null){
				OnColorChanged();
			}
			Debug.Log (colState);
		}
	}

	public static void clearEvent(){
		OnColorChanged = null;
	}

}
