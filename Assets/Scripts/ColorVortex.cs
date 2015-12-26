using UnityEngine;
using System.Collections;
/*
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

	//fields for finding the matching fruit- instances
	public string fruitTag;
	private GameObject[] fruit;
	private SpriteRenderer[] renderers;
	private Collider2D[] colliders;

	//is set in each subclass
	protected ColorState ownColor;

	public SpriteRenderer alphaParent; //when objects fade in or out, a coroutine is started. all fruit get the same color value as alpha parent (so you need only one coroutine)
	public float fadeSpeed = 0.3f;
	private bool fadeIsRunning = false; //for the fade coroutines



	//the triggerbox
	private CircleCollider2D trigger;



	protected virtual void Awake(){
		//make the trigger really a trigger
		trigger = GetComponent<CircleCollider2D> ();
		trigger.isTrigger = true;

		//set the colorstate to the default state
		colState = ColorState.noCol;


		/*
	 * ATTENTION,PLEASE!! THIS IS KICKED OUT BECAUSE I CHANGED THE STRUCTURE OF THE CODE. 
	 * FROM NOW ON, THE ONLY THING IT WILL DO IS TO SET THE CURRENT COLOR STATE 
	 * INSTEAD OF CONTROLLING TRANSPARENCY AND SO ON OF ALL THE OBJECTS!
		//find all fruit and create the arrays
		//I decided to use arrays instead of lists because I have to pass them as parameters into methods and I did not find a 
		//proper way to do this with Generic Lists.....
		createArrays ();
		foreach (Collider2D col in colliders) {
			col.enabled = false;
		}

		Color invisible = Color.white;
		invisible.a = 0;
		alphaParent.color = invisible;

		OnColorChanged += checkColorState;
		OnColorChanged += test;

	}






	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Player" ) {
			if(colState != ownColor){
				changeColor (ownColor);

			}else if(colState == ownColor ){
				Debug.Log ("It's the same color!");
				Debug.Log ("colState: " + colState);
			}
		}
		Debug.Log ("something hit my trigger!" + ownColor + colState);
	}


	void test(){
		Debug.Log ("Event OnColorChanged happened");
	}


	void changeColor(ColorState newColor){
		colState = newColor;
		OnColorChanged();
		Debug.Log ("changed color to " + newColor);
	}





	//whenever the color is changed, the script checks if it has to do something (hide something, etc)
	private void checkColorState(){
		if (colState == ownColor) {
			createArrays ();
			foreach( Renderer ren in renderers){
				ren.enabled = true;
			}
			StartCoroutine (fade (1, alphaParent.GetComponent<SpriteRenderer>(), fadeSpeed));
			StartCoroutine(synchronizeColor (renderers, alphaParent.GetComponent<SpriteRenderer>()));			
			Debug.Log ("activates " + ownColor + " Stuff");
		} else{
			createArrays ();
			StartCoroutine (fade (0, alphaParent, fadeSpeed));
			StartCoroutine (synchronizeColor (renderers, alphaParent.GetComponent<SpriteRenderer>()));
			Debug.Log ("deactivates " + ownColor + " Stuff");
		}
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
/*
	void createArrays(){
		fruit = GameObject.FindGameObjectsWithTag (fruitTag);

		renderers = new SpriteRenderer[fruit.Length];
		colliders = new CircleCollider2D [fruit.Length];
		for (int i = 0; i < fruit.Length; i++) {
			renderers[i] = fruit[i].GetComponent<SpriteRenderer>();
			renderers[i].color = Color.clear;
			colliders[i] = fruit[i].GetComponent<Collider2D>();
		}
	}

	void manageComponents(float alpha){
		if (alpha < 0.5) {
			foreach (Collider2D col in colliders) {
				col.enabled = false;
			}

		} else if (alpha > 0.5) {
			foreach (Collider2D col in colliders) {
				col.enabled = true;
			}
		}
	}

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


	}

	protected IEnumerator synchronizeColor(SpriteRenderer[] children, SpriteRenderer parent){
		while (fadeIsRunning) {
			foreach(Collider2D col in colliders){
				col.enabled = false;
			}
			foreach(SpriteRenderer child in children){
				child.color = parent.color;
				//Debug.Log ("alpha parent: " + parent.color.a);
				//Debug.Log ("alpha child: " + child.color.a);
			}
			//Debug.Log ("Coroutine synchronizeColor is running!");
			yield return null;
		}
		//Debug.Log ("Coroutine synchronizeColor stopped!");
		foreach(SpriteRenderer child in children){
			child.color = parent.color;
			//Debug.Log ("alpha parent: " + parent.color.a);
			//Debug.Log ("alpha child: " + child.color.a);
		}
		manageComponents (parent.color.a);
	}
}
















/* BACKUP
 * [RequireComponent(typeof(CircleCollider2D))]
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
	}

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
	}

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
*/
