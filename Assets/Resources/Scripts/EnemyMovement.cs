using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyMovement : MonoBehaviour
{
    private Rigidbody _rb;
    public GameObject laserPrefab;
    private float _forwardSpeed;
    private float _sidewaysSpeed;
    private float _forwardSpeedCap;
    private float _rotationSpeed;
    private float _forwardForce;
    public bool _canShoot;
    public float shootTimer;
    private Quaternion _targetRotation;
    private Vector3 _direction;
    private Vector3 _targetPosition;
    private float _distanceToObjectHit;
    private float _shootCooldown;
    private string _objectTag;
    public float timeSinceLastRaycastHit;
    private Quaternion _previousTargetRotation;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _forwardSpeedCap = 2000f;
        _rotationSpeed = 500f;
        _forwardForce = 2000f;
        _canShoot = false;
        shootTimer = 10f;
        _forwardSpeed = 100f;
        _shootCooldown = 5f;
        timeSinceLastRaycastHit = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (shootTimer <= 0)
            _canShoot = true;

        Vector3 localVelocity = transform.InverseTransformDirection(_rb.velocity);
        _sidewaysSpeed = localVelocity.x;

        // Sends out raycasts checking to see if the player is nearby
        for (int i = 0; i < 200; i++)
        {
            RaycastHit hit;
            Vector3 rayDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
            bool hitSomething = Physics.Raycast(transform.position, rayDirection, out hit, 9000f);

            if (hitSomething)
                _objectTag = hit.transform.gameObject.tag;

            if (hitSomething && _objectTag == "Player")
            {
                // If the raycast hits the player, rotate towards the ray's rotation
                _previousTargetRotation = _targetRotation;
                _targetRotation = Quaternion.LookRotation(hit.point - transform.position);

                // Gets the distance to the player and its position
                _distanceToObjectHit = Vector3.Distance(transform.position, hit.point);
                _targetPosition = hit.collider.gameObject.transform.position;
            }

            // Nonfunctional code
            // if (hitSomething && _objectTag == "EnemyPrefab")
            // {
            //     // If the enemy the raycast hit is within 150 units, move in another _direction
            //     if (_distanceToObjectHit < 150f)
            //     {
            //         _targetPosition = hit.collider.gameObject.transform.position;
            //         _targetRotation = Quaternion.LookRotation(_targetPosition - transform.position);
            //     }
            // }

        }

        // If the _targetRotation is not null, rotate towards the _targetRotation and move closer
        if (_targetRotation != null)
        {
            if (_previousTargetRotation == _targetRotation)
            {
                timeSinceLastRaycastHit += Time.deltaTime;
            }
            else
            {
                timeSinceLastRaycastHit = 0f;
            }

            // Now that the raycast revealed the player's position and distance, rotate and move towards the player
            transform.rotation = Quaternion.Slerp(transform.rotation, _targetRotation, _rotationSpeed * Time.deltaTime);
            transform.position = Vector3.MoveTowards(transform.position, _targetPosition, _forwardSpeed * Time.deltaTime);

            // Determines if the player is facing the enemy using the dot product
            _direction = transform.forward;
            float dot = Vector3.Dot(_direction, _targetPosition - transform.position);

            // If the player is facing the enemy, turn to the side and move forward
            if (dot > .9)
            {
                _rb.AddForce(transform.forward * _forwardSpeed * Time.deltaTime);
                _rb.AddForce(transform.right * _sidewaysSpeed * Time.deltaTime);
            }

            // If the time since the last raycast hit is greater than 1 second, move forward
            if (timeSinceLastRaycastHit > 1f)
            {
                MoveForward();
            }
        }
        else
        {
            // Since the _targetRotation is null, move around until the player is found
            MoveForward();
        }

        // If the enemy is close to the player, rotate towards them and shoot
        if (_distanceToObjectHit < 800f && _canShoot)
        {
            Shoot();
            _canShoot = false;
            shootTimer = _shootCooldown;
        }

        // If the enemy is touching the player, kill the player and all enemies
        if (_distanceToObjectHit < 100f && _objectTag == "Player")
        {
            SceneManager.LoadScene("GameOverScene", LoadSceneMode.Single);
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
        }
    }

    public void Shoot()
    {
        // Creates a laser in front of the enemy
        GameObject laser = Instantiate(laserPrefab, transform) as GameObject;
    }
    
}
