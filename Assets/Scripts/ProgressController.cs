using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ProgressController : MonoBehaviour
{
    [SerializeField] List<MessageSO> messages = new List<MessageSO>();
    [SerializeField] GameObject popUp;

    List<GameObject> enemiesInScene = new List<GameObject>();
    GameObject finalDoorway;
    bool betrayalTriggered = false;
    [SerializeField] GameObject soldier;
    Vector3 soldierOne;
    Quaternion soldierOneRotation;
    Vector3 soldierTwo;
    Quaternion soldierTwoRotation;
    Vector3 soldierThree;
    Quaternion soldierThreeRotation;

    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            enemiesInScene.Add(enemy);
        }
        finalDoorway = GameObject.FindGameObjectWithTag("Final Doorway");
        prepareSoldiers();

        StartCoroutine(displayMessage(0));
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!betrayalTriggered)
        {
            betrayalTriggered = true;
            finalDoorway.GetComponent<BoxCollider2D>().enabled = true;
            StartCoroutine(displayMessage(2));
        }
    }

    public IEnumerator displayMessage(int i)
    {
        popUp.GetComponentInChildren<TextMeshProUGUI>().text = messages[i].GetMessage();
        popUp.SetActive(true);
        yield return new WaitForSeconds(6f);
        popUp.SetActive(false);
        if (betrayalTriggered)
        {
            Instantiate(soldier, soldierOne, soldierOneRotation);
            Instantiate(soldier, soldierTwo, soldierTwoRotation);
            Instantiate(soldier, soldierThree, soldierThreeRotation);
        }
    }

    public void PlayerDead()
    {
        if (betrayalTriggered)
        {
            SceneManager.LoadScene(2);
        }
        else
        {
            StartCoroutine(displayMessage(3));
            StartCoroutine(WaitForSceneLoad());
        }
    }

    private IEnumerator WaitForSceneLoad()
    {
        yield return new WaitForSeconds(6f);
        SceneManager.LoadScene(0);
    }

    void prepareSoldiers()
    {
        soldierOne.x = -30.1700001f; soldierOne.y = 0.159999996f; soldierOne.z = 0f;
        soldierOneRotation.x = 0; soldierOneRotation.y = 0; soldierOneRotation.z = -88.351f;

        soldierTwo.x = -30.1700001f; soldierTwo.y = -8.5f; soldierTwo.z = 0f;
        soldierTwoRotation.x = 0; soldierOneRotation.y = 0; soldierOneRotation.z = 271.649048f;


        soldierThree.x = -35.3600006f; soldierThree.y = -4.19999981f; soldierThree.z = 0f;
        soldierThreeRotation.x = 0; soldierOneRotation.y = 0; soldierOneRotation.z = 4.863f;
    }
}
