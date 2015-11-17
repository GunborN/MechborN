using UnityEngine;
using System.Collections;

public class ChangeScene : MonoBehaviour {

	public AudioSource startSound;

	public void ChangeToScene(int sceneToChangeTo)
	{
		Time.timeScale = 1;
		startSound.Play ();
		Application.LoadLevel (sceneToChangeTo);
	}

	public void QuitTheGame()
	{
		Application.Quit ();
	}
}
