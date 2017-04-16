using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaptureManagement : MonoBehaviour {
    public List<Vector2> pos;

    private List<PlayerPositionManager> players = new List<PlayerPositionManager>();
    bool taken = false;

    void Start() {
        foreach (var player in GameObject.FindGameObjectsWithTag("Player")) {
            players.Add(player.GetComponent<PlayerPositionManager>());
        } 
    }

	void Update () {
        if(!taken) {
            foreach (var player in players) {

                bool contained = true;
                foreach (var position in pos) {
                    if (!player.lastPositions.Contains(position)) {
                        contained = false;
                        break;
                    }
                }

                if (contained) {
                    Debug.Log("TAKEN !");
                    foreach (var obj in GetComponentsInChildren<Renderer>()) {
                        // TODO : Change for player color
                        obj.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.red);
                    }
                    taken = true;
                }
            }
        }
	}
}
