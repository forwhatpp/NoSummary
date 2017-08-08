using Assets.Scripts;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Home : MonoBehaviour{
    public Text HiScore;
    public AudioSource AudioSource;

    void Update() {
        this.HiScore.text = Player.HiScore.ToString("#,##0");
    }

    public void Function_StartGame() {
        SceneManager.LoadScene("Play", LoadSceneMode.Single);
    }

    public void Function_Setting() {

    }

    public void Function_Exit() {
        Application.Quit();
    }
}
