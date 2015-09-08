using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Red : ColorVortex {
	GameObject[] redFruit;
	SpriteRenderer[] renderers;
	Collider2D[] colliders;

	void Awake(){
		base.Awake ();

		OnColorChanged += checkColorState;
		onFadeFinished += manageObjects;
		onFadeFinished += test;
	}

	void Start(){
		ownColor = ColorState.red;
		//deactivateObjects (redFruit);
		Color c = alphaParent.color;
		c.a = 0;
		alphaParent.color = c;

		refreshArrays ();
		foreach (Collider2D col in colliders) {
			col.enabled = false;
		}
	}

	public override void checkColorState(){
		if (colState == ColorState.red) {
			refreshArrays ();
			foreach( Renderer ren in renderers){
				ren.enabled = true;
			}
			StartCoroutine (fade (1, alphaParent.GetComponent<SpriteRenderer>(), fadeSpeed));
			StartCoroutine(synchronizeColor (renderers, alphaParent.GetComponent<SpriteRenderer>()));			
			Debug.Log ("activates red Stuff");
		} else{
			refreshArrays ();
			StartCoroutine (fade (0, alphaParent, fadeSpeed));
			StartCoroutine (synchronizeColor (renderers, alphaParent.GetComponent<SpriteRenderer>()));
			Debug.Log ("deactivates red stuff");
		}
	}

	void manageObjects(float alpha){
		if (alpha == 0) {
			foreach (Collider2D col in colliders) {
				col.enabled = false;
			}
			foreach(Renderer ren in renderers){
				ren.enabled = false;
			}
		} else if (alpha == 1) {
			foreach (Collider2D col in colliders) {
				col.enabled = true;
			}
		}
	}

	void refreshArrays(){
		redFruit = GameObject.FindGameObjectsWithTag ("red_fruit");
		
		renderers = new SpriteRenderer[redFruit.Length];
		colliders = new CircleCollider2D [redFruit.Length];
		for (int i = 0; i < redFruit.Length; i++) {
			renderers[i] = redFruit[i].GetComponent<SpriteRenderer>();
			renderers[i].color = Color.clear;
			colliders[i] = redFruit[i].GetComponent<CircleCollider2D>();
		}
	}

	void test(float alpha){
		Debug.Log ("event onFadeFinished has happened! Alpha: " + alpha);
	}
}
