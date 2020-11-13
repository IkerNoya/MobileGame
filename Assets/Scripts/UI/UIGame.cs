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

}
