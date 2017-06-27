using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaptureManagement : MonoBehaviour {
    public List<Vector2> pos;
    public bool taken = false;
    public int score = 0;

    private List<PlayerPositionManager> players = new List<PlayerPositionManager>();

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
                        player.score += score;

                        obj.material.shader = new Shader();

                        obj.material.EnableKeyword("_EMISSION");
                        obj.material = player.capture;
                        // obj.material.SetColor("_EmissionColor", player.color);
                        // obj.GetComponent<Renderer>().material.SetColor("_EmissionColor", player.color);
                    }
                    taken = true;
                }
            }
        }
	}
}
