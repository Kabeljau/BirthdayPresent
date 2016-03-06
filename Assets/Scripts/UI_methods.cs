using UnityEngine;
using System.Collections;

public class UI_methods : MonoBehaviour {

	public void show(GameObject GO){
		GO.SetActive (true);
	}

	public void hide(GameObject GO){
		GO.SetActive (false);
	}
}
