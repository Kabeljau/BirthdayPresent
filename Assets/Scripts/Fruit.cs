using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CircleCollider2D))]
public class Fruit : MonoBehaviour {

	private CircleCollider2D col;
	//needed for oscillating
	Vector3 pos1;
	Vector3 pos;

	public float yLimit;
	public float speed;
	private float delay;

	public AudioClip pickUp;
	void Awake(){

		col = GetComponent<CircleCollider2D> ();
		col.isTrigger = true;

		pos = transform.position;
		pos1 = transform.position;
		delay = Random.Range (0.0f, 0.6f);




	}
	void FixedUpdate(){
		//Debug.Log ("speed: " + speed);
		upAndDown ();


	}



	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Player") {
			AudioSource a = other.GetComponent<AudioSource>();
			a.clip = pickUp;
			a.Play ();
			GameManager.s.score += 1;
			Destroy(this.gameObject);
		}
	}


	void upAndDown(){
		pos.y = pos1.y + yLimit * (Mathf.Sin (Time.time+delay));
		transform.position = pos;
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
