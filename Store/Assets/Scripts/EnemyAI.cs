using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform player; // Referencia al Transform del jugador
    public float pursuitSpeed = 5f; // Velocidad de persecución
    public float arrivalSpeed = 2f; // Velocidad cuando el enemigo está cerca del jugador
    public float arrivalRadius = 3f; // Distancia en la cual el enemigo empieza a reducir la velocidad
    public float health = 3f; // Vida del enemigo
    public GameObject itemDropPrefab; // Prefab del objeto que se va a spawnear al morir

    private Rigidbody2D rb;

    private void Start()
    {
        // Obtener el componente Rigidbody2D del enemigo
        rb = GetComponent<Rigidbody2D>();
        PlayerController PlayerGO = FindAnyObjectByType<PlayerController>();
        player = PlayerGO.gameObject.transform;
    }

    private void Update()
    {
        PursuePlayer();

        if (health <= 0)
        {
            // Spawnear el objeto al morir
            if (itemDropPrefab != null)
            {
                Instantiate(itemDropPrefab, transform.position, Quaternion.identity);
            }
            Destroy(this.gameObject);
        }
    }

    private void PursuePlayer()
    {
        // Calcular la dirección hacia el jugador
        Vector2 direction = (player.position - transform.position).normalized;
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // Ajustar la velocidad según la distancia al jugador
        float speed = (distanceToPlayer <= arrivalRadius) ? arrivalSpeed : pursuitSpeed;

        // Mover el enemigo hacia el jugador
        rb.velocity = direction * speed;

        // Rotar el enemigo para que mire hacia el jugador (opcional)
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rb.rotation = angle;
    }

    private void OnDisable()
    {
        // Detener el movimiento cuando el enemigo es desactivado
        rb.velocity = Vector2.zero;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bala"))
        {
            Destroy(collision.gameObject);
            health--;
        }
    }
}
