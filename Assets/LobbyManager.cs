using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
    private int vehicleId = 0;

    private Dictionary<int, int> playersId = new Dictionary<int, int>();
    private Dictionary<int, string> playersNames = new Dictionary<int, string>();
    private Dictionary<int, string> playersVehicle = new Dictionary<int, string>();

    private string[] playerNames = new string[4];

	void Start () {
		
	}
	
	void Update () {

        #region NUMBER_PLAYER_SELECTION
        if (playerSelection) {
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
        #endregion NUMBER_PLAYER_SELECTION

        #region NAME_SELECTION
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
        #endregion NAME_SELECTION

        #region VEHICLE_SELECTION
        if (vehicleSelection) {
            if(playerIndexVehicle < playersId.Count + 1) {
                if (Input.GetButtonDown("P" + playersId[playerIndexVehicle] + "A")) {
                    switch(vehicleId) {
                        case 0:
                            playersVehicle.Add(playerIndexVehicle, "fire");
                            break;
                        case 1:
                            playersVehicle.Add(playerIndexVehicle, "icecream");
                            break;
                        case 2:
                            playersVehicle.Add(playerIndexVehicle, "garbage");
                            break;
                        case 3:
                            playersVehicle.Add(playerIndexVehicle, "police");
                            break;
                    }
                    Destroy(vehicles[vehicleId]);
                    vehicles[vehicleId] = null;
                    playerIndexVehicle++;

                    while (vehicles[vehicleId] == null) {
                        vehicleId++;
                        if (vehicleId > vehicles.Length - 1) {
                            vehicleId = 0;
                        }
                    }

                    foreach (var vehicle in vehicles) {
                        if (vehicle != null) {
                            vehicle.SetActive(false);
                        }
                    }
                    vehicles[vehicleId].SetActive(true);

                } else if(Input.GetAxisRaw("Horizontal" + playersId[playerIndexVehicle]) != 0 && canMove) {
                    canMove = false;
                    if (Input.GetAxisRaw("Horizontal" + playersId[playerIndexVehicle]) > 0) {
                        vehicleId++;
                        if (vehicleId > vehicles.Length - 1) {
                            vehicleId = 0;
                        }

                        while (vehicles[vehicleId] == null) {
                            vehicleId++;
                            if (vehicleId > vehicles.Length - 1) {
                                vehicleId = 0;
                            }
                        }

                    } else if (Input.GetAxisRaw("Horizontal" + playersId[playerIndexVehicle]) < 0) {
                        vehicleId--;
                        if (vehicleId < 0) {
                            vehicleId = vehicles.Length - 1;
                        }

                        while (vehicles[vehicleId] == null) {
                            vehicleId--;
                            if (vehicleId < 0) {
                                vehicleId = vehicles.Length - 1;
                            }
                        }
                    }

                    foreach (var vehicle in vehicles) {
                        if(vehicle != null) {
                            vehicle.SetActive(false);
                        }
                    }
                    vehicles[vehicleId].SetActive(true);

                    StartCoroutine("move");
                }
            } else {
                GameSettings gs = GameObject.Find("GameSettings").GetComponent<GameSettings>();
                gs.playersVehicle = playersVehicle;
                
                SceneManager.LoadScene("game");
            }
        }
        #endregion VEHICLE_SELECTION
    }

    IEnumerator move() {
        yield return new WaitForSeconds(0.3f);
        canMove = true;
    }
}
