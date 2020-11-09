using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPController : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] float zOffset;

    float degreePerSec = 180f;

    GameObject player;
    PlayerController pc;

    Quaternion initialRot;
    Vector3 initialPos;
    void Awake()
    {
        initialRot = transform.rotation;
        initialPos = transform.position;
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null) pc = player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (slider != null) slider.value = pc.GetHP();
        transform.position = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z - zOffset);
    }

    void LateUpdate()
    {
        transform.rotation = initialRot;
    }

}
