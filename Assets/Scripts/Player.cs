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
    AudioSource audioSource;

    [SerializeField] float moveSpeed;
    [SerializeField] GameObject bullet;
    [SerializeField] Transform gun;
    [SerializeField] float bulletSpread = 3f;
    [SerializeField] float burstTime = 0.1f;
    [SerializeField] float fireDelay = 0.8f;
    [SerializeField] AudioClip gunShot;
    [SerializeField] float gunVolume = 0.8f;
    
    //Vector3 spread;

    bool burstReady = true;
    bool isShooting = false;


    

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        
        //spread = new Vector3(0, 0, randomSpread);
        
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
        if (burstReady)
        {
            burstReady = false;
            myAnimator.SetTrigger("isShooting");
            StartCoroutine(BurstFire());
        }
        
    }

    private IEnumerator BurstFire()
    {
        if (!isShooting)
        {
            isShooting = true;
            StartCoroutine(Fire());
        }
        yield return new WaitForSeconds(fireDelay);
        burstReady = true;
    }

    private IEnumerator Fire()
    {
        for (int i = 1; i <=3; i++)
        {
            Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, transform.rotation.eulerAngles.z + Random.Range(-bulletSpread, bulletSpread)));
            Instantiate(bullet, gun.position, rotation);
            audioSource.PlayOneShot(gunShot, gunVolume);
            yield return new WaitForSeconds(burstTime);
        }
        isShooting = false;
    }



    //void OnFire(InputValue value)
    //{
    //    //if (!isAlive)
    //    //    return;
    //    if (isShooting == false)
    //    {
    //        isShooting = true;
    //        myAnimator.SetTrigger("isShooting");
    //        StartCoroutine(Fire());
    //    }
    //}

    //private IEnumerator Fire()
    //{
    //    for (int i = 1; i <= 3; i++)
    //    {
    //        Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, transform.rotation.eulerAngles.z + Random.Range(-bulletSpread, bulletSpread)));
    //        Instantiate(bullet, gun.position, rotation);
    //        yield return new WaitForSeconds(burstTime);
    //    }
    //    isShooting = false;
    //}

}
