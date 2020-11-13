using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject winScreen;

    void Start()
    {
        PlayerController.Win += ActivateWinScreen;
        DeactivateWinScreen();
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
