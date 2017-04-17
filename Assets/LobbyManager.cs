using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviour {
    public Image name;

    private bool nameSelection = true;
    private bool vehicleSelection = false;
    private bool playerSelection = false;

    private int maxChar = 4;
    private int indexChar = 1;
    private int indexPlayer = 1;

    private string[] playerNames = new string[4];

	void Start () {
		
	}
	
	void Update () {
        if(playerSelection) {
            if (Input.GetButtonDown("P1A") || Input.GetButtonDown("P2A") || Input.GetButtonDown("P3A") || Input.GetButtonDown("P4A")) {
                Debug.Log(Mathf.FloorToInt(Mathf.Abs(transform.position.x)) + " ; " + Mathf.FloorToInt(Mathf.Abs(transform.position.z)));
            }
        }

		if(nameSelection) {
            if (indexChar < maxChar) {

            }

            if (Input.GetButtonDown("P1A")) {
                Debug.Log("A");
            }

            if (Input.GetButtonDown("P1B")) {
                Debug.Log("B");
            }

            if (Input.GetButtonDown("P1X")) {
                Debug.Log("X");
            }

            if (Input.GetButtonDown("P1Y")) {
                Debug.Log("Y");
            }
        }

        if(vehicleSelection) {

        }
	}
}
