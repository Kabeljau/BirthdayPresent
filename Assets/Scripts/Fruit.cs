using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CircleCollider2D))]
public class Fruit : MonoBehaviour {

	//is set in the inspector and tells the object which colorstate it has to react on
	public ColorState ownColor;
	//returns true when ColorController.colState equals ownColor
	private bool active;

	private CircleCollider2D col;

	//used to fade in and out
	private SpriteRenderer ren;
	private Color opaque;
	private Color transparent;
	public float fadeSpeed;


	//needed for oscillating
	Vector3 pos1;
	Vector3 pos;

	public float yLimit;
	public float speed;
	private float delay;

	

	void Awake(){
		ColorController.OnColorChanged += reaction;

		col = GetComponent<CircleCollider2D> ();
		col.isTrigger = true;

		pos = transform.position;
		pos1 = transform.position;

		delay = Random.Range (0.0f, 0.6f);

		ren = GetComponent<SpriteRenderer> ();

		opaque = ren.color;
		opaque.a = 1;
		transparent = ren.color;
		transparent.a = 0;
		if (fadeSpeed == 0) {
			Debug.Log (this + "fadeSpeed is set to zero. The object will not visibly react to the colorState!");
		}

	}
	void FixedUpdate(){
		//Debug.Log ("speed: " + speed);
		upAndDown ();

		fade ();
	}



	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Player") {
			Destroy(this.gameObject);
		}
	}


	void upAndDown(){
		pos.y = pos1.y + yLimit * (Mathf.Sin (Time.time+delay));
		transform.position = pos;
	}

	//controls what happens at the states; I put it in an own method just in case I want to add some more actions
	void reaction(){
		if (ColorController.colState == ownColor) {
			active = true;
		} else {
			active = false;
		}
	}


	void fade(){

		if (active) {
			if (ren.color.a < 1) {


			} 
		} else {
			if(ren.color.a > 0){

			}
		}
	}

	void OnDestroy(){
		ColorController.OnColorChanged -= reaction;
	}
	/*protected IEnumerator fade(float alpha, SpriteRenderer ren, float speed){
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
		
		
	}*/
}
