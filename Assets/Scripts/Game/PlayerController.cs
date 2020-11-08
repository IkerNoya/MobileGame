using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float rotSpeed;
    float horizontalRotation;
    bool canShoot = false;
    int hp = 100;

    void Update()
    {
        horizontalRotation = Input.GetAxis("Horizontal");
        transform.position += transform.forward * Input.GetAxis("Vertical") * Time.deltaTime * speed;
        if (horizontalRotation != 0)
            transform.Rotate(new Vector3(0, horizontalRotation * rotSpeed * Time.deltaTime, 0));
        if (Input.GetAxis("Vertical") > 0 || Input.GetAxis("Vertical") < 0) canShoot = false;
        else canShoot = true;
    }
    public bool GetShootBool()
    {
        return canShoot;
    }

}
