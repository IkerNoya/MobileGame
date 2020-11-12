
using UnityEngine;
using UnityEngine.UI;

public class HPController : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] Image bar;
    [SerializeField] float zOffset;

    float degreePerSec = 180f;

    [SerializeField] GameObject player;
    [SerializeField] GameObject enemy;
    [SerializeField] PlayerController pc;
    [SerializeField] Enemy enemyScript;

    public enum User
    {
        player, enemy
    }
    public User user;

    Quaternion initialRot;
    Vector3 initialPos;
    void Awake()
    {
        initialRot = slider.transform.rotation;
        initialPos = slider.transform.position;
    }

    void Start()
    {
        switch (user)
        {
            case User.player:
                if(bar!=null) bar.color = Color.green;
                break;
            case User.enemy:
                if (bar != null) bar.color = Color.red;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch (user)
        {
            case User.player:
                if (slider != null) slider.value = pc.GetHP();
                if (player!=null) slider.transform.position = new Vector3(transform.position.x, slider.transform.position.y, transform.position.z - zOffset);
                break;
            case User.enemy:
                if (slider != null) slider.value = enemyScript.GetHP();
                if (enemy!=null) slider.transform.position = new Vector3(transform.position.x, slider.transform.position.y, transform.position.z - zOffset);
                break;
        }
    }

    void LateUpdate()
    {
        slider.transform.rotation = initialRot;
    }

}
