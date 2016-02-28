using UnityEngine;
using System.Collections;

public class SwitchScene : MonoBehaviour {

	public void changeScene(string sceneName){
		Application.LoadLevel (sceneName);
	}

	public void quitGame(){
		Application.Quit ();
	}
}
