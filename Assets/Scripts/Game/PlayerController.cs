using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Transform target = null;
    [Space]
    [SerializeField] float speed;
    [SerializeField] float rotSpeed;
    Vector3 movement;

    void Update()
    {
        movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        transform.position += movement * Time.deltaTime * speed; 
    }
    void LateUpdate()
    {
        if(movement!=Vector3.zero)
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), rotSpeed);
    }
}
