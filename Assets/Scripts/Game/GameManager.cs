using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject winScreen;
    [SerializeField] LevelManager levelManager;
    int levelsCompleted = 0;
    GameObject player;
    #region SINGLETON
    public static GameManager instance;
    void Awake()
    {
        instance = this;
    }
    #endregion
    void Start()
    {
        PlayerController.WinPlayformCollision += CheckWinScreen;
        player = GameObject.FindGameObjectWithTag("Player");
        DeactivateWinScreen();
        StartLevel();
    }

    void Update()
    {
        
    }

    void StartLevel()
    {
        int enemyAmmount = 1 + levelsCompleted;
        if(enemyAmmount>4)
            enemyAmmount = Random.Range(4, 7);
        levelManager.LoadLevel(enemyAmmount);
        player.GetComponent<PlayerController>().Respawn();
    }

    public void OnClickContinue()
    {
        StartLevel();
        DeactivateWinScreen();
    }

    void DeactivateWinScreen()
    {
        winScreen.SetActive(false);
    }

    void CheckWinScreen(PlayerController pc)
    {
        if (levelManager.GetActiveEnemies() <= 0)
        {
            levelsCompleted++;
            winScreen.SetActive(true);
        }
    }

    public int GetLevelsCompleted()
    {
        return levelsCompleted;
    }

    void OnDisable()
    {
        PlayerController.WinPlayformCollision -= CheckWinScreen;
    }
}
