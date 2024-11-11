using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public GameObject StartG;
    public GameObject Lose;
    public GameObject Player;
    public PlayerController pc;
    void Start()
    {
        StartG.SetActive(true);
        Time.timeScale = 0;
    }

    private void Update()
    {
        if(pc.vida <= 0)
        {
            Lose.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void PlayG()
    {
        Time.timeScale = 1.0f;
        StartG.SetActive(false);
        Player.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene("Game");
    }

}
