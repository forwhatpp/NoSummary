using UnityEngine;
using UnityEngine.UI;

public class Setting: MonoBehaviour {

    private static Setting _init;

    public AudioSource AudioSource;
    public AudioClip CorrectSound;
    public AudioClip IncorrectSound;

    public Toggle SoundSetting;

    void Awake() {
        _init = this;
        DontDestroyOnLoad(this);
    }

    // Use this for initialization
    void Start() {
        this.AudioSource.mute = false;
    }

    // Update is called once per frame
    void Update() {

    }

    public void SoundMuteToggle() {
        this.AudioSource.mute = !this.SoundSetting.isOn;
    }

    public static void Play_CorrectSound() {
       // _init.AudioSource.PlayOneShot(_init.CorrectSound);
    }

    public static void Play_IncorrectSound() {
       // _init.AudioSource.PlayOneShot(_init.IncorrectSound);
//        int a = 10;
    }
}