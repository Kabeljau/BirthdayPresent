using UnityEngine;
using System.Collections;

public class AlphaParent : MonoBehaviour {

	public ColorState ownColor;

	private bool active;

	private SpriteRenderer ren;
	public float fadeSpeed;

	void Awake(){
		//the default fruit will not be affected by any color change
		if (ownColor != ColorState.noCol) {
			ColorController.OnColorChanged += reaction;
		}

		ren = GetComponent<SpriteRenderer> ();
		if (fadeSpeed == 0) {
			Debug.Log (this + "fadeSpeed equals 0. Sprites will not fade in/out!");
		}
	}

	void reaction(){
		if (ColorController.colState == ownColor) {
			active = true;
			StartCoroutine (fade (1));
			Debug.Log ("fades");
		} else {
			active = false;
			StartCoroutine (fade (0));
		}
	}

	private IEnumerator fade(float alpha){
		Color c = ren.color;
		c.a = alpha;
		Debug.Log ("coroutine fade started");
		//while the difference is still too big
		while(Mathf.Abs (ren.color.a - c.a)> 0.05f){
			ren.color = Color.Lerp (ren.color, c, fadeSpeed*Time.deltaTime);
			Debug.Log (ren.color);
			yield return null;
		}
		ren.color = c;
		StopAllCoroutines ();
	}
}
