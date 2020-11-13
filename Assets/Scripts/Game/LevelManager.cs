using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] Vector2 GridSize;
    [SerializeField] Vector3 GridDimension;
    [SerializeField] Vector3 GridOrigin;

    [SerializeField] GameObject[] enemies;
    [SerializeField] GameObject[] props;

    List<Vector2> GridPositions = new List<Vector2>();

    float yInitialPos = 0;

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

    }
    void FillVectorList()
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                GridPositions.Add(new Vector2(i, j));
            }
        }
    }

    void SpawnProps()
    {
        for (int i = 0; i < props.Length; i++)
        {
            if (props[i]!=null)
            {
                int index = Random.Range(0, GridPositions.Count);
                props[i].transform.position = new Vector3((GridPositions[index].x * GridDimension.x) + GridOrigin.x, yInitialPos, (GridPositions[index].y * GridDimension.z) + GridOrigin.z);
                props[i].SetActive(true);
                GridPositions.RemoveAt(index);
            }
        }
    }
    void SpawnEnemies(int spawnAmmount)
    {
        for (int i = 0; i < enemies.Length; i++)
        {
            if(enemies[i]!=null)
                enemies[i].SetActive(false);
        }
        for (int i = 0; i < spawnAmmount; i++)
        {
            if (enemies != null)
            {
                int index = Random.Range(0, GridPositions.Count);
                enemies[i].transform.position = new Vector3((GridPositions[index].x * GridDimension.x) + GridOrigin.x, yInitialPos, (GridPositions[index].y * GridDimension.z) + GridOrigin.z);
                enemies[i].SetActive(true);
                GridPositions.RemoveAt(index);
            }
        }
    }
    public void LoadLevel(int enemyAmmount)
    {
        GridPositions.Clear();
        FillVectorList();
        SpawnProps();
        SpawnEnemies(enemyAmmount);
    }
    public int GetActiveEnemies()
    {
        int value = 0;
        for(int i = 0; i < enemies.Length; i++)
        {
            if(enemies[i]!=null && enemies[i].activeSelf)
                value++;
        }
        return value;
    }
}
