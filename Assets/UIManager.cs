using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class UIManager : MonoBehaviour
{

    [SerializeField] private TMP_Text cluePercent;
    [SerializeField] private Image healthBar;
    private GameObject gameManager;

    public void Start()
    {
        // uncomment when Game Manager object available to use in scenes
        // gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
    }

    // updates the number of clues found when passed a float value by ??
    public void updateCluesFound(float percent)
    {
        cluePercent.text = (int)(percent*100) + "%";
    }

    // updates health bar when passed a float value by ??
    public void updateHealthBar(float percent)
    {
        healthBar.fillAmount = percent;
    }

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

}
