using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerController : MonoBehaviour {

    public Vector2 mouseOffset;  // Desplazamiento para que el mazo no esté exactamente sobre el ratón

    Animator animator;
    private Camera cam;

    private void Start() {
        cam = Camera.main;
        animator = GetComponent<Animator>();

    }

    private void Update() {
        // Convertir la posición del ratón a posición del mundo
        Vector3 mousePos = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cam.nearClipPlane));

        // Añadir el offset a la posición
        mousePos += new Vector3(mouseOffset.x, mouseOffset.y, 0);

        // Mover el objeto a la nueva posición con offset
        transform.position = new Vector3(mousePos.x, mousePos.y, transform.position.z);

        // Ejecutar animación del mazo
        if (Input.GetMouseButtonDown(0) && GameManager.Instance.isGameRunning) {
            animator.SetTrigger("Hit");
            AudioManager.Instance.PlayHammerHitAudio();
        }
    }
}
