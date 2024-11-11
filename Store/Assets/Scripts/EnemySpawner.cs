using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // Prefab del enemigo que vamos a spawnear
    public Transform[] spawnPoints; // Array de puntos de spawn
    public float spawnInterval = 3f; // Intervalo de spawn (cada 3 segundos)

    private List<GameObject> activeEnemies = new List<GameObject>(); // Lista para almacenar enemigos activos
    private bool isSpawning = false; // Controla si el spawn está activo
    private Coroutine spawnCoroutine; // Referencia a la corrutina de spawn

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isSpawning)
        {
            StartSpawning();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StopSpawning();
        }
    }

    private void StartSpawning()
    {
        isSpawning = true;

        // Generar un enemigo en cada punto de spawn al inicio
        foreach (Transform spawnPoint in spawnPoints)
        {
            SpawnEnemy(spawnPoint.position);
        }

        // Iniciar la corrutina para spawnear un enemigo cada 3 segundos
        spawnCoroutine = StartCoroutine(SpawnEnemies());
    }

    private void StopSpawning()
    {
        isSpawning = false;

        // Detener la corrutina de spawn
        if (spawnCoroutine != null)
        {
            StopCoroutine(spawnCoroutine);
        }

        // Eliminar todos los enemigos activos
        foreach (GameObject enemy in activeEnemies)
        {
            Destroy(enemy);
        }

        activeEnemies.Clear(); // Limpiar la lista de enemigos activos
    }

    private void SpawnEnemy(Vector2 position)
    {
        // Instanciar un enemigo en la posición dada y añadirlo a la lista de enemigos activos
        GameObject enemy = Instantiate(enemyPrefab, position, Quaternion.identity);
        activeEnemies.Add(enemy);
    }

    private IEnumerator SpawnEnemies()
    {
        while (isSpawning)
        {
            yield return new WaitForSeconds(spawnInterval);

            // Elegir un spawnpoint aleatorio y generar un enemigo allí
            Transform randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            SpawnEnemy(randomSpawnPoint.position);
        }
    }
}

