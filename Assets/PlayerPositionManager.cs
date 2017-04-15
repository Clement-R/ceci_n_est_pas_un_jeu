﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPositionManager : MonoBehaviour {
    public List<Vector2> lastPosition = new List<Vector2>();

    private Vector2 position;
    private Vector2 previousPosition;
    private List<PlayerPositionManager> enemies = new List<PlayerPositionManager>();
    
    void Update() {
        position = new Vector2(Mathf.RoundToInt(Mathf.Abs(transform.position.x)), Mathf.RoundToInt(Mathf.Abs(transform.position.z)));

        if (position != previousPosition) {
            // If the new pos is equal to one of the saved points
            if (lastPosition.Count > 0 && lastPosition.Exists(pos => pos == position)) {
                // Launch detection of the building in the closed area and let GameManager add the points
                Debug.Log("Loop detected");

                // Remove trail
                resetTrail();
            }
            else {
                // Save new pos in the array
                lastPosition.Add(position);

                // If the new pos is in one of the enemies array, stop its trail
                foreach (var enemy in enemies) {
                    if(enemy.lastPosition.Count > 0) {
                        Debug.Log("More than one spot");
                        if(enemy.lastPosition.Exists(pos => pos == position)) {
                            Debug.Log("Lel");
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
    }

    public void resetTrail() {
        // Clear the list of last positions
        lastPosition.Clear();

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