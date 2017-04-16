using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuAnimation : MonoBehaviour {
    public float delay;
    public Sprite[] images;

    private Image im;
    private int id = 0;

	void Start () {
        im = GetComponent<Image>();
        StartCoroutine("updateImage");
    }
    
    IEnumerator updateImage() {
        yield return new WaitForSeconds(delay);

        id++;
        if(id > 1) {
            id = 0;
        }

        im.sprite = images[id];
        StartCoroutine("updateImage");
    }
}
