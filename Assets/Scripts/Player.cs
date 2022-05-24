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
        //Vector2 projectedMousePosition = mainCamera.ScreenToWorldPoint(mousePosition);
        faceMouse();
        Run();
    }

    void faceMouse()
    {
        Vector2 mousePos = mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        transform.right = mousePos - new Vector2(transform.position.x, transform.position.y);
        //Vector2 direction = projectedMousePosition - transform.position;
    }

    void Run()
    {
        //if (!isAlive)
        //    return;

        Vector2 playerVelocity = new Vector2(moveInput.x * moveSpeed, moveInput.y * moveSpeed);
        myRigidbody.velocity = playerVelocity;

        bool playerHasSpeed = Mathf.Abs(myRigidbody.velocity.magnitude) > Mathf.Epsilon;
        myAnimator.SetBool("isMoving", playerHasSpeed);

        //if (playerHasHorizontalSpeed)
        //    FlipSprite();
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

        
        //Instantiate(bullet, gun.position, Quaternion.identity);
        
    }




    //Vector2 moveInput;
    //Vector2 mousePosition;
    //Rigidbody2D myRigidbody;
    //Animator myAnimator;
    //Camera mainCamera;
    //Transform myTransform;

    //[SerializeField] float moveSpeed;
    //[SerializeField] GameObject bullet;
    //[SerializeField] Transform gun;
    //Vector2 mousePos;

    //// Start is called before the first frame update
    //void Start()
    //{
    //    mainCamera = Camera.main;
    //    myRigidbody = GetComponent<Rigidbody2D>();
    //    myAnimator = GetComponent<Animator>();
    //    myTransform = this.transform;
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    //Vector2 projectedMousePosition = mainCamera.ScreenToWorldPoint(mousePosition);
    //    faceMouse();
    //    Run();
    //}

    //void faceMouse()
    //{
    //    mousePos = mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
    //    transform.right = mousePos - new Vector2(transform.position.x, transform.position.y);
    //    //Vector2 direction = projectedMousePosition - transform.position;
    //}

    //void Run()
    //{
    //    //if (!isAlive)
    //    //    return;

    //    Vector2 playerVelocity = new Vector2(moveInput.x * moveSpeed, moveInput.y * moveSpeed);
    //    myRigidbody.velocity = playerVelocity;

    //    bool playerHasSpeed = Mathf.Abs(myRigidbody.velocity.magnitude) > Mathf.Epsilon;
    //    myAnimator.SetBool("isMoving", playerHasSpeed);

    //    //if (playerHasHorizontalSpeed)
    //    //    FlipSprite();
    //}

    //void OnMove(InputValue value)
    //{
    //    moveInput = value.Get<Vector2>();
    //}

    //void OnFire(InputValue value)
    //{
    //    //if (!isAlive)
    //    //    return;
    //    myAnimator.SetTrigger("isShooting");
    //    GameObject projectileTransform = Instantiate(bullet, gun.position, transform.rotation);

    //    Vector3 mousePosition = mousePos;
    //    Vector3 shootDir = (mousePosition - gun.position).normalized;
    //    //Instantiate(bullet, gun.position, Quaternion.identity);
    //    projectileTransform.GetComponent<Projectile>().Setup(shootDir);
    //}
}
