using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoleController : MonoBehaviour {

    Animator animator;
    Collider2D collider;
    public bool isOut = false;
    
    private void Awake() {
        animator = GetComponent<Animator>();
        collider = GetComponent<Collider2D>();
    }

    
    public void GoesUp() {
        Debug.Log("Saliendo");
        isOut = true;
        animator.SetTrigger("Show");
        StartCoroutine(HideInSeconds());
        collider.enabled = true;
    }

    public void GoesDown() {
        Debug.Log("Ocultando");
        animator.SetTrigger("Hide");
        collider.enabled = false;
    }

    public void GetHit() {
        Debug.Log("Golpeando");
        isOut = false;
        animator.SetTrigger("Hit");
        collider.enabled = false;
        GameManager.Instance.UpdateScore();
        AudioManager.Instance.PlayPainAudio();
        StopAllCoroutines();

    }

    private IEnumerator HideInSeconds() {
        yield return new WaitForSeconds(GameManager.Instance.moleTimeUp);
        GoesDown();
        yield return new WaitForSeconds(0.5f);
        isOut = false;
    }

    private void OnMouseDown() {
        if (isOut && GameManager.Instance.isGameRunning) {
            GetHit();
        }
    }
}
