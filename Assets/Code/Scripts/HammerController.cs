using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerController : MonoBehaviour {

    public Vector2 mouseOffset;  // Desplazamiento para que el mazo no est� exactamente sobre el rat�n

    Animator animator;
    private Camera cam;

    private void Start() {
        cam = Camera.main;
        animator = GetComponent<Animator>();

    }

    private void Update() {
        // Convertir la posici�n del rat�n a posici�n del mundo
        Vector3 mousePos = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cam.nearClipPlane));

        // A�adir el offset a la posici�n
        mousePos += new Vector3(mouseOffset.x, mouseOffset.y, 0);

        // Mover el objeto a la nueva posici�n con offset
        transform.position = new Vector3(mousePos.x, mousePos.y, transform.position.z);

        // Ejecutar animaci�n del mazo
        if (Input.GetMouseButtonDown(0) && GameManager.Instance.isGameRunning) {
            animator.SetTrigger("Hit");
            AudioManager.Instance.PlayHammerHitAudio();
        }
    }
}
