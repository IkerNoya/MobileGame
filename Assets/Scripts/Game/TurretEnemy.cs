using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretEnemy : TurretController
{
    [SerializeField] GameObject tank;
    Bullet bulletScript;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        bulletScript = bullet.GetComponent<Bullet>();
    }
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= timeLimit+1f)
        {
            bulletScript.setUser(Bullet.User.enemy);
            Instantiate(bullet, shootingPoint.transform.position, Quaternion.identity);
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
