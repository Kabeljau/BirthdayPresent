using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class CharacterMovement : MonoBehaviour
{

    //fields for simple left/right movement
    public float maxSpeed;
    public float jumpForce;
    private float move;
    private bool facingRight = false;

    //Valentin Stuff
    [SerializeField]
    private float maxRotation;
    [SerializeField]
    private float lerpRate;
    private Vector2 curNormal;
    private IEnumerator cor;


    private Rigidbody2D rb;

    private Animator animator;

    //fields for jumping
    public static bool grounded = true;
    public Transform groundCheckLeft;
    public Transform groundCheckRight;
    float groundRadius = 0.1f;
    public LayerMask whatIsGround;

    //fields for ground-aligning
    public float groundDetectingDistance;
    private Vector3 xAxis = new Vector3(1, 0, 0);

    // cheats
    bool jumpingEnabled;


    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        Enemy.OnTouchedEnemy += stun;
    }

    void FixedUpdate()
    {


        move = Input.GetAxis("Horizontal");



        rb.velocity = new Vector2(move * maxSpeed, rb.velocity.y);

        if (move > 0 && !facingRight) { flip(); }
        else if (move < 0 && facingRight) { flip(); }

    }

    void Update()
    {
        bool imminentHit = Physics2D.OverlapCircle(groundCheckLeft.position, groundRadius, whatIsGround) || Physics2D.OverlapCircle(groundCheckRight.position, groundRadius, whatIsGround);


        //checks if one of the characters feet touches the ground (I made two ground checkers because it is a quadruped)
        if (!grounded && imminentHit)
        {
            grounded = true;
        }
        else if (!imminentHit)
        {
            grounded = false;
        }


        animator.SetFloat("Speed", Mathf.Abs(move));

        if (Input.anyKeyDown)
        {
            cheat(Input.inputString);
            //Debug.Log (Input.anyKeyDown.ToString ());
        }

        if (grounded && Input.GetButtonDown("Jump") || jumpingEnabled && Input.GetButtonDown("Jump"))
        {
            rb.AddForce(new Vector2(0, jumpForce));
        }

		//Send a raycast to determine the hit ground plane
		if (grounded)
		{
			Ray2D ray = new Ray2D(transform.position + Vector3.up * 0.2f, -Vector3.up);
			RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, groundDetectingDistance, whatIsGround.value);
			//Debug.DrawRay(transform.position, hit.normal, Color.red);
			
			Quaternion rotQuat = Quaternion.FromToRotation(Vector3.up, hit.normal);
			transform.rotation = Quaternion.Slerp(transform.rotation, rotQuat, 0.3f);
			
			Debug.Log ("grounded");
		}
		else
		{
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.identity, 0.3f);
			Debug.Log ("not grounded");
		}
    }

   /* private void LateUpdate()
    {
        //Send a raycast to determine the hit ground plane
        if (grounded)
        {
            Ray2D ray = new Ray2D(transform.position + Vector3.up * 0.2f, -Vector3.up);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, groundDetectingDistance, whatIsGround.value);
            //Debug.DrawRay(transform.position, hit.normal, Color.red);

            Quaternion rotQuat = Quaternion.FromToRotation(Vector3.up, hit.normal);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotQuat, 0.3f);

			Debug.Log ("grounded");
        }
        else
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.identity, 0.3f);
			Debug.Log ("not grounded");
        }
    }*/

    //flips the character for walking left/right
    void flip()
    {
        facingRight = !facingRight;

        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    //void alignToGround(){

    //    //sends a raycast from the player down to the collider
    //    Vector3 down = -transform.up;
    //    Ray2D ray = new Ray2D (new Vector2 (transform.position.x, transform.position.y+4), new Vector2 (down.x, down.y));

    //    //gets the normal of the detected collider and creates newDown -> the new look direction will be based on this vector
    //    RaycastHit2D hit = Physics2D.Raycast (ray.origin, ray.direction, groundDetectingDistance, whatIsGround.value);
    //    Vector2 newDown = -hit.normal;

    //    //calculates the angle between the current down vector and the new down vector
    //    float angle = ((newDown.x*down.x + down.y * newDown.y) / (newDown.magnitude * down.magnitude));
    //    angle = Mathf.Acos (angle); 
    //    angle *= Mathf.Rad2Deg;

    //    //calculates the angle between newDown and the xAxis to find out whether the player is walking up or down a hill 
    //    if(Mathf.Acos((newDown.x*xAxis.x + xAxis.y * newDown.y) / (newDown.magnitude * xAxis.magnitude))*Mathf.Rad2Deg < 90){
    //        //Debug.Log ("direction of newDown is negative");
    //        //Debug.Log ("angle with xAxis: " + Mathf.Acos((newDown.x*xAxis.x + xAxis.y * newDown.y) / (newDown.magnitude * xAxis.magnitude))*Mathf.Rad2Deg);
    //        //Vector3 hitPoint = new Vector3(hit.point.x, hit.point.y, 0);
    //        //Debug.DrawLine (new Vector3(hit.point.x -20, hit.point.y, 0), hitPoint + 20 * xAxis, Color.white);
    //        angle = -angle;
    //    }

    //    //rotates the player if ground is flat enough
    //    if (Mathf.Abs (angle) < 70 && angle != 0) {

    //        Vector3 newLookDirection = new Vector3(newDown.y, -newDown.x, 0);
    //        //Debug.Log ("newLookDirection:" + newLookDirection);

    //        //the following didn't work but I kept it in, just in case...
    //        //transform.rotation= Quaternion.LookRotation (newLookDirection, new Vector3(0, 1, 0 ));
    //        //transform.eulerAngles = new Vector3 (0, 0, angle);
    //        //transform.Rotate(new Vector3(0, 0, 1), angle);
    //        //transform.Rotate (new Vector3(0, 0, angle));
    //        //transform.Rotate (0, 0, angle);
    //        //transform.Rotate (new Vector3(0, 0, angle));

    //        Quaternion rot = transform.rotation;
    //        rot.SetLookRotation (newLookDirection);
    //        //transform.rotation = rot;
    //        //Debug.DrawLine (transform.position, new Vector3(transform.position.x + newLookDirection.x * 1000, transform.position.y + newLookDirection.y * 1000, 0), Color.blue); 
    //    }


    ////	Debug.Log ("angle: " + angle);
    //    //Debug.DrawLine (transform.position, new Vector3(transform.position.x + down.x * 1000, transform.position.y + down.y * 1000, 0), Color.red); //the normal vector of the character
    //    //Debug.DrawLine (transform.position, new Vector3(transform.position.x + newDown.x * 1000, transform.position.y + newDown.y * 1000, 0), Color.green); //the normal vector of the ground



    //}

    //allows for endless jumping until the crazy upside-down-flipping-thing is under control...
    void cheat(string button)
    {
        switch (button)
        {
            case "j":
                jumpingEnabled = true;
                //Debug.Log ("jumpingEnabled:" + jumpingEnabled);
                break;
        }
    }

    //deactivates the character movement script for a second. does not take care of proper animations yet
    private void stun()
    {
        StartCoroutine("stunning");
        Debug.Log("stun was called");
    }

    private IEnumerator stunning()
    {
        Debug.Log("stunning started");
        this.enabled = false;
        yield return new WaitForSeconds(1);
        this.enabled = true;
        Debug.Log("stunning stopped");
    }









    //IGNORE THIS!!!!!!
    //IGNORE THIS!!!!!!
    //IGNORE THIS!!!!!!
    //IGNORE THIS!!!!!!
    //IGNORE THIS!!!!!!
    //IGNORE THIS!!!!!!
    //IGNORE THIS!!!!!!
    //IGNORE THIS!!!!!!
    //IGNORE THIS!!!!!!
    //IGNORE THIS!!!!!!
    /*void alignToGround2(){
        deleted inspector fields
         * 
        public Transform testObject;
        public Transform testObject2;
		
    public Transform raycaster1;
    public Transform raycaster2;
	
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

        Vector3 fwd = transform.right;
        //Debug.DrawLine (transform.position, transform.position + fwd * 100);
        Vector3 d = -transform.up;
        Debug.DrawLine (transform.position, transform.position + d * 100);
        Debug.DrawLine (raycaster1.transform.position, raycaster2.transform.position);
        Debug.Log ("fwdpoint: " + (transform.position + fwd * 100));

    }*/


}
