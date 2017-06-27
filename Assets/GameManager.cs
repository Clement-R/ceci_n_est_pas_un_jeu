using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public GameObject[] playerPositions;
    public List<Vehicle> vehicles = new List<Vehicle>();
    public List<CaptureManagement> buildings = new List<CaptureManagement>();

    private GameSettings gs;

    void Awake() {
        gs = GameObject.Find("GameSettings").GetComponent<GameSettings>();
    }

    void Start () {
        // Instantiate all choosed vehicles to the good starting points
        // Foreach, change its player number to the linked controller id
        foreach (var playerSetting in gs.playersVehicle) {
            Transform tr = playerPositions[playerSetting.Key - 1].transform;

            foreach (var vehicle in vehicles) {
                if(vehicle.name == playerSetting.Value) {
                    GameObject go = Instantiate(vehicle.vehicle, tr.position, Quaternion.identity);
                    go.GetComponent<PlayerMovement>().m_PlayerNumber = playerSetting.Key;
                }
            }
        }
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
