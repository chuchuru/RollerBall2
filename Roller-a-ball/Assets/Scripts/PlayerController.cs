using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public TMP_Text countText;
    public TMP_Text winText;
    public float speed = 10.0f;
    private Rigidbody rb;
    public GameObject restartButton; // Bot�n para reiniciar

    private int count; // Puntuaci�n

    // Movimiento
    private float movementX;
    private float movementY;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;

        // Verifica si est�s en la primera escena
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            SetCountText();
            winText.gameObject.SetActive(false);

            if (restartButton != null)
                restartButton.SetActive(false); // Oculta el bot�n en la primera escena
        }
        else
        {
            // Si est�s en la segunda escena, activa el texto y el bot�n
            if (winText != null)
            {
                winText.gameObject.SetActive(true);
                winText.text = "You Win!";
            }

            if (restartButton != null)
                restartButton.SetActive(true); // Muestra el bot�n
        }
    }

    void OnMove(InputValue movementValue)
    {
        // Obt�n los valores de movimiento en x y y
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        rb.AddForce(movement * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (SceneManager.GetActiveScene().buildIndex == 0 && other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count++; // Incrementa la puntuaci�n
            SetCountText();
        }
    }

    void SetCountText()
    {
        countText.text = "Player count: " + count.ToString() + "/12";
        if (count >= 12)
        {
            // Cambia a la segunda escena
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    public void RestartGame()
    {
        // Reinicia desde la primera escena
        SceneManager.LoadScene(0);
    }
}
