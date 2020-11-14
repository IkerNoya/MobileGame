using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITutorial : MonoBehaviour
{
    [SerializeField] GameObject page1;
    [SerializeField] GameObject page2;

    void Start()
    {
        page1.SetActive(true);
        page2.SetActive(false);
    }
    public void OnClickNext(int page)
    {
        if (page == 1)
        {
            page1.SetActive(true);
            page2.SetActive(false);
        }
        else if (page == 2)
        {
            page1.SetActive(false);
            page2.SetActive(true);
        }
    }
}
