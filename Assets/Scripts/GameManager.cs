using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Assets.Scripts;
using System.Collections.Generic;
using Assets.Scripts.Map;
using System;
using Assets.Scripts.GridMap;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager Instance {
        get;private set;
    }

    #region Event
    public GameObject EventPanel;
    public GameObject Event_NewGame,Event_EndGame,Event_Pause;
    #endregion

    public Text text_score, text_needNumber;
    public Timer timer;
    public NumberMapController _numberMapController;
    private QuestionManager _quizManager;
    private int quiz_answer = 0;

    public const double MAX_TIME = 90;
    public const double START_TIME = 60;
    public const double EXTRA_TIME = 5;

    void Awake() {
        Player.Score = 0;
        GameManager.Instance = this;
        this._quizManager = new QuestionManager(5);
    }

	// Use this for initialization
	void Start () {
        this._numberMapController.setMap(this._quizManager.MapData);
        //this.quiz_answer = this._quizManager.getQuestion();
	}
	
	// Update is called once per frame
	void Update () {
        this.text_score.text = Player.Score.ToString();
        this.text_needNumber.text = quiz_answer.ToString();
	}


    public void NewGame() {
        Player.Score = 0;
        this.timer.StartTimer(START_TIME,EndGame);
        this.quiz_answer = this._quizManager.getQuestion();
        this.Event_NewGame.SetActive(false);
        this.EventPanel.SetActive(false);
    }

    public void LoadNewGame() {
        this.Event_EndGame.SetActive(false);
        this.Event_NewGame.SetActive(true);
    }

    private void EndGame() {
        this.EventPanel.SetActive(true);
        this.Event_EndGame.SetActive(true);

        this.Event_EndGame.transform.FindChild("Text_Score").GetComponent<Text>().text = "Your Score"+'\n'+Player.Score;
        if(Player.HiScore < Player.Score) {
            Player.HiScore = Player.Score;
        }
    }

    public void Function_BackToHome() {
        SceneManager.LoadScene("Home",LoadSceneMode.Single);
    }

    public void Function_Pause() {

        this.EventPanel.SetActive(true);
        this.Event_Pause.SetActive(true);

        this.timer.Pause();
        this._numberMapController.Hide();
    }

    public void Function_Resume() {

        this.EventPanel.SetActive(false);
        this.Event_Pause.SetActive(false);

        this._numberMapController.Show();
        this.timer.ResumeTimer();
    }

    public void Pass() {
        AddScore(-100);
        NextQuestion();
    }

    public void AddScore(int score) {
        Player.Score = Math.Max(score+Player.Score,0);
    }
   
    private void InCorrect() {
        //this.timer.AddExtraTime(-3f);
        Setting.Play_IncorrectSound();
    }

    private void Correct(List<MapCell> selectedCellList){
        int blockCount = selectedCellList.Count;

        Setting.Play_CorrectSound();

        AddScore(blockCount * (int)(100 + blockCount*0.25));

        int ExtraTime = Math.Min((int)EXTRA_TIME,blockCount);

        if(this.timer.currentTime + ExtraTime > MAX_TIME) {
            int bonus = (int)(this.timer.currentTime + ExtraTime - MAX_TIME);
            AddScore(bonus * blockCount);
        }
        this.timer.AddExtraTime(ExtraTime);

        List<IPositionProperties> position = new List<IPositionProperties>();
        foreach(MapCell m in selectedCellList) {
            position.Add(m);
            m.Spin();
        }
        this._quizManager.GenerateNumberAtFixPosition(position);

        this._numberMapController.setMap(this._quizManager.MapData);
        NextQuestion();


    }

    private void NextQuestion() {
        this.quiz_answer = this._quizManager.getQuestion();
    }

    public static void SubmitAnswer(List<MapCell> selectedCellList) {
        int ans = 0;

        if(selectedCellList.Count < 2) {
            return;
        }
        //List<Vector2> fixPoint = new List<Vector2>();

        foreach(MapCell m in selectedCellList) {
            ans += Int32.Parse(m.textField.text);
        }

        //Correct !
        if(ans==GameManager.Instance.quiz_answer) {
            GameManager.Instance.Correct(selectedCellList);
        } else {
            Debug.Log("Ans=Quiz > "+ans+"/"+ GameManager.Instance.quiz_answer);
            GameManager.Instance.InCorrect();
        }
    }
}
