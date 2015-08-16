using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Rigidbody2D))]
public class CharacterMovement : MonoBehaviour {

	public float maxSpeed;
	public float jumpForce;

	private bool facingRight;
	private Rigidbody2D rb;

	bool grounded = false;
	public Transform groundCheck;
	float groundRadius = 0.002f;
	public LayerMask whatIsGround;

	void Awake(){
		rb = GetComponent<Rigidbody2D> ();
	}

	void FixedUpdate(){
		grounded = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsGround);

		float move = Input.GetAxis ("Horizontal");

		rb.velocity = new Vector2 (move * maxSpeed, rb.velocity.y);

		if (move > 0 && !facingRight) {flip();}
		else if(move < 0 && facingRight){flip();}

	}

	void Update(){
		if(grounded && Input.GetButtonDown ("Jump")){
			rb.AddForce(new Vector2(0, jumpForce));
		}
	}

	void flip(){
		facingRight = !facingRight;

		Vector3 scale = transform.localScale;
		scale.x *= -1;
		transform.localScale = scale;
	}
}
