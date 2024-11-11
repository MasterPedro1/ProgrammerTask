using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject crosshairPrefab; // Prefab de la mira
    public GameObject projectilePrefab; // Prefab del proyectil
    public Transform firePoint; // Punto desde donde se disparará el proyectil
    public float projectileSpeed = 10f; // Velocidad del proyectil
    public float projectileLifetime = 3f; // Tiempo de vida de cada proyectil en segundos

    private GameObject crosshair; // Instancia de la mira
    private bool canShoot = false; // Controla si el jugador puede disparar

    private void Start()
    {
        // Crear la mira pero mantenerla desactivada inicialmente
        crosshair = Instantiate(crosshairPrefab);
        crosshair.SetActive(false);
    }

    private void Update()
    {
        // Actualizar la posición de la mira a la posición del mouse si está activada
        if (canShoot)
        {
            UpdateCrosshairPosition();
            if (Input.GetButtonDown("Fire1"))
            {
                Shoot();
            }
        }
    }

    private void UpdateCrosshairPosition()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        crosshair.transform.position = mousePosition;
    }

    private void Shoot()
    {
        // Crear el proyectil en el punto de disparo
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);

        // Calcular la dirección hacia el mouse
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePosition - (Vector2)firePoint.position).normalized;

        // Asignar la velocidad al proyectil
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        rb.velocity = direction * projectileSpeed;

        // Destruir el proyectil después de 3 segundos
        Destroy(projectile, projectileLifetime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Activar la mira cuando el jugador entra en el área del spawner
        if (collision.CompareTag("Spawner"))
        {
            canShoot = true;
            crosshair.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Desactivar la mira cuando el jugador sale del área del spawner
        if (collision.CompareTag("Spawner"))
        {
            canShoot = false;
            crosshair.SetActive(false);
        }
    }
}
