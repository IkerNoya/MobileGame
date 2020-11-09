using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretEnemy : TurretController
{
    [SerializeField] GameObject tank;
    Bullet bulletScript;
    ObjectPooler pool;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        bulletScript = bullet.GetComponent<Bullet>();
        flash = GetComponentInChildren<ParticleSystem>();
        pool = ObjectPooler.instance;
    }
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= timeLimit+1f)
        {
            flash.Play();
            pool.SpawnBulletsFromPool("Bullet_Enemy", shootingPoint.transform.position, Quaternion.identity, Bullet.User.enemy, target.position - transform.position);
            timer = 0;
        }
    }
    void LateUpdate()
    {
        //if (canRotate)
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(target.position - transform.position), rotationSpeed * Time.deltaTime);
        //else
        //    transform.rotation = Quaternion.Slerp(transform.rotation, tank.transform.rotation, rotationSpeed * Time.deltaTime);
    }
}
