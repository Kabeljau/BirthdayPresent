using UnityEngine;
using System.Collections;

public enum ColorState{
	red, 
	green, 
	blue, 
	noCol
}

[RequireComponent(typeof(CircleCollider2D))]
public class ColorVortex : MonoBehaviour {
	
	//Don't be confused by the silly name! I had to chose something else than "Color", since this already exists!!
	
	public delegate void colorChanged();
	public static event colorChanged OnColorChanged;
	
	private static ColorState ColState;
	public static ColorState colState {
		get{return ColState;}
		set{
			ColState = value;
			if(OnColorChanged != null){
				OnColorChanged();
			}
		}
	}
	
	protected ColorState ownColor;

	public SpriteRenderer alphaParent; //when objects fade in or out, a coroutine is started. all fruit get the same color value as alpha parent (so you need only one coroutine)
	public delegate void fadeFinished(float alpha);
	public static event fadeFinished onFadeFinished; //when this event is called, the colliders are activated or deactivated. this way, no objects are destroyed while the coroutine is running


	public float fadeSpeed = 0.3f;

	private CircleCollider2D col;

	private bool fadeIsRunning = false; //for the fade coroutines

	protected virtual void Awake(){
		col = GetComponent<CircleCollider2D> ();
		col.isTrigger = true;
		colState = ColorState.noCol;
	}
	
	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Player" ) {
			if(colState != ownColor){
				changeColor (ownColor);
				checkColorState();
				OnColorChanged();
			}else if(colState == ownColor ){
				Debug.Log ("It's the same color!");
			}
		}
		Debug.Log ("something hit my trigger!" + ownColor + colState);
	}
	
	public virtual void checkColorState(){
	}
	
	void changeColor(ColorState newColor){
		colState = newColor;
		Debug.Log ("changed color to " + newColor);
	}

	/*protected void activateObjects(GameObject[] objects){
		foreach (GameObject obj in objects) {
			obj.GetComponent<Collider2D>().enabled = true;
			obj.GetComponent<Renderer>().enabled= true;
		}
	}

	protected void deactivateObjects(GameObject[] objects){
		foreach (GameObject obj in objects) {
			obj.GetComponent<Collider2D>().enabled = false;
			obj.GetComponent<Renderer>().enabled = false;
		}
	}*/

	protected IEnumerator fade(float alpha, SpriteRenderer ren, float speed){
		fadeIsRunning = true;
		Color c = ren.color;
		c.a = alpha;
		Debug.Log ("c.a" + c.a);
		while (Mathf.Abs (c.a - ren.color.a) > 0.07) {
			ren.color = Color.Lerp (ren.color, c, speed * Time.deltaTime);
			//Debug.Log ("Coroutine fade is running! alpha: " + ren.color.a);
			yield return null;
		}
		ren.color = c;
		fadeIsRunning = false;
		//Debug.Log ("Coroutine fade stopped!");
		onFadeFinished (ren.color.a);
	}

	/*protected IEnumerator fade(float alpha, SpriteRenderer ren, float speed,string method , GameObject[] objects){
		fadeIsRunning = true;
		Color c = ren.color;
		c.a = alpha;
		Debug.Log ("c.a" + c.a);
		if (method == "activate") {
			activateObjects (objects);
		} else {
			Debug.Log ("No valid number or 1");
		}
		while (Mathf.Abs (c.a - ren.color.a) > 0.07) {
			ren.color = Color.Lerp (ren.color, c, speed * Time.deltaTime);
			Debug.Log ("Coroutine fade is running! alpha: " + ren.color.a);
			yield return null;
		}
		ren.color = c;
		fadeIsRunning = false;
		if (method == "deactivate") {
			deactivateObjects (objects);
		} else {
			Debug.Log ("No valid string");
		}
		Debug.Log ("Coroutine fade stopped!");
	}*/

	protected IEnumerator synchronizeColor(SpriteRenderer[] children, SpriteRenderer parent){
		while (fadeIsRunning) {

			foreach(SpriteRenderer child in children){
				child.color = parent.color;
				//Debug.Log ("alpha parent: " + parent.color.a);
				//Debug.Log ("alpha child: " + child.color.a);
			}
			//Debug.Log ("Coroutine synchronizeColor is running!");
			yield return null;
		}
		//Debug.Log ("Coroutine synchronizeColor stopped!");
	}
}

