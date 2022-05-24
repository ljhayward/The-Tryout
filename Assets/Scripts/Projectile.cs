using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Projectile : MonoBehaviour
{
    Rigidbody2D myRigidbody;
    //Vector2 direction;
    [SerializeField] float projectileSpeed = 0.5f;
    Vector3 shootDir;

    // Start is called before the first frame update
    //void Start()
    //{
    //    myRigidbody = GetComponent<Rigidbody2D>();
    //    direction = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
    //    myRigidbody.AddForce(direction * projectileSpeed, ForceMode2D.Impulse);
    //}

    // Update is called once per frame
    void Update()
    {
        //myRigidbody.AddForce(direction * projectileSpeed, ForceMode2D.Impulse);
        transform.position += shootDir * projectileSpeed * Time.deltaTime;
    }

    public void Setup(Vector3 shootDir)
    {
        this.shootDir = shootDir;
    }    
}
