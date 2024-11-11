using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteraction : MonoBehaviour
{
    // Panel o UI de la tienda que queremos mostrar
    public GameObject shopUIPanel;
    public GameObject shopUIPanel2;
    public GameObject Player;
    private bool isShopOpen = false;

    // M�todo que detecta cuando el jugador entra en el �rea de interacci�n
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verificamos si el objeto que entr� es el jugador
        if (collision.CompareTag("Player") && !isShopOpen)
        {
            // Activar la UI de la tienda
            OpenShop();
            Time.timeScale = 0f;
        }
    }
    
    

    // M�todo para abrir la tienda
    private void OpenShop()
    {
        Player.SetActive(false);
        shopUIPanel.SetActive(true);
        isShopOpen = true;
        Debug.Log("Tienda abierta: �Compra o vende art�culos!");
    }

    // M�todo para cerrar la tienda
    public void CloseShop()
    {
        Player.SetActive(true);
        shopUIPanel2.SetActive(false);
        isShopOpen = false;
        Debug.Log("Tienda cerrada");
        Time.timeScale = 1.0f;
    }

    public void CloseText()
    {
        shopUIPanel2.SetActive(true);
        shopUIPanel.SetActive(false);
        
    }
}
