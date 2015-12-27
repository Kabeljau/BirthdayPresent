using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer))]
public class ColorObjects : MonoBehaviour {

	//this is a script that cares for certain platforms, enemies and walls to appear/disappear. It sits on the parent object
	//of each type. The children themselves have NO own reaction to any color change

	public ColorState[] activeColors;

	private Collider2D[] ColChildren;
	private SpriteRenderer[] RenChildren;

	private SpriteRenderer ren;
	public float fadeSpeed;

	void Awake(){
		ColChildren = GetComponentsInChildren<Collider2D> ();
		RenChildren = GetComponentsInChildren<SpriteRenderer> ();

		ren = GetComponent<SpriteRenderer> ();

		ColorController.OnColorChanged += reaction;

	}

	private void reaction(){
		//checks if it needs to be activated or not by searching for colState in the active array
		int index_active = System.Array.IndexOf (activeColors, ColorController.colState);
		if (index_active > -1) {
			foreach (Collider2D col in ColChildren) {
				col.enabled = true;
				StartCoroutine (fade(1));
			}
		} else {
			foreach(Collider2D col in ColChildren){
				col.enabled = false;
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
			foreach(SpriteRenderer r in RenChildren){
				r.color = ren.color;
			}
			yield return new WaitForSeconds(0.1f);
		}
		ren.color = c;
		StopAllCoroutines ();
	}
}
