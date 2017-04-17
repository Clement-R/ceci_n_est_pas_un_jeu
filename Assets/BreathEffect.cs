using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BreathEffect : MonoBehaviour {
    public float effectTime = 1.0f;
    public float minSize = 0.5f;
    public float maxSize = 1f;

    RectTransform rect;

	void Start () {
        rect = GetComponent<RectTransform>();
        StartCoroutine(launchEffect(maxSize, minSize, effectTime));
	}

    IEnumerator launchEffect(float startScale,  float targetScale, float time) {
        float elapsedTime = 0;
        while (elapsedTime < time) {
            float v = Mathf.Lerp(startScale, targetScale, (elapsedTime / time));
            rect.localScale = new Vector3(v, v, v);
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        if(rect.localScale.x > 0.9) {
            StartCoroutine(launchEffect(maxSize, minSize, effectTime));
        } else {
            StartCoroutine(launchEffect(minSize, maxSize, effectTime));
        }
    }
}
