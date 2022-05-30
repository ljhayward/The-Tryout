using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    [SerializeField] float health = 100f;
    [SerializeField] GameObject blood;
    ProgressController progressController;

    private void Start()
    {
        progressController = FindObjectOfType<ProgressController>();
    }

    public float getHealth()
    {
        return health;
    }

    public void setHealth(float change)
    {
        health -= change;
        if (health <= 0)
        {
            if (gameObject.CompareTag("Enemy"))
            {
                progressController.killEnemy(this.gameObject);
            }

            if (gameObject.CompareTag("Player"))
            {
                progressController.PlayerDead();
            }

            Instantiate(blood, transform.position, Quaternion.identity);
            Destroy(gameObject);
            
            
        }
    }
}
