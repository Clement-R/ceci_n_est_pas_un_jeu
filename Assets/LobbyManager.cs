using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviour {
    public Image canvas;
    public Image numberDisplay;
    public GameObject Go;
    public Sprite[] numbers;
    public GameObject[] vehicles;

    public Sprite i_playerSelection;
    public Sprite i_nameSelection;
    public Sprite i_vehicleSelection;

    public GameObject playerSelectionAssets;
    public GameObject vehicleSelectionAssets;

    private bool playerSelection = true;
    private bool nameSelection = false;
    private bool vehicleSelection = false;
    private bool canMove = true;

    private int maxChar = 4;
    private int indexChar = 1;
    private int indexPlayer = 0;
    private int playerIndexName = 1;
    private int playerIndexVehicle = 1;

    private Dictionary<int, int> playersId = new Dictionary<int, int>();
    private Dictionary<int, string> playersNames = new Dictionary<int, string>();
    private Dictionary<int, GameObject> playersVehicle = new Dictionary<int, GameObject>();

    private string[] playerNames = new string[4];

	void Start () {
		
	}
	
	void Update () {
        if(playerSelection) {
            if (Input.GetButtonDown("P1A")) {
                if(!playersId.ContainsValue(1)) {
                    indexPlayer++;
                    playersId.Add(indexPlayer, 1);
                }
            }

            if(Input.GetButtonDown("P2A")) {
                if (!playersId.ContainsValue(2)) {
                    indexPlayer++;
                    playersId.Add(indexPlayer, 2);
                }
            }

            if (Input.GetButtonDown("P3A")) {
                if (!playersId.ContainsValue(3)) {
                    indexPlayer++;
                    playersId.Add(indexPlayer, 3);
                }
            }

            if (Input.GetButtonDown("P4A")) {
                if (!playersId.ContainsValue(4)) {
                    indexPlayer++;
                    playersId.Add(indexPlayer, 4);
                }
            }

            numberDisplay.sprite = numbers[indexPlayer];

            if (playersId.Count >= 2) {
                // Show go
                Go.SetActive(true);
            }
            else {
                // Hide go
                Go.SetActive(false);
            }

            if (Input.GetButtonDown("P1Start") || Input.GetButtonDown("P2Start") || Input.GetButtonDown("P3Start") || Input.GetButtonDown("P4Start")) {
                if(playersId.Count >= 2) {

                    foreach (var player in playersId) {
                        playersNames.Add(player.Key, "");
                    }

                    playerSelection = false;
                    
                    // nameSelection = true;
                    // canvas.sprite = i_nameSelection;

                    vehicleSelection = true;
                    canvas.sprite = i_vehicleSelection;
                    
                    playerSelectionAssets.SetActive(false);
                    vehicleSelectionAssets.SetActive(true);
                }
            }
        }

		if(nameSelection) {
            if (indexChar <= maxChar) {
                if (Input.GetButtonDown("P"+ playersId[playerIndexName] + "A")) {
                    playersNames[playerIndexName] += "A";
                    indexChar++;
                    Debug.Log("A");
                } else if (Input.GetButtonDown("P" + playersId[playerIndexName] + "B")) {
                    playersNames[playerIndexName] += "B";
                    indexChar++;
                    Debug.Log("B");
                } else if (Input.GetButtonDown("P" + playersId[playerIndexName] + "X")) {
                    playersNames[playerIndexName] += "X";
                    indexChar++;
                    Debug.Log("X");
                } else if (Input.GetButtonDown("P" + playersId[playerIndexName] + "Y")) {
                    playersNames[playerIndexName] += "Y";
                    indexChar++;
                    Debug.Log("Y");
                }
            } else {
                indexChar = 0;
                playerIndexName++;
                // TODO : Reset name buttons

                if (playerIndexName == playersId.Count + 1) {
                    Debug.Log(playersNames[0]);
                    Debug.Log(playersNames[1]);
                    Debug.Log("Ready for vehicle selection");
                }
            }
        }

        if(vehicleSelection) {
            if(playerIndexVehicle < playersId.Count + 1) {
                if (Input.GetButtonDown("P" + playersId[playerIndexVehicle] + "A")) {
                    // playersVehicle[playerIndexVehicle] = none;
                    playerIndexVehicle++;
                    Debug.Log("A");
                } else if(Input.GetAxisRaw("Horizontal" + playersId[playerIndexVehicle]) != 0 && canMove) {
                    canMove = false;
                    Debug.Log("Move h");
                }
            }
        }
	}

    IEnumerator move() {
        yield return new WaitForSeconds(0.1f);
        canMove = true;
    }
}
