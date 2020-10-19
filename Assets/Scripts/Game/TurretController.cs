using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] GameObject shootingPoint;
    [SerializeField] GameObject bullet;
    [SerializeField] float rotationSpeed;
    GameObject tank;
    PlayerController player;
    bool canRotate = false;
    float timer = 0;
    float timeLimit = 1.5f;
    void Start()
    {
        tank = GameObject.FindGameObjectWithTag("Player");
        player = tank.GetComponent<PlayerController>();
    }

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit))
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
            if (timer >= timeLimit)
            {
                Instantiate(bullet, shootingPoint.transform.position, Quaternion.identity);
                timer = 0;
            }
        }
        else if(target==null || !player.GetShootBool())
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
    public Transform GetTarget()
    {
        return target;
    }
}
