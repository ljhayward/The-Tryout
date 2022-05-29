using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Enemy : MonoBehaviour
{
    public AIPath aIPath;
    [SerializeField] float maxAimDistance = 100f;
    [SerializeField] float minCooldownTime = 2f;
    [SerializeField] float maxCooldownTime = 10f;
    float cooldownTime = 5f;
    [SerializeField] int bulletSpread;
    [SerializeField] GameObject bullet;
    [SerializeField] Transform gun;
    [SerializeField] float turnSpeed;
    [SerializeField] float rotationModifier;

    GameObject target;
    AIDestinationSetter aIDest;
    Vector2 direction;
    Animator myAnimator;
    int layerMask;
    bool canShoot = true;
    bool seenTarget = false;    //switch for when to start chasing

    //Rigidbody2D myRigidbody;
    //bool playerHasSpeed;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        aIDest = GetComponent<AIDestinationSetter>();
        aIDest.target = target.transform;
        myAnimator = GetComponent<Animator>();
        layerMask = ~(LayerMask.GetMask("Enemy"));

        //myRigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
        FacePlayer();
        IsPlayerInAim();
        IsMoving();
        
        
    }

    void FacePlayer()
    {
        if (target != null)
        {
            Vector3 vectorToTarget = target.transform.position - transform.position;
            float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg - rotationModifier;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * turnSpeed);
        }
    }

    void IsPlayerInAim()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, maxAimDistance, layerMask);
        if (hit && hit.collider.name == "Player" && !seenTarget)
        {
            seenTarget = true;      //may replace with method to start pathfinding
        }
        if (hit && hit.collider.name == "Player" && canShoot)
        {
            StartCoroutine(Shoot());
        }
    }

    void IsMoving()
    {
        myAnimator.SetBool("isMoving", transform.hasChanged);
        if (transform.hasChanged)
        {
            
            transform.hasChanged = false;
        }
        else
        {
            
        }
    }

    public IEnumerator Shoot()
    {
        canShoot = false;
        cooldownTime = Random.Range(minCooldownTime, maxCooldownTime);
        Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, transform.rotation.eulerAngles.z + Random.Range(-bulletSpread, bulletSpread)));
        Instantiate(bullet, gun.position, rotation);
        //Instantiate(bullet, gun.position, transform.rotation);
        yield return new WaitForSeconds(cooldownTime);
        canShoot = true;
    }
}
