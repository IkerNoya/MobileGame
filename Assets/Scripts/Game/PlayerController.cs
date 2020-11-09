using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float rotSpeed;
    [SerializeField] GameObject aimIndicator;
    [SerializeField] int hp = 100;
    [SerializeField] ParticleSystem tankExplosion;
    [SerializeField] int damage;

    GameObject tank;
    BoxCollider bCollider;

    float horizontalRotation;
    bool canShoot = false;
    bool isDead = false;
    private void Awake()
    {
        ShellExplosion.Hit_Player += TakeDamage;
    }
    private void Start()
    {
        tank = GameObject.FindGameObjectWithTag("Tank");
        bCollider = GetComponent<BoxCollider>();
    }
    void Update()
    {
        if (isDead)
            return;

        horizontalRotation = Input.GetAxis("Horizontal");
        transform.position += transform.forward * Input.GetAxis("Vertical") * Time.deltaTime * speed;
        if (horizontalRotation != 0)
            transform.Rotate(new Vector3(0, horizontalRotation * rotSpeed * Time.deltaTime, 0));
        if (Input.GetAxis("Vertical") > 0 || Input.GetAxis("Vertical") < 0) canShoot = false;
        else canShoot = true;

    }
    public bool GetShootBool()
    {
        return canShoot;
    }
    public GameObject GetAimIndicator()
    {
        return aimIndicator;
    }
    void Respawn()
    {
        isDead = false;
        tank.SetActive(true);
        bCollider.enabled = false;
        hp = 100;
    }
    void TakeDamage(ShellExplosion explotion)
    {
        hp -= damage;
        if (hp <= 0)
        {
            isDead = true;
            tank.SetActive(false);
            bCollider.enabled = false;
            if (tankExplosion != null) tankExplosion.Play();
        }
    }
    private void OnDisable()
    {
        ShellExplosion.Hit_Player -= TakeDamage;
    }
}
