using UnityEngine;
using System.Collections;

/*public enum ColorState{
	red, 
	green, 
	blue, 
	noCol
}

public class ColorController : MonoBehaviour {

	//objects to appear/disappear with the different colors
	private GameObject[] ap_red;  //appears on red (platforms)
	private GameObject[] dis_green;  //disappears on green (spiders)
	private GameObject[] dis_blue;  //disappears on blue (blocks)

	private static ColorState state;


	void Awake(){
		ap_red = GameObject.FindGameObjectsWithTag ("ap_red");
		dis_green = GameObject.FindGameObjectsWithTag ("dis_green");
		dis_blue = GameObject.FindGameObjectsWithTag ("dis_blue");
		state = ColorState.noCol;
		checkState ();
	}

	void OnTriggerEnter(Collider other){
		checkState (other);
	}

	void checkState(){
		switch (state) {
			
		case ColorState.red:
			state = ColorState.red;
			deActivate(ap_red, true);
			deActivate(dis_green, true);
			deActivate(dis_blue, true);
			break;
		case ColorState.green: 
			state = ColorState.green;
			deActivate(ap_red, false);
			deActivate(dis_green, false);
			deActivate(dis_blue, true);
			break;
		case ColorState.blue:
			state = ColorState.blue;
			deActivate(ap_red, false);
			deActivate(dis_green, true);
			deActivate(dis_blue, false);
			break;
		case ColorState.noCol:
			state = ColorState.noCol;
			deActivate(ap_red, false);
			deActivate(dis_green, true);
			deActivate(dis_blue, true);
			
			break;
		}
	}
	void checkState(Collider other){
		switch (other.tag) {
			
		case "red":
			state = ColorState.red;
			deActivate(ap_red, true);
			deActivate(dis_green, true);
			deActivate(dis_blue, true);
			break;
		case "green": 
			state = ColorState.green;
			deActivate(ap_red, false);
			deActivate(dis_green, false);
			deActivate(dis_blue, true);
			break;
		case "blue":
			state = ColorState.blue;
			deActivate(ap_red, false);
			deActivate(dis_green, true);
			deActivate(dis_blue, false);
			break;
		case "noCol":
			state = ColorState.noCol;
			deActivate(ap_red, false);
			deActivate(dis_green, true);
			deActivate(dis_blue, true);
			
			break;
		}
	}

	void deActivate( GameObject[] objects, bool stateToBe){
		foreach(GameObject ob in objects){
			ob.SetActive (stateToBe);
		}
	}


}
*/