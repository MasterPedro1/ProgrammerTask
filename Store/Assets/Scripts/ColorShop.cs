using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class ColorShop : MonoBehaviour
{
    public SpriteRenderer playerSprite; // Referencia al SpriteRenderer del jugador

    // Definimos un botón y un texto para cada color
    public Button redButton;
    public TextMeshProUGUI redButtonText;
    public GameObject precior;
    public Button blueButton;
    public TextMeshProUGUI blueButtonText;
    public GameObject preciob;
    public Button greenButton;
    public TextMeshProUGUI greenButtonText;
    public GameObject preciog;
    public Button yellowButton;
    public TextMeshProUGUI yellowButtonText;
    public GameObject precioy;

    public Button healthButton; // Botón para aumentar la vida

    public Button sellMeatButton; // Botón para vender carne por oro

    public TextMeshProUGUI Meat;
    public TextMeshProUGUI Gold;

    private string equippedColor = ""; // Almacena el color actualmente equipado
    private int playerGold = 15; // Ejemplo de oro inicial, se puede ajustar o enlazar con PlayerController
    private PlayerController playerController; // Referencia al PlayerController
    private bool isSelling = false; // Controla si se está vendiendo carne continuamente
    public float sellInterval = 0.2f; // Intervalo de tiempo entre cada venta continua de carne

    private void Start()
    {
        // Inicializar botones con el texto "Comprar"
        redButtonText.text = "Comprar";
        blueButtonText.text = "Comprar";
        greenButtonText.text = "Comprar";
        yellowButtonText.text = "Comprar";

        // Obtener la referencia a PlayerController
        playerController = FindObjectOfType<PlayerController>();

        // Agregar listeners a los botones
        redButton.onClick.AddListener(() => HandleColorButtonClick("Red", 25, redButtonText, Color.red, precior));
        blueButton.onClick.AddListener(() => HandleColorButtonClick("Blue", 50, blueButtonText, Color.blue, preciob));
        greenButton.onClick.AddListener(() => HandleColorButtonClick("Green", 50, greenButtonText, Color.green, preciog));
        yellowButton.onClick.AddListener(() => HandleColorButtonClick("Yellow", 50, yellowButtonText, Color.yellow, precioy));

        // Listener para aumentar vida
        healthButton.onClick.AddListener(IncreaseHealth);

        // Listener para vender carne por oro (presionar y mantener)
        sellMeatButton.onClick.AddListener(SellMeatForGold);

    }
    private void Update()
    {
        // Si se está vendiendo continuamente, ejecutar la venta cada intervalo
        Meat.text = "X " + playerController.carne.ToString();
        Gold.text = "X " + playerGold.ToString();
        if (isSelling)
        {
            SellMeatForGold();
        }
    }

    private void HandleColorButtonClick(string colorName, int price, TextMeshProUGUI buttonText, Color color, GameObject priceDisplay)
    {
        if (buttonText.text == "Comprar")
        {
            // Verificar si el jugador tiene suficiente oro
            if (playerGold >= price)
            {
                playerGold -= price;
                buttonText.text = "Equipar";
                priceDisplay.SetActive(false);
                Debug.Log($"{colorName} comprado por {price} de oro. Oro restante: {playerGold}");
            }
            else
            {
                Debug.Log("No tienes suficiente oro.");
            }
        }
        else if (buttonText.text == "Equipar")
        {
            EquipColor(buttonText, color, colorName);
        }
        else if (buttonText.text == "Quitar")
        {
            UnequipColor(buttonText);
        }
    }

    private void EquipColor(TextMeshProUGUI buttonText, Color color, string colorName)
    {
        // Cambia el color del sprite del jugador al seleccionado
        playerSprite.color = color;

        // Cambia el texto del botón a "Quitar"
        buttonText.text = "Quitar";

        // Restablece otros botones a "Equipar" si tienen el estado "Quitar"
        UpdateOtherButtons(colorName);

        // Actualiza el color actualmente equipado
        equippedColor = colorName;
    }

    private void UnequipColor(TextMeshProUGUI buttonText)
    {
        // Cambia el color del sprite del jugador a blanco
        playerSprite.color = Color.white;

        // Cambia el texto del botón a "Equipar"
        buttonText.text = "Equipar";

        // Elimina el color actualmente equipado
        equippedColor = "";
    }

    private void UpdateOtherButtons(string colorName)
    {
        // Cambia los botones que no están equipados a "Equipar" si estaban en "Quitar"
        if (colorName != "Red" && redButtonText.text == "Quitar")
        {
            redButtonText.text = "Equipar";
        }
        if (colorName != "Blue" && blueButtonText.text == "Quitar")
        {
            blueButtonText.text = "Equipar";
        }
        if (colorName != "Green" && greenButtonText.text == "Quitar")
        {
            greenButtonText.text = "Equipar";
        }
        if (colorName != "Yellow" && yellowButtonText.text == "Quitar")
        {
            yellowButtonText.text = "Equipar";
        }
    }

    private void IncreaseHealth()
    {
        // Aumentar la vida si el jugador tiene al menos 50 de oro
        if (playerGold >= 50)
        {
            if (playerController.vida < 40)
            {
                playerGold -= 50;
                playerController.vida += 40;
                Debug.Log("Vida aumentada en 40. Oro restante: " + playerGold);

            }
        }
        else
        {
            Debug.Log("No tienes suficiente oro para aumentar la vida.");
        }
    }

    private void SellMeatForGold()
    {
        // Vender 2 de carne por 1 de oro si se mantiene presionado el botón
        if (playerController.carne >= 2)
        {
            playerController.carne -= 2;
            playerGold += 1;
            Debug.Log("2 de carne vendidos por 1 de oro. Oro total: " + playerGold + ", Carne restante: " + playerController.carne);
        }
        else
        {
            Debug.Log("No tienes suficiente carne para vender.");
            isSelling = false; // Detener la venta si no hay suficiente carne
        }
    }


}