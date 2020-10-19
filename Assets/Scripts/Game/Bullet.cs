using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting;

public class Bullet : MonoBehaviour
{
    float speed = 8f;
    Vector3 direction;
    TurretController turret;
    Transform target;
    void Start()
    {
        turret = FindObjectOfType<TurretController>();
        target = turret.GetTarget();
        direction = target.transform.position - transform.position;
        transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
        Destroy(gameObject, 3.0f);
    }
    void Update()
    {
        transform.position += direction.normalized * speed * Time.deltaTime;
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }    
    }
}
