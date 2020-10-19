using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretPlayer : TurretController
{
    PlayerController player;
    GameObject tank;
    Bullet bulletScript;
    void Start()
    {
        tank = GameObject.FindGameObjectWithTag("Player");
        player = tank.GetComponent<PlayerController>();
        bulletScript = bullet.GetComponent<Bullet>();
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                if (hit.collider.CompareTag("Enemy"))
                {
                    target = hit.collider.transform;
                }
            }
        }
        if (target != null && player.GetShootBool())
        {
            timer += Time.deltaTime;
            canRotate = true;
            if (timer >= timeLimit - 0.5f)
            {
                bulletScript.setUser(Bullet.User.player);
                Instantiate(bullet, shootingPoint.transform.position, Quaternion.identity);
                timer = 0;
            }
        }
        else if (target == null || !player.GetShootBool())
        {
            canRotate = false;
            timer = 0;
        }
    }
    void LateUpdate()
    {
        if (canRotate)
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(target.position - transform.position), rotationSpeed * Time.deltaTime);
        else
            transform.rotation = Quaternion.Slerp(transform.rotation, tank.transform.rotation, rotationSpeed * Time.deltaTime);
    }
}
