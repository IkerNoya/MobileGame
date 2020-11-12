using System;
using System.Collections.Generic;
using UnityEngine;

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

    public static event Action<PlayerController> Win;
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

        horizontalRotation = InputManager.Instance.GetAxis("Horizontal_1");
        transform.position += transform.forward * InputManager.Instance.GetAxis("Vertical_1") * Time.deltaTime * speed;
        if (horizontalRotation != 0)
            transform.Rotate(new Vector3(0, horizontalRotation * rotSpeed * Time.deltaTime, 0));
        if (InputManager.Instance.GetAxis("Vertical_1") > 0 || InputManager.Instance.GetAxis("Vertical_1") < 0) canShoot = false;
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
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("WinPlatform"))
        {
            Win?.Invoke(this);
        }
    }
    private void OnDisable()
    {
        ShellExplosion.Hit_Player -= TakeDamage;
    }
    public int GetHP()
    {
        return hp;
    }
}
