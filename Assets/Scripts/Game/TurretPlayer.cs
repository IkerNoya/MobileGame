using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.Rendering;

public class TurretPlayer : TurretController
{
    PlayerController player;
    GameObject tank;
    Bullet bulletScript;
    GameObject aim;
    [SerializeField]float orbitDistance=10f;
    [SerializeField]float orbitDegreePerSecond = 180f;

    void Start()
    {
        tank = GameObject.FindGameObjectWithTag("Player");
        player = tank.GetComponent<PlayerController>();
        bulletScript = bullet.GetComponent<Bullet>();
        flash = GetComponentInChildren<ParticleSystem>();
        aim = player.GetAimIndicator();
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
                if(bulletScript!=null) bulletScript.setUser(Bullet.User.player);
                if(flash!=null) flash.Play();
                if(bullet!=null) Instantiate(bullet, shootingPoint.transform.position, Quaternion.identity);
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
        if (target != null)
        {
            Vector3 direction = target.position - transform.position;
            if (canRotate)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotationSpeed * Time.deltaTime);
            }
            else
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, tank.transform.rotation, rotationSpeed * Time.deltaTime);
            }
            OrbitAimIndicator(direction);
        }
        else
        {
            OrbitAimIndicator(tank.transform.forward);
        }
    }
    void OrbitAimIndicator(Vector3 direction)
    {
        if (target != null)
        {
            aim.transform.position = transform.position + (aim.transform.position - transform.position).normalized * orbitDistance;
            //aim.transform.RotateAround(transform.position, Vector3.up, orbitDegreePerSecond * Time.deltaTime);
            aim.transform.position = Vector3.RotateTowards(transform.position, transform.position + (direction.normalized * orbitDistance), 360f ,orbitDegreePerSecond * Time.deltaTime);
            aim.transform.rotation = Quaternion.LookRotation(direction.normalized, Vector3.up);
        }
    }
}
