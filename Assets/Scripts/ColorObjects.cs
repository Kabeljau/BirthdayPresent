using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(SpriteRenderer))]
public class ColorObjects : MonoBehaviour {

	//this is a script that cares for certain platforms, enemies and walls to appear/disappear. It sits on the parent object
	//of each type. The children themselves have NO own reaction to any color change

	public ColorState[] activeColors;

	private Collider2D[] ColChildren;
	private SpriteRenderer[] RenChildren;

	private List<Collider2D> L_ColChildren = new List<Collider2D> ();
	private List<SpriteRenderer> L_RenChildren = new List<SpriteRenderer>();

	private SpriteRenderer ren;
	public float fadeSpeed;

	public bool childrenAreDynamic;


	void Awake(){


		ColChildren = GetComponentsInChildren<Collider2D> ();
		RenChildren = GetComponentsInChildren<SpriteRenderer> ();

		if (childrenAreDynamic) {
			foreach(Collider2D c in ColChildren){
				L_ColChildren.Add (c);
			}
			foreach(SpriteRenderer r in RenChildren){
				L_RenChildren.Add (r);
			}
		}

		ren = GetComponent<SpriteRenderer> ();

		ColorController.OnColorChanged += reaction;
	}
	void Start(){
		reaction ();
	}

	private void reaction(){
		//checks if it needs to be activated or not by searching for colState in the active array
		int index_active = System.Array.IndexOf (activeColors, ColorController.colState);

	
		if (index_active > -1) {
			if(childrenAreDynamic){ 
				//active and list
				L_RenChildren.RemoveAll (item => item == null);
				L_ColChildren.RemoveAll (item => item == null);
				StartCoroutine (fade(1));
			}else{
				//active and array
				StartCoroutine (fade (1));
			}

		} else {
			if(childrenAreDynamic){
				//not active and List
				L_RenChildren.RemoveAll (item => item == null);
				L_ColChildren.RemoveAll (item => item == null);
				foreach(Collider2D c in L_ColChildren){
					c.enabled = false;
				}
				StartCoroutine (fade (0));
			}else{
				//not active and array
				foreach(Collider2D col in ColChildren){
					col.enabled = false;
				}
				StartCoroutine (fade (0));
			}
		}
	}

	private IEnumerator fade(float alpha){
		Color c = ren.color;
		c.a = alpha;
		Debug.Log ("coroutine fade started");
		//while the difference is still too big
		while(Mathf.Abs (ren.color.a - c.a)> 0.05f){
			ren.color = Color.Lerp (ren.color, c, fadeSpeed);
			Debug.Log (ren.color);

			if(childrenAreDynamic){
				//works with lists, ignores array
				foreach(SpriteRenderer r in L_RenChildren){
					r.color = ren.color;
				}
			}else{
				//works with array, ignores list
				foreach(SpriteRenderer r in RenChildren){
					r.color = ren.color;
				}
			}

			yield return new WaitForSeconds(0.1f);
		}
		ren.color = c;
		if (alpha == 1.0) {
				if(childrenAreDynamic){  //works on the list
					foreach(Collider2D co in L_ColChildren){
						co.enabled = true;
				}
			}else{   //works on the array
				foreach(Collider2D co in ColChildren){
					co.enabled = true;
				}
			}
		}
		StopAllCoroutines ();
	}

	/*for(var i = list.Count - 1; i > -1; i--)
	{
		if (list[i] == null)
			list.RemoveAt(i);
	}*/
}
