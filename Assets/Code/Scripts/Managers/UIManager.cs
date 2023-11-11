using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour {
    public static UIManager Instance { get; private set; }

    public GameObject uiPanel;
    public GameObject finalScorePanel;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI finalScoreText;
    public TextMeshProUGUI timeText;
    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(gameObject);
        }
    }
    public void Init() {
        scoreText.text = "0";
    }
    public void SetScoreText(string text) {
        scoreText.text = text;
    }

    public void SetTimeText(string text) {  
        timeText.text = text; 
    }

    public void ShowFinalScore() {
        finalScoreText.text = scoreText.text;
        uiPanel.SetActive(false);
        finalScorePanel.SetActive(true);
    }

    public void PlayAgainBtnTapped() {
        uiPanel.SetActive(true);
        finalScorePanel.SetActive(false);
        GameManager.Instance.InitGame();
    }
}




