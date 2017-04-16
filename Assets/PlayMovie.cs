using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMovie : MonoBehaviour {
	void Start () {
        MovieTexture tex = GetComponent<Renderer>().material.mainTexture as MovieTexture;
        tex.loop = true;
        tex.Play();
    }
}
