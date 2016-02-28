using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	//not quite sure what a game manager normally does, THIS one cares for the score, the UI and sits on an empty game object 
	//that when touched exits the level. I should actually make the exit an own object that triggers an event or something like that, 
	// but since this is such a small game I will do it like this. 
	//Only one instance of this is allowed!!

	public static GameManager s;
	
	private  int Score;
	public int score
	{
		get
		{
			return Score;
		}
		set
		{
			Score = value;
			Debug.Log ("score: " + score);
			setScore();
		}
	}
	
	private int maxScore;
	
	public Text Tscore;

	//everything that will be shown when the level is finished
	private GameObject[] GOendscreen;
	public Text TfruitCollected;
	public Text TmaxFruit;
	
	void Awake(){
		s = this;
	}
	
	void Start(){
		score = 0;

		GOendscreen = GameObject.FindGameObjectsWithTag("Endscreen");

		GameObject[] fruit = GameObject.FindGameObjectsWithTag ("Fruit");

		maxScore = fruit.Length;

		foreach (GameObject GO in GOendscreen) {
			GO.SetActive(false);
		}
	}
	
	private void setScore(){
		Tscore.text = "" + score;
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Player") {
			levelFinished();
			other.GetComponent<CharacterMovement>().enabled = false;
		}
	}

	private void levelFinished(){
		TfruitCollected.text = "" + score;
		TmaxFruit.text = "" + maxScore;
		foreach (GameObject GO in GOendscreen) {
			GO.SetActive(true);
		}
	}



}
