﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float rotSpeed;
    float horizontalRotation;

    void Update()
    {
        horizontalRotation = Input.GetAxis("Horizontal");
        transform.position += transform.forward * Input.GetAxis("Vertical") * Time.deltaTime * speed;
        if (horizontalRotation != 0)
            transform.Rotate(new Vector3(0, horizontalRotation * rotSpeed * Time.deltaTime, 0));
    }

}
