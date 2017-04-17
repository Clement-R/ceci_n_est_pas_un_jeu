using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPositionManager : MonoBehaviour {
    public List<Vector2> lastPositions = new List<Vector2>();
    public int score = 0;
    public Text scoreDisplay;
    public Color color;

    private Vector2 position;
    private Vector2 previousPosition;
    private List<PlayerPositionManager> enemies = new List<PlayerPositionManager>();
    private MapManagement mapManager;

    void Start() {
        foreach (var enemy in GameObject.FindGameObjectsWithTag("Player")) {
            if(enemy != gameObject) {
                enemies.Add(enemy.GetComponent<PlayerPositionManager>());
            }
        }

        mapManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<MapManagement>();
    }

    void Update() {
        scoreDisplay.text = ""+score;

        position = new Vector2(Mathf.FloorToInt(Mathf.Abs(transform.position.x)), Mathf.FloorToInt(Mathf.Abs(transform.position.z)));

        if (position != previousPosition) {
            // If the new pos is equal to one of the saved points
            if (lastPositions.Count > 0 && lastPositions.Exists(pos => pos == position)) {
                // Launch detection of the building in the closed area and let GameManager add the points
                // TODO : Capture
                Debug.Log("Loop detected");

                // Remove trail
                resetTrail();
            }
            else {
                // Save new pos in the array
                lastPositions.Add(position);

                // If the new pos is in one of the enemies array, stop its trail
                foreach (var enemy in enemies) {
                    if(enemy.lastPositions.Count > 0) {
                        if(enemy.lastPositions.Exists(pos => pos == position)) {
                            enemy.resetTrail();
                        }
                    }
                }
            }

            previousPosition = position;
        }

        if(Input.GetKeyDown(KeyCode.Space)) {
            resetTrail();
        }
        
        if (Input.GetButtonDown("P1B")) {
            Debug.Log(Mathf.FloorToInt(Mathf.Abs(transform.position.x)) + " ; " + Mathf.FloorToInt(Mathf.Abs(transform.position.z)));
        }
    }

    public void resetTrail() {
        // Clear the list of last positions
        lastPositions.Clear();

        // Remove trail
        StartCoroutine("clearTrail");
    }

    IEnumerator clearTrail() {
        TrailRenderer trail = gameObject.GetComponentInChildren<TrailRenderer>();
        trail.time = 0;
        yield return new WaitForEndOfFrame();
        trail.time = Mathf.Infinity;
    }
}
