using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Services.Analytics.Platform;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager Instance { get; private set; }
    public float minTimeToSpawm;
    public float maxTimeToSpawm;
    public int maxActiveMoles;
    public MoleController[] moles;
    public int score;
    public float moleTimeUp;
    public bool isGameRunning = false;

    public float originalMoleTimeUp;
    public float originalMinTimeToSpawm;
    public float originalMaxTimeToSpawm;
    public int originalMaxActiveMoles;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(gameObject);
        }
        GameObject[] molesGO = GameObject.FindGameObjectsWithTag("Mole");
        moles = molesGO.Select(mole => mole.GetComponent<MoleController>()).ToArray();

        originalMinTimeToSpawm = minTimeToSpawm;
        originalMaxTimeToSpawm = maxTimeToSpawm;
        originalMoleTimeUp = moleTimeUp;
        originalMaxActiveMoles = maxActiveMoles;


    }
    void Start() {
        InitGame();
    }

    public void InitGame() {
        score = 0;
        isGameRunning = true;
        UIManager.Instance.Init();
        StartCoroutine(SpawnMoleRoutine());
        GetComponent<CountDownTimer>().StartTimer();

        moleTimeUp = originalMoleTimeUp;
        minTimeToSpawm = originalMinTimeToSpawm;
        maxTimeToSpawm = originalMaxTimeToSpawm;
        maxActiveMoles = originalMaxActiveMoles;
    }
    public void UpdateScore() {
        score++;
        UIManager.Instance.SetScoreText(score.ToString());
    }


    private IEnumerator SpawnMoleRoutine() {
        while (isGameRunning) {
            yield return new WaitForSeconds(Random.Range(minTimeToSpawm, maxTimeToSpawm));

            // Obtener un topo aleatorio que esté oculto
            MoleController randomMole = GetRandomHiddenMole();

            if (randomMole != null) {
                randomMole.GetComponent<MoleController>().GoesUp();
            }
        }
    }

    private MoleController GetRandomHiddenMole() {
        // Filtrar los topos que están ocultos
        var hiddenMoles = System.Array.FindAll(moles, mole => !mole.isOut);

        // Comprobar si no hay demasiados topos activos
        int activeCount = System.Array.FindAll(moles, mole => mole.isOut).Length;
        if (activeCount >= maxActiveMoles) {
            return null;
        }

        if (hiddenMoles.Length == 0) {
            return null;
        }

        // Devolver un topo oculto aleatorio
        return hiddenMoles[Random.Range(0, hiddenMoles.Length)];
    }

    public void GameFinished() {
        isGameRunning = false;
        StopAllCoroutines();
        UIManager.Instance.ShowFinalScore();
    }
    
}
