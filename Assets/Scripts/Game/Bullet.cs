using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting;

public class Bullet : MonoBehaviour, IPooledObjects
{
    public enum User
    {
        player, enemy
    }
    public User user;
    float speed = 8f;
    Vector3 Direction;
    TurretController turret;
    Transform target;
    public void OnObjectSpawn()
    {
        transform.GetChild(0).gameObject.SetActive(true);
        
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
    }
    void Update()
    {
        transform.position += Direction.normalized * speed * Time.deltaTime;
    }
    public void setUser(User _user)
    {
        user = _user;
    }
    public User getUser()
    {
        return user;
    }
    public void SetDirection(Vector3 direction)
    {
        Direction = direction;
    }

}
