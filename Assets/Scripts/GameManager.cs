using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Material[] bodyMaterials;
    public GameObject foodPrefab;
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < 5; i++)
            SpawnFood();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnFood()
    {
        GameObject food = GameObject.Instantiate(foodPrefab);
        float randX = Random.Range(-50, 50);
        float randZ = Random.Range(-50, 50);
        food.transform.position = new Vector3(randX, 0.5f, randZ);
    }

    public void Die()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
