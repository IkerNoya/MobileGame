using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting;

public class Bullet : MonoBehaviour
{
    public enum User
    {
        player, enemy
    }
    public User user;
    float speed = 8f;
    Vector3 direction;
    TurretController turret;
    Transform target;
    void Start()
    {
        switch (user)
        {
            case User.player:
                turret = FindObjectOfType<TurretPlayer>();
                break;
            case User.enemy:
                turret = FindObjectOfType<TurretEnemy>();
                break;
        }
        target = turret.GetTarget();
        direction = target.transform.position - transform.position;
        transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
        Destroy(gameObject, 3.0f);
    }
    void Update()
    {
        transform.position += direction.normalized * speed * Time.deltaTime;
    }
    public void setUser(User _user)
    {
        user = _user;
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Enemy") && user==User.player)
        {
            Destroy(gameObject);
        }
        if (collision.collider.CompareTag("Player") && user == User.enemy)
        {
            Destroy(gameObject);
        }
    }
}
