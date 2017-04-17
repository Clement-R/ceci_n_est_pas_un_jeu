using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {
    public Button[] buttons;
    public Image im;
    public float delay;

    public Sprite[] images;
    public Sprite controls;
    public Sprite credits;

    private int id = 0;
    private bool launched = true;
    private bool creditsShown = false;
    private bool controlsShown = false;

    void Start () {
        StartCoroutine("updateImage");

        buttons[0].GetComponent<Button>().onClick.AddListener(launchGame);
        buttons[1].GetComponent<Button>().onClick.AddListener(showControls);
        buttons[2].GetComponent<Button>().onClick.AddListener(showCredits);
        buttons[3].GetComponent<Button>().onClick.AddListener(quitGame);
    }

    void Update() {
        if(creditsShown) {
            if(Input.anyKeyDown) {
                showCredits();
                creditsShown = false;
            }
        }

        if (controlsShown) {
            if (Input.anyKeyDown) {
                showControls();
                controlsShown = false;
            }
        }
    }

    IEnumerator updateImage() {
        yield return new WaitForSeconds(delay);

        id++;
        if (id > 1) {
            id = 0;
        }

        im.sprite = images[id];
        StartCoroutine("updateImage");
    }

    public void launchGame() {
        SceneManager.LoadScene("lobby");
    }

    void showControls() {
        toggleButtons();
        if (!controlsShown) {
            im.sprite = controls;
            controlsShown = true;
        }
    }
    
    void showCredits() {
        toggleButtons();
        if(!creditsShown) {
            im.sprite = credits;
            creditsShown = true;
        }
    }

    void quitGame() {
        Application.Quit();
    }

    void toggleButtons() {
        if(launched) {
            StopCoroutine("updateImage");
            foreach (var button in buttons) {
                button.GetComponent<Image>().enabled = false;
            }
            launched = false;
        } else {
            StartCoroutine("updateImage");
            foreach (var button in buttons) {
                button.GetComponent<Image>().enabled = true;
            }
            launched = true;
        }
    }
}
