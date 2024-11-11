using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f; // Velocidad de movimiento del jugador
    private Rigidbody2D rb;
    private Vector2 movement;
    private SpriteRenderer spriteRenderer; // Referencia al SpriteRenderer
    [HideInInspector]
    public float carne = 0;
    public TextMeshProUGUI carneCount;
    public float vida = 40;
    public Slider health;

    private void Start()
    {
        // Obtener el componente Rigidbody2D y SpriteRenderer
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        // Obtener la entrada de movimiento
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // Normalizar el vector de movimiento para que la velocidad sea constante en diagonales
        movement = movement.normalized;

        // Llamar al método para manejar la dirección del sprite
        FlipSprite();
        carneCount.text = "X " + carne;
        health.value = vida;

    }

    private void FixedUpdate()
    {
        // Aplicar movimiento al jugador
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    private void FlipSprite()
    {
        // Verificar la dirección del movimiento en el eje X
        if (movement.x < 0)
        {
            // Si se mueve hacia la izquierda, activar el flip en X
            spriteRenderer.flipX = true;
        }
        else if (movement.x > 0)
        {
            // Si se mueve hacia la derecha, desactivar el flip en X
            spriteRenderer.flipX = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Carne"))
        {
            carne = carne + 1;
            Destroy(collision.gameObject);
        }
        

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Enemy"))
        {
            vida = vida - 1;
        }
    }
}