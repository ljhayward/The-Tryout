using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Projectile : MonoBehaviour
{
    [SerializeField] float damage = 50f;
    Rigidbody2D myRigidbody;
    [SerializeField] float projectileSpeed = 0.5f;

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.right * projectileSpeed * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Health>())
        {
            collision.gameObject.GetComponent<Health>().setHealth(damage);
        }
        Destroy(gameObject);
    }


}
