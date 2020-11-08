using UnityEngine;

public class ShellExplosion : MonoBehaviour
{
    [SerializeField] LayerMask m_TankMask;
    [SerializeField] ParticleSystem m_ExplosionParticles; 
    [SerializeField] AudioSource m_ExplosionAudio;
    [SerializeField] GameObject bullet;
    [SerializeField] float m_MaxDamage = 100f;                  
    [SerializeField] float m_ExplosionForce = 1000f;            
    [SerializeField] float m_MaxLifeTime = 1f;                  
    [SerializeField] float m_ExplosionRadius = 5f;
    Bullet bulletScript;


    private void Start()
    {
        bulletScript = GetComponent<Bullet>();
        //Destroy(gameObject, m_MaxLifeTime);
    }


    private void OnTriggerEnter(Collider other)
    {
        // Find all the tanks in an area around the shell and damage them.
    }
    private void OnCollisionEnter(Collision collision)
    {
        if((collision.gameObject.CompareTag("Player") && bulletScript.getUser() != Bullet.User.player) || (collision.gameObject.CompareTag("Enemy") && bulletScript.getUser() != Bullet.User.enemy))
        {
            m_ExplosionParticles.Play();
            m_ExplosionAudio.Play();
            Destroy(gameObject, m_MaxLifeTime);
            Destroy(bullet);
        }
    }


    private float CalculateDamage(Vector3 targetPosition)
    {
        // Calculate the amount of damage a target should take based on it's position.
        return 0f;
    }
}