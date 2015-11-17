using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TheScore : MonoBehaviour {

	private int score;
	public Text scoreText;

	// Use this for initialization
	void Start () {
		score = 0;
	}
	
	// Update is called once per frame
	void Update () {
		scoreText.text = score.ToString();
	}

	void UpdateScore(int scoreToAdd)
	{
		score += scoreToAdd;
	}
}
