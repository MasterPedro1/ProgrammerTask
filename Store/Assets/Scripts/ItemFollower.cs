using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemFollower : MonoBehaviour
{
    private Transform player; // Referencia al Transform del jugador
    public float followSpeed = 3f; // Velocidad con la que el objeto sigue al jugador
    private bool startFollowing = false; // Controla si el objeto debe comenzar a seguir al jugador

    private void Start()
    {
        // Encontrar el transform del jugador
        PlayerController PlayerGO = FindAnyObjectByType<PlayerController>();
        player = PlayerGO.gameObject.transform;

        // Iniciar la corrutina para esperar antes de comenzar a seguir
        StartCoroutine(StartFollowingPlayer());
    }

    private IEnumerator StartFollowingPlayer()
    {
        // Esperar 1.5 segundos antes de comenzar a seguir al jugador
        yield return new WaitForSeconds(.7f);
        startFollowing = true;
    }

    private void Update()
    {
        // Si debe seguir al jugador, moverse hacia su posición
        if (startFollowing && player != null)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            transform.position = Vector2.MoveTowards(transform.position, player.position, followSpeed * Time.deltaTime);
        }
    }

    
}