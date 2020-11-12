using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject winScreen;
    [SerializeField] List<GameObject> enemies = new List<GameObject>();
    void Start()
    {
        PlayerController.Win += ActivateWinScreen;
        DeactivateWinScreen();
        for (int i = 0; i < 8; i++)
        {
            enemies.Add(GameObject.FindGameObjectWithTag("Enemy"));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnLevelLoad()
    {
        
    }

    void DeactivateWinScreen()
    {
        winScreen.SetActive(false);
    }

    void ActivateWinScreen(PlayerController pc)
    {
        winScreen.SetActive(true);
    }

    void OnDisable()
    {
        PlayerController.Win -= ActivateWinScreen;
    }
}
