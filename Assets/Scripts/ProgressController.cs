using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressController : MonoBehaviour
{
    List<GameObject> enemiesInScene = new List<GameObject>();
    GameObject finalDoorway;

    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            enemiesInScene.Add(enemy);
        }
        finalDoorway = GameObject.FindGameObjectWithTag("Final Doorway");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void killEnemy(GameObject deadEnemy)
    {
        Debug.Log("Enemies remaining: " + enemiesInScene.Count);
        enemiesInScene.Remove(deadEnemy);

        if (enemiesInScene.Count == 0)
        {
            finalDoorway.GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
