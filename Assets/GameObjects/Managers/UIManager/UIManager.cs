using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class UIManager : MonoBehaviour
{
	#region Attributes

	private GameManager gameManager;
    public UIKit uiKit;
	#endregion

	#region Methods

	// calls Game Manager to quit the game
	public void quitGame()
    {
        print("TODO Quit Game");
        // gameManager.quitGame();
    }

    // calls Game Manager to load main menu scene
    public void goToMainMenu()
    {
        print("TODO Go to Main Menu");
        // gameManager.goToMainMenu();
    }
	#endregion

	#region Unity

	public void Start()
    {
        // uncomment when Game Manager object available to use in scenes
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        uiKit = GameObject.FindObjectOfType<UIKit>().GetComponent<UIKit>();
    }
	#endregion
}
