using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	public float collisionForce;

	/*void OnCollisionEnter(Collision other){
		if (other.gameObject.tag == "Player") {
			Vector2 dir = new Vector2(other.transform.position.x, other.transform.position.y);
			other.gameObject.GetComponent<Rigidbody2D>().AddForceAtPosition (, dir);
		}
	}*/
}
