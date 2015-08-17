using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Rigidbody2D))]
public class CharacterMovement : MonoBehaviour {

	public float maxSpeed;
	public float jumpForce;
	private float move;

	private bool facingRight = false;
	private Rigidbody2D rb;

	private Animator animator;

	bool grounded = true;
	public Transform groundCheck;
	float groundRadius = 0.002f;
	public LayerMask whatIsGround;

	void Awake(){
		rb = GetComponent<Rigidbody2D> ();
		animator = GetComponent<Animator> ();
	}

	void FixedUpdate(){
		grounded = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsGround);

		move = Input.GetAxis ("Horizontal");



		rb.velocity = new Vector2 (move * maxSpeed, rb.velocity.y);

		if (move > 0 && !facingRight) {flip();}
		else if(move < 0 && facingRight){flip();}

	}

	void Update(){
		animator.SetFloat ("Speed", Mathf.Abs (move));

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
