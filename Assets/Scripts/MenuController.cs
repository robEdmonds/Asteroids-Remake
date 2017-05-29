using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        // Start game
        if (Input.GetButtonDown("Submit"))
            StartGame();

        // Quit game
        if (Input.GetButtonDown("Cancel"))
            Quit();
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Scene/main");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
