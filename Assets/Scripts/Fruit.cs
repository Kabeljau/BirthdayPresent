using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CircleCollider2D))]
public class Fruit : MonoBehaviour {

	CircleCollider2D col;

	Vector3 pos1;
	Vector3 pos;

	public float yLimit;
	public float speed;
	private float delay;

	

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
			Destroy(this.gameObject);
		}
	}


	void upAndDown(){
		pos.y = pos1.y + yLimit * (Mathf.Sin (Time.time+delay));
		transform.position = pos;
	}


}
