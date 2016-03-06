using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	public bool isTrap;
	public AudioClip touchPlayer;
	public delegate void touchedEnemy(Enemy e);
	public static event touchedEnemy OnTouchedEnemy;

	public GameObject fruitParticles;

	public float collisionForce;

	//event tells CharacterMovement that Player has touched Enemy-> characterMovement will stun player for a second 
	void OnCollisionEnter2D(Collision2D other){
		if (OnTouchedEnemy != null) {
			OnTouchedEnemy(this);
			AudioSource au =  other.gameObject.GetComponent<AudioSource>();
			au.clip = touchPlayer;
			au.Play ();
			Debug.Log (this + "collided with enemy");
			GameObject particles = Instantiate (fruitParticles, transform.position, transform.rotation) as GameObject; 
			Destroy (particles, 3.0f);
		}
	}

	public static void clearEvent(){
		OnTouchedEnemy = null;
	}
}
