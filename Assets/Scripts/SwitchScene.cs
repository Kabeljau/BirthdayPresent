using UnityEngine;
using System.Collections;

public class SwitchScene : MonoBehaviour {

	public void changeScene(string sceneName){
		ColorController.clearEvent ();
		Enemy.clearEvent ();
		Application.LoadLevel (sceneName);
	}

	public void quitGame(){
		Application.Quit ();
	}
}
