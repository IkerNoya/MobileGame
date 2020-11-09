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
    public static event Action<ShellExplosion> Hit_Enemy;
    SphereCollider sc;

    private void Start()
    {
        bulletScript = GetComponent<Bullet>();
        Destroy(gameObject, m_MaxLifeTime);
        sc = GetComponent<SphereCollider>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if ((other.gameObject.CompareTag("Player") && bulletScript.getUser() != Bullet.User.player))
        {
            m_ExplosionParticles.Play();
            m_ExplosionAudio.Play();
            Destroy(gameObject, hitLifeTime);
            Destroy(bullet);
            sc.enabled = false;
            if (Hit_Player != null) Hit_Player(this);
        }
        else if ((other.gameObject.CompareTag("Enemy") && bulletScript.getUser() != Bullet.User.enemy))
        {
            m_ExplosionParticles.Play();
            m_ExplosionAudio.Play();
            Destroy(gameObject, hitLifeTime);
            Destroy(bullet);
            sc.enabled = false;
            if (Hit_Enemy != null) Hit_Enemy(this);
        }
        else if (other.gameObject.CompareTag("Enviroment"))
        {
            m_ExplosionParticles.Play();
            m_ExplosionAudio.Play();
            Destroy(gameObject, hitLifeTime);
            Destroy(bullet);
            sc.enabled = false;
        }
    }
}