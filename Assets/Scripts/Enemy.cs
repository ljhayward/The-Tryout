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
    [SerializeField] GameObject bullet;
    [SerializeField] Transform gun;
    [SerializeField] float turnSpeed;
    [SerializeField] float rotationModifier;

    GameObject target;
    Vector2 direction;
    int layerMask;
    bool canShoot = true;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        layerMask = ~(LayerMask.GetMask("Enemy"));
    }

    // Update is called once per frame
    void Update()
    {
        //FaceVelocity();
        FacePlayer();
        IsPlayerInAim();
        
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

    //void FaceVelocity()
    //{
    //    direction = aIPath.desiredVelocity;

    //    transform.right = direction;
    //}

    void IsPlayerInAim()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, maxAimDistance, layerMask);
        if (hit && hit.collider.name == "Player" && canShoot)
        {
            StartCoroutine(Shoot());
        }
    }

    public IEnumerator Shoot()
    {
        canShoot = false;
        cooldownTime = Random.Range(minCooldownTime, maxCooldownTime);
        Instantiate(bullet, gun.position, transform.rotation);
        yield return new WaitForSeconds(cooldownTime);
        canShoot = true;
    }
}
