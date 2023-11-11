using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountDownTimer : MonoBehaviour {

    public int initialTimeInSeconds; 

    private int currentTimeInSeconds;     
    private bool isCounting = false;
    private int elapsedTime = 0;

 
    public void StartTimer() {
        // Inicializa el tiempo y comienza la cuenta atrás
        currentTimeInSeconds = initialTimeInSeconds;
        UpdateTimerText();
        StartCoroutine(Countdown());
    }

    IEnumerator Countdown() {
        isCounting = true;

        while (currentTimeInSeconds > 0) {
            yield return new WaitForSeconds(1);
            currentTimeInSeconds--;
            elapsedTime++;
            UpdateTimerText();

            if (elapsedTime % 10 == 0) { // Cada 10 segundos reduce el tiempo en superficie de los topos
                if (GameManager.Instance.moleTimeUp > 0.6) {
                    GameManager.Instance.moleTimeUp -= GameManager.Instance.moleTimeUp * 6 / 100; // El tiempo minimo será 0.6
                } else {
                    GameManager.Instance.moleTimeUp = 0.6f;
                }
            }
            if (elapsedTime % 20 == 0) { // Cada 20 segundos aumenta el número maximo de topos y se reduce el tiempo de spawn
                if (GameManager.Instance.maxActiveMoles < 6) {
                    GameManager.Instance.maxActiveMoles++;
                }
                if (GameManager.Instance.minTimeToSpawm > 0) {
                    GameManager.Instance.minTimeToSpawm -= GameManager.Instance.originalMinTimeToSpawm * 12 / 100;
                    GameManager.Instance.maxTimeToSpawm -= GameManager.Instance.originalMaxTimeToSpawm * 12 / 100;
                }
            }
        }
        
        isCounting = false;
        TimerFinished();
    }

    void UpdateTimerText() {
        int minutes = currentTimeInSeconds / 60;
        int seconds = currentTimeInSeconds % 60;
        UIManager.Instance.SetTimeText(string.Format("{0:00}:{1:00}", minutes, seconds));
    }

    void TimerFinished() {
        GameManager.Instance.GameFinished();
    }
}
