using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

	public float autoLoadNextLevelAfter;
    public string levelName;

	void Start () {
		if (autoLoadNextLevelAfter <= 0) {
			Debug.Log ("Level auto load disabled, use a postive number is seconds");
		} else {
			Invoke ("LoadLevel", autoLoadNextLevelAfter);
		}
	}

	public void LoadLevel(){
		Debug.Log ("New Level load: " + levelName);
		SceneManager.LoadScene(levelName);
	}

	public void QuitRequest(){
		Debug.Log ("Quit requested");
		Application.Quit ();
	}
}
