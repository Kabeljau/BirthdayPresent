﻿using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Rigidbody2D))]
public class CharacterMovement : MonoBehaviour {

	//fields for simple left/right movement
	public float maxSpeed;
	public float jumpForce;
	private float move;
	private bool facingRight = false;
	
	private Rigidbody2D rb;
	
	private Animator animator;

	//fields for jumping
	bool grounded = true;
	public Transform groundCheck;
	float groundRadius = 0.002f;
	public LayerMask whatIsGround;

	//fields for ground-aligning
	public Transform raycaster1;
	public Transform raycaster2;
	public float groundDetectingDistance;
	public Transform testObject;
	public Transform testObject2;

	// cheats
	bool jumpingEnabled;


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

		if (Input.anyKeyDown) {
			cheat (Input.inputString);
			//Debug.Log (Input.anyKeyDown.ToString ());
		}

		if(grounded && Input.GetButtonDown ("Jump") || jumpingEnabled && Input.GetButtonDown ("Jump")){
			rb.AddForce(new Vector2(0, jumpForce));
		}
		alignToGround ();

	}

	void flip(){
		facingRight = !facingRight;

		Vector3 scale = transform.localScale;
		scale.x *= -1;
		transform.localScale = scale;
	}

	void alignToGround(){
		// the plan is: have two points next to each other in the character and calculate the vector(here called curRot) between them. 
		//from each point a ray is cast straight downwards. 
		// when the two rays hit a collider, a vector is drawn between the hitpoints.
		//now calculate the angle between those two vectors and rotate your character accordingly to make it stick to the ground

		Ray2D ray1 = new Ray2D (new Vector2(raycaster1.transform.position.x, raycaster1.transform.position.y), new Vector2(0, -1));
		Ray2D ray2 = new Ray2D (new Vector2(raycaster2.transform.position.x, raycaster2.transform.position.y), new Vector2(0, -1));

		//Debug.Log ("raycaster1pos: " + raycaster1.transform.position);

		RaycastHit2D hit1 = new RaycastHit2D ();
		RaycastHit2D hit2 = new RaycastHit2D ();

		Vector2 v1 = new Vector2 (); //the points where the rays hit the collider
		Vector2 v2 = new Vector2 ();

		hit1 = Physics2D.Raycast (ray1.origin, ray1.direction, groundDetectingDistance , whatIsGround.value);
		hit2 = Physics2D.Raycast (ray2.origin, ray2.direction, groundDetectingDistance , whatIsGround.value);

		v1 = hit1.point;
		v2 = hit2.point;

		Vector3 pos = new Vector3(v1.x, v1.y, 0); //just for visualizing the hitpoints
		testObject.transform.position = pos;
		Vector2 theVector = (v1 - v2);
		Vector3 pos2 = new Vector3(v2.x, v2.y, 0);
		testObject2.transform.position = pos2;


		//Debug.Log ("v1: " + v1); //--> y changes when jumping. it shouldn't change!!! ??---->>>> It changed because I deleted the layer mask information....... 
		//Debug.Log ("v2: " + v2);
		//Debug.Log ("theVector: " + theVector);

		//calculate the vector within the character that represents its local axis
		Vector2 curRot = new Vector2 (raycaster1.transform.position.x-raycaster2.transform.position.x, raycaster1.transform.position.y-raycaster2.transform.position.y);

		//Debug.Log ("curRot: " + curRot);
		//Debug.Log ("theVector.magnitude):" + theVector.magnitude);
		//Debug.Log ("curRot.magnitude:" + curRot.magnitude);
		//Debug.Log ("dotproduct:" + Vector2.Dot (theVector, curRot));

		//calculate the angle between the two vectors.     -->>> the calculation itself returns a cosinus value, so I need to use acos to make it a degree value--but it doesn't work?? I guess it returns  radians, but how the hell can I turn those into degrees?? :( 
		float angle = ((theVector.x*curRot.x + theVector.y*curRot.y) / (theVector.magnitude * curRot.magnitude));
		angle = Mathf.Acos (angle);   // nope but WHY NOT???  my calculator gets it right.....
		angle *= Mathf.Rad2Deg;  //nope... YEEEESSSS!!!!! WORKED!!!
		//angle /= Mathf.Rad2Deg;  //nope
		//angle = (angle * 180) / Mathf.PI; //nope
		if (angle > 180) {
			angle = 360-angle;
		}
		//Debug.Log ("angle: " + angle);

		if(Physics2D.Raycast (ray1.origin, ray1.direction, groundDetectingDistance , whatIsGround.value)){
			//Debug.Log ("rotates");
			transform.rotation = Quaternion.identity; //somehow it keeps turning upside down when i don't do that.....
			transform.Rotate (0, 0, angle, Space.World);
		}

	}


	void cheat(string button){
		switch (button) {
		case "j":
			jumpingEnabled = true;
			Debug.Log ("jumpingEnabled:" + jumpingEnabled);
			break;
		}
	}







}
