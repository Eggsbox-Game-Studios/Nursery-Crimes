using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public CameraBehaviour LevelCamera;
    [SerializeField] PlayerController player;
    void Start()
    {
        Debug.Log("Game Loaded");
        GameObject _camera = GameObject.FindGameObjectWithTag("MainCamera");
        LevelCamera = _camera.GetComponent<CameraBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
