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

    Vector2 direction;
    int layerMask;
    bool canShoot = true;

    // Start is called before the first frame update
    void Start()
    {
        layerMask = ~(LayerMask.GetMask("Enemy"));
    }

    // Update is called once per frame
    void Update()
    {
        FaceVelocity();
        IsPlayerInAim();
        
    }

    void FaceVelocity()
    {
        direction = aIPath.desiredVelocity;

        transform.right = direction;
    }

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
