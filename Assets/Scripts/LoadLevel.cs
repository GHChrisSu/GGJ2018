using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadLevel : MonoBehaviour {
    private float autoLoadNextLevelAfter = 3f;
    private string levelName;
    private Button levelLoadButton;
    public static bool Moving { get; set; }

    private void Start()
    {
        levelLoadButton = GetComponent<Button>();
        Moving = false;
    }

    public void LoadLevelWithName(string levelName)
    {
        Debug.Log("New Level load: " + levelName);
        this.levelName = levelName;
        MoveDoor.Moving = true;
        Moving = true;
        SceneManager.LoadScene(levelName, LoadSceneMode.Additive);
        Invoke("DestoryCurrentScene", autoLoadNextLevelAfter);
    }

    private void Update()
    {
        if (Moving)
        {
            levelLoadButton.transform.Translate(new Vector3(3, 0, 0));
        }
    }

    private void DestoryCurrentScene()
    {
        SceneManager.UnloadSceneAsync("ChooseLevel");
    }

}
