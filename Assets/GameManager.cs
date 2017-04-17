using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public GameObject[] playerPositions;
    public List<CaptureManagement> buildings = new List<CaptureManagement>();

    void Awake() {
        // GameSettings gs = GameObject.Find("GameSettings").GetComponent<GameSettings>();
    }

    void Start () {
		
	}
	
	void Update () {
        // Check if all buildings are captured
        bool gameEnd = true;
        foreach (var building in buildings) {
            if (building.taken == false) {
                gameEnd = false;
            }
        }
        // If so we end the game
        if(gameEnd) {
            Time.timeScale = 0.0f;
        }
	}
}
