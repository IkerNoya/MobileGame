using System;
using System.Collections;
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
    Vector3 initialPos;
    Quaternion initialRot;

    float horizontalRotation;
    bool canShoot = false;
    bool isDead = false;

    public static event Action<PlayerController> WinPlayformCollision;
    public static event Action<PlayerController> Loose;
    private void Awake()
    {
        ShellExplosion.Hit_Player += TakeDamage;
    }
    private void Start()
    {
        initialPos = transform.position;
        initialRot = transform.rotation;
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
    public void Respawn()
    {
        transform.position = initialPos;
        transform.rotation = initialRot;
        isDead = false;
        GetComponent<Rigidbody>().useGravity = true;
        tank.SetActive(true);
        bCollider.enabled = true;
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
            GetComponent<Rigidbody>().useGravity = false;
            if (tankExplosion != null) tankExplosion.Play();
            StartCoroutine(LooseGameEvent(2));
            
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("WinPlatform"))
        {
            WinPlayformCollision?.Invoke(this);
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
    IEnumerator LooseGameEvent(float time)
    {
        yield return new WaitForSeconds(time);
        Loose?.Invoke(this);
        yield return null;
    }
}
