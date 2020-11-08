using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    [SerializeField] protected Transform target;
    [SerializeField] protected GameObject shootingPoint;
    [SerializeField] protected GameObject bullet;
    [SerializeField] protected float rotationSpeed;
    protected ParticleSystem flash;
    protected bool canRotate = false;
    protected float timer = 0;
    protected float timeLimit = 1.5f;

    public Transform GetTarget()
    {
        return target;
    }
}
