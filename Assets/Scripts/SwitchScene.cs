using UnityEngine;
using System.Collections;

public class SwitchScene : MonoBehaviour {

	public void changeScene(int index){
		Application.LoadLevel (index);
	}

	public void quitGame(){
		Application.Quit ();
	}
}
