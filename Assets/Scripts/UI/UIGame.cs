using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIGame : MonoBehaviour
{
    [SerializeField] Text levelAmmount;
    void Update()
    {
        levelAmmount.text = GameManager.instance.GetLevelsCompleted().ToString();
    }

    public void OnClickMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void OnClickQuit()
    {
        Application.Quit();
    }
}
