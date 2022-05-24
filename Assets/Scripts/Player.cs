using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    Vector2 moveInput;
    Vector2 mousePosition;
    Rigidbody2D myRigidbody;
    Animator myAnimator;
    Camera mainCamera;

    [SerializeField] float moveSpeed;
    [SerializeField] GameObject bullet;
    [SerializeField] Transform gun;


    

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();

        
    }

    // Update is called once per frame
    void Update()
    {
        FaceMouse();
        Run();
    }

    void FaceMouse()
    {
        Vector2 mousePos = mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        transform.right = mousePos - new Vector2(transform.position.x, transform.position.y);
    }

    void Run()
    {
        //if (!isAlive)
        //    return;

        Vector2 playerVelocity = new Vector2(moveInput.x * moveSpeed, moveInput.y * moveSpeed);
        myRigidbody.velocity = playerVelocity;

        bool playerHasSpeed = Mathf.Abs(myRigidbody.velocity.magnitude) > Mathf.Epsilon;
        myAnimator.SetBool("isMoving", playerHasSpeed);

        
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void OnFire(InputValue value)
    {
        //if (!isAlive)
        //    return;
        myAnimator.SetTrigger("isShooting");
        Instantiate(bullet, gun.position, transform.rotation);

        
        
        
    }




    
}
