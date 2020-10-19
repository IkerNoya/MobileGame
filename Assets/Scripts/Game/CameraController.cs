using UnityEngine;

public class CameraController : MonoBehaviour   
{
    [SerializeField] Transform target;
    [SerializeField] float speed;
    [Space]
    [SerializeField] Vector3 offset;

    void Update()
    {
        transform.position = target.position - offset;
    }
    void LateUpdate()
    {
        transform.rotation = Quaternion.LookRotation(target.position - transform.position, Vector3.up);
    }
}
