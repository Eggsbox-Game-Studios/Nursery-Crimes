using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public CameraBehaviour LevelCamera;
    PlayerController player;
    public UIManager uiManager;
    void Start()
    {
        //Get Variable Components
        uiManager = GameObject.FindObjectOfType<UIManager>().GetComponent<UIManager>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();


        GameObject _camera = GameObject.FindGameObjectWithTag("MainCamera");
        LevelCamera = _camera.GetComponent<CameraBehaviour>();

        Debug.Log("Game Loaded");
    }
}
