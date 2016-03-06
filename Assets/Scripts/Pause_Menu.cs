using UnityEngine;
using System.Collections;

public class Pause_Menu : MonoBehaviour {

	public GameObject pausePanel;
	public KeyCode pause;
	private bool paused;

	void Awake(){
		pause = KeyCode.P;
		pausePanel.SetActive (false);
		paused = false;
		Time.timeScale = 1.0f;
		Debug.Log ("time scale is: " + Time.timeScale);
	}

	void Update () {
		Debug.Log ("paused");
		if (Input.GetKeyUp (pause) && paused == false) {
			Debug.Log("pause Game");

			Time.timeScale = 0;
			pausePanel.SetActive (true);
			paused = true;
		} else if (Input.GetKeyUp (pause) && paused) {
			Debug.Log("unpause Game");

			pausePanel.SetActive (false);
			Time.timeScale = 1.0f;
			paused = false;
		}
	}

	public void continueGame(){
		pausePanel.SetActive (false);
		Time.timeScale = 1;
	}


}
