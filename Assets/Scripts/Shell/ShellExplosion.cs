using JetBrains.Annotations;
using System;
using UnityEngine;

public class ShellExplosion : MonoBehaviour
{
    [SerializeField] LayerMask m_TankMask;
    [SerializeField] ParticleSystem m_ExplosionParticles;
    [SerializeField] AudioSource m_ExplosionAudio;
    [SerializeField] GameObject bullet;
    [SerializeField] float m_ExplosionForce = 1000f;
    [SerializeField] float m_MaxLifeTime = 3f;
    float hitLifeTime = 1f;
    Bullet bulletScript;
    public static event Action<ShellExplosion> Hit_Player;
    SphereCollider sc;

    private void Start()
    {
        bulletScript = GetComponent<Bullet>();
        sc = GetComponent<SphereCollider>();
    }
    void Update()
    {
        if (bullet.activeSelf)
            sc.enabled = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if ((other.gameObject.CompareTag("Player") && bulletScript.getUser() != Bullet.User.player))
        {
            m_ExplosionParticles.Play();
            m_ExplosionAudio.Play();
            bullet.SetActive(false);
            sc.enabled = false;
            bullet.SetActive(false);
            Handheld.Vibrate();
            if (Hit_Player != null) Hit_Player(this);
        }
        else if ((other.gameObject.CompareTag("Enemy") && bulletScript.getUser() != Bullet.User.enemy))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null) enemy.TakeDamage();
            m_ExplosionParticles.Play();
            m_ExplosionAudio.Play();
            bullet.SetActive(false);
            sc.enabled = false;
        }
        else if (other.gameObject.CompareTag("Enviroment"))
        {
            m_ExplosionParticles.Play();
            m_ExplosionAudio.Play();
            bullet.SetActive(false);
            sc.enabled = false;
        }
    }
    public void SetCollider(bool value)
    {
        sc.enabled = value;
    }
    
}