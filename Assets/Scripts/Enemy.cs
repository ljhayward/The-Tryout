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
    [SerializeField] AudioClip gunShot;
    [SerializeField] float gunVolume = 0.8f;
    [SerializeField] float turnSpeed;
    [SerializeField] float rotationModifier;

    GameObject target;
    AIDestinationSetter aIDest;
    Vector2 direction;
    Animator myAnimator;
    AudioSource audioSource;
    int layerMask;
    bool canShoot = true;
    public bool seenTarget = false;    //switch for when to start chasing

    

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        aIDest = GetComponent<AIDestinationSetter>();
        aIDest.target = target.transform;
        myAnimator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        layerMask = ~(LayerMask.GetMask("Enemy"));
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
        if (target != null && seenTarget)
        {
            Vector3 vectorToTarget = target.transform.position - transform.position;
            float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg - rotationModifier;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * turnSpeed);
        }
    }

    void IsPlayerInAim()
    {
        if(target != null && !aIPath.enabled)
        {
            RaycastHit2D sightHit = Physics2D.Raycast(transform.position, target.transform.position - transform.position, maxAimDistance, layerMask);
            if (sightHit && sightHit.collider.name == "Player" && !seenTarget)
            {
                seenTarget = true;
                aIPath.enabled = true;
            }
        }
        RaycastHit2D aimHit = Physics2D.Raycast(transform.position, transform.right, maxAimDistance, layerMask);
        if (aimHit && aimHit.collider.name == "Player" && canShoot)
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
        GetComponentInChildren<ParticleSystem>().Play();
        audioSource.PlayOneShot(gunShot, gunVolume);
        yield return new WaitForSeconds(cooldownTime);
        canShoot = true;
    }
}
