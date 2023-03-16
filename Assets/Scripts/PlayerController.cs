using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour
{

    public GameObject puerta;
    public float speed = 1;

    private Rigidbody rb;

    private float movementX;
    private float movementY;

    private int cointCount;
    public TextMeshProUGUI textoFin;

    // Start is called before the first frame update
    void Start()
    {
        // instanciando el objeto que contiene este script
        // la bola
        rb = GetComponent<Rigidbody>();
    }

    /**
     *  Evento Input System
     **/
    private void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
        // mensaje para la consola del Unity
        // Debug.Log("Estoy en OnMove ");
    }

    private void FixedUpdate()
    {
        // para el teclado
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        rb.AddForce(movement * speed);

        // recogemos los datos del acelerometro
        Vector3 dir = Vector3.zero;
        dir.x = -Input.acceleration.y;
        dir.z = Input.acceleration.x;
        if (dir.sqrMagnitude > 1)
            dir.Normalize();
        
        dir *= Time.deltaTime;
        transform.Translate(dir * speed);
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("choque");
        if (other.gameObject.CompareTag("PickUp"))
        {
            cointCount++;
            Debug.Log("Score: " + cointCount);
            other.gameObject.SetActive(false);
            if (cointCount == 8)
            {
                speed = speed*3;
            } 
        }

        if (other.gameObject.CompareTag("Malo"))
        {
            transform.position = new Vector3(0, 1, 0);
        }

        if (other.gameObject.CompareTag("Fin"))
        {
            textoFin.gameObject.SetActive(true);
        }
        
        

    }
    
    
 
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            puerta.SetActive(false);
        }
    }
    
    
}

