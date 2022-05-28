using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] float health = 100f;
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
            Destroy(gameObject);
        }
    }
}
