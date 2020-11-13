using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] int hp = 100;
    [SerializeField] int damage = 25;
    [SerializeField] GameObject tank;
    [SerializeField] ParticleSystem explotion;
    bool isDead = false;
    BoxCollider bCollider;
    void Start()
    {
        bCollider = GetComponent<BoxCollider>();
    }

    void Update()
    {
        if (isDead)
            return;
    }
    
    public void TakeDamage()
    {
        hp -= damage;
        if (hp <= 0)
        {
            tank.SetActive(false);
            explotion.Play();
            isDead = true;
            StartCoroutine(WaitForExplotion());
        }
    }

    public int GetHP()
    {
        return hp;
    }

    IEnumerator WaitForExplotion()
    {
        yield return new WaitForSeconds(1f);
        transform.position = new Vector3(100, 100, 100);
        tank.SetActive(true);
        gameObject.SetActive(false);
        hp = 100;
    }
}
