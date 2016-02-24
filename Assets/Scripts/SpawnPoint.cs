using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CircleCollider2D))]
public class SpawnPoint : MonoBehaviour {

	private CircleCollider2D col;

	void Start(){
		col = GetComponent<CircleCollider2D> ();
		col.isTrigger = true;
	}

	void OnTriggerEnter2D(Collider2D other){
		Debug.Log ("set Spawn Point");
		if (other.tag == "Player") {
			CharacterMovement.curSpawn = this.transform.position;
		}

	}
}
