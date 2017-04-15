using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPositionManager : MonoBehaviour {
    private Vector2 position;
    
    void Update() {
        Debug.Log(Mathf.RoundToInt(Mathf.Abs(transform.position.x)) + " : " + Mathf.RoundToInt(Mathf.Abs(transform.position.z)));
    }
}
