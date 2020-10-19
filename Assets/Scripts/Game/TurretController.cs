using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float rotationSpeed;
    GameObject tank;
    bool canRotate = false;
    void Start()
    {
        tank = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit))
        {
            if (hit.collider.CompareTag("Enemy") && Input.GetKeyDown(KeyCode.Mouse0))
            {
                target = hit.collider.transform;
                Debug.Log("TARGET ADQUIRED");
            }
        }
        if (target != null) canRotate = true;
        else canRotate = false;
       
    }
    void LateUpdate()
    {
        if (canRotate)
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(target.position - transform.position), rotationSpeed * Time.deltaTime);
        else
            transform.rotation = Quaternion.Slerp(transform.rotation, tank.transform.rotation, rotationSpeed * Time.deltaTime);
    }
}
