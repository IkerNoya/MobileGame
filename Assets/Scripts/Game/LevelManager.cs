using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] Vector2 GridSize;
    [SerializeField] Vector3 GridDimension;
    [SerializeField] Vector3 GridOrigin;

    [SerializeField] GameObject[] enemies;
    [SerializeField] GameObject[] props;

    float yInitialPos = 1.2f;

    void Start()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        props = GameObject.FindGameObjectsWithTag("Prop");
        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].SetActive(false);
        }
        for (int i = 0; i < props.Length; i++)
        {
            props[i].SetActive(false);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnProps();
        }
    }

    void SpawnProps()
    {
        int index = Random.Range(0, props.Length);
        int x = Random.Range(0, (int)GridSize.x);
        int z = Random.Range(0, (int)GridSize.y);
        props[index].transform.position = new Vector3((x * GridDimension.x) + GridOrigin.x, yInitialPos, (z * GridDimension.z) + GridOrigin.z);
        props[index].SetActive(true);
    }
}
