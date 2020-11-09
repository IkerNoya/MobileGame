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
    void Awake()
    {
        ShellExplosion.Hit_Enemy += TakeDamage;
    }
    void Start()
    {
        bCollider = GetComponent<BoxCollider>();
    }

    void Update()
    {
        if (isDead)
            return;
    }
    
    void TakeDamage(ShellExplosion se)
    {
        hp -= damage;
        if (hp <= 0)
        {
            Destroy(tank);
            explotion.Play();
            isDead = true;
            bCollider.enabled = false;
            Destroy(gameObject, 1.5f);
        }
    }

    void OnDisable()
    {
        ShellExplosion.Hit_Enemy -= TakeDamage;
    }
}
