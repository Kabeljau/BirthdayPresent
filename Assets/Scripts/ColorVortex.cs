using UnityEngine;
using System.Collections;

public enum ColorState{
	red, 
	green, 
	blue, 
	noCol
}

[RequireComponent(typeof(CircleCollider2D))]
public class ColorVortex : MonoBehaviour {
	
	//Don't be confused by the silly name! I had to chose something else than "Color", since this already exists!!
	
	public delegate void colorChanged(ColorState State);
	public static event colorChanged OnColorChanged;
	
	private static ColorState ColState;
	public static ColorState colState {
		get{return ColState;}
		set{
			ColState = value;
			if(OnColorChanged != null){
				OnColorChanged(ColState);
			}
		}
	}
	
	protected ColorState ownColor;

	private CircleCollider2D col;

	void Awake(){
		col = GetComponent<CircleCollider2D> ();
		col.isTrigger = true;
	}
	
	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Player" ) {
				changeColor (ownColor);
				checkColorState();
		}
	}
	
	public virtual void checkColorState(){
	}
	
	void changeColor(ColorState newColor){
		colState = newColor;
		Debug.Log ("changed color to " + newColor);
	}
}

