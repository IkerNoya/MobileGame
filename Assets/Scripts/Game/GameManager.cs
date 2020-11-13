using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        PlayerController.Loose += LooseGame;
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

    void LooseGame(PlayerController pc)
    {
        SceneManager.LoadScene("menu");
    }

    public int GetLevelsCompleted()
    {
        return levelsCompleted;
    }

    void OnDisable()
    {
        PlayerController.WinPlayformCollision -= CheckWinScreen;
        PlayerController.WinPlayformCollision -= LooseGame;
    }
}
