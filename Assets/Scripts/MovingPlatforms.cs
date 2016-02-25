using UnityEngine;
using System.Collections;

public class MovingPlatforms : MonoBehaviour {

	public Transform pointA;
	public Transform pointB;

	public float speed;

	//I want to use fixed directions to move back and forth instead of multiplying speed with -1. 
	private Vector3 vecAB;
	private Vector3 vecBA;

	private Vector3 direction;



	void Start(){
		transform.position = pointA.position;
		vecAB = new Vector3 (pointB.position.x - pointA.position.x ,pointB.position.y - pointA.position.y, pointB.position.z - pointA.position.z);
		vecBA = - vecAB;
	}

	void Update(){
		//quick and dirty because if the speed is too high, this might not get detected but it will work so YOLO!!
		if (Vector3.Distance (pointA.position, transform.position) < 0.4) {
			direction = vecAB;
		}else if (Vector3.Distance (pointB.position, transform.position) < 0.4) {
			direction = vecBA;
		}
		transform.Translate (direction * Time.deltaTime * speed);
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Player") {
			other.transform.parent = this.transform;
		}
	}

	void OnTriggerExit2D(Collider2D other){
		if (other.tag == "Player") {
			other.transform.parent = null;
		}
	}
}
