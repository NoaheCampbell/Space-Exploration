using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Rigidbody _rb;
    public GameObject laserPrefab;
    private float _forwardSpeed;
    private float _sidewaysSpeed;
    private float _backSpeed;
    private float _forwardSpeedCap;
    private float _rotationSpeed;
    private float _forwardForce;
    public bool _canShoot;
    public float shootTimer;
    private Quaternion targetRotation;
    private Vector3 direction;
    private Vector3 targetPosition;
    private float distanceToObjectHit;
    private float _shootCooldown;
    private string objectTag;
    public bool isPlayerDead;


    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _forwardSpeedCap = 20000f;
        _rotationSpeed = 500f;
        _forwardForce = 2000f;
        _canShoot = false;
        shootTimer = 10f;
        _forwardSpeed = 100f;
        _shootCooldown = 1f;
        isPlayerDead = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (shootTimer < 0)
            _canShoot = true;

        Vector3 localVelocity = transform.InverseTransformDirection(_rb.velocity);
        _sidewaysSpeed = localVelocity.x;

        // Creates 100 raycasts checking to see if the player is nearby
        for (int i = 0; i < 100; i++)
        {
            RaycastHit hit;
            Vector3 rayDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
            bool hitSomething = Physics.Raycast(transform.position, rayDirection, out hit, 9000f);

            if (hitSomething)
                objectTag = hit.transform.gameObject.tag;

            if (hitSomething && objectTag == "Player")
            {
                // If the raycast hits the player, rotate towards the ray's rotation
                targetRotation = Quaternion.LookRotation(hit.point - transform.position);

                // Gets the distance to the player and its position
                distanceToObjectHit = Vector3.Distance(transform.position, hit.point);
                targetPosition = hit.collider.gameObject.transform.position;
            }
            if (hitSomething && objectTag == "EnemyPrefab")
            {
                // If the enemy the raycast hit is within 150 units, move in another direction
                if (distanceToObjectHit < 150f)
                {
                    targetPosition = hit.collider.gameObject.transform.position;
                    targetRotation = Quaternion.LookRotation(targetPosition - transform.position);
                }
            }
        }

        // If the targetRotation is not null, rotate towards the targetRotation and move closer
        if (targetRotation != null)
        {
            // Now that the raycast revealed the player's position and distance, rotate and move towards the player
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, _forwardSpeed * Time.deltaTime);

            // Determines if the player is facing the enemy using the dot product
            direction = transform.forward;
            float dot = Vector3.Dot(direction, targetPosition - transform.position);

            // If the player is facing the enemy, turn to the side and move forward
            if (dot > .9)
            {
                _rb.AddForce(transform.forward * _forwardSpeed * Time.deltaTime);
                _rb.AddForce(transform.right * _sidewaysSpeed * Time.deltaTime);
            }
        }
        else 
        {
            // Since the targetRotation is null, move around until the player is found
            MoveForward();
        }

        // If the enemy is close to the player, rotate towards them and shoot
        if (distanceToObjectHit < 800f && _canShoot && !isPlayerDead)
        {
            Shoot();
        }

        // If the enemy is touching the player, kill the player and all enemies
        if (distanceToObjectHit < 70f && objectTag == "Player")
        {
            isPlayerDead = true;
            GameObject gameOverCanvas = Instantiate(Resources.Load("Prefabs/GameOverPrefab"), new Vector3(0, 0, 0), Quaternion.identity) as GameObject;

            // Destroy all enemies
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject enemy in enemies)
            {
                Destroy(enemy);
            }
        }

        shootTimer -= Time.deltaTime;   
    }

    public void MoveForward()
    {
        // Moves the enemy forward if it is not at the maximum speed
        if (_forwardSpeed < _forwardSpeedCap)
        {
            _rb.AddForce(transform.forward * _forwardForce * Time.deltaTime);
            _forwardSpeed += _forwardForce * Time.deltaTime;
            _backSpeed -= _forwardForce * Time.deltaTime;
        }
    }

    public void Shoot()
    {
        // Creates a laser in front of the enemy
        GameObject laser = Instantiate(laserPrefab, transform) as GameObject;
        _canShoot = false;
        shootTimer = _shootCooldown;
    }
    
}
