using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    // Creates variables for the rigidbody and the _player's speed and rotation speed
    private Rigidbody _rb;
    private float _forwardForce = 2000f;
    private float _sidewaysForce = 2000f;
    public GameObject laserPrefab;
    private ParticleSystem _leftMainEngineParticles;
    private ParticleSystem _rightMainEngineParticles;
    private ParticleSystem _topLeftEngineParticles;
    private ParticleSystem _bottomLeftEngineParticles;
    private ParticleSystem _topRightEngineParticles;
    private ParticleSystem _bottomRightEngineParticles;
    private GameObject _player;
    private float _sidewaysSpeedCap;
    private float _forwardSpeedCap;
    public float forwardSpeed;
    public float sidewaysSpeed;
    public float backSpeed;
    public static Camera _amera;

    // Start is called before the first frame update
    void Start()
    {
        camera = transform.GetChild(0).GetComponent<Camera>();
        GetComponent<Camera>().transform.tag = "MainCamera";


        _rb = GetComponent<Rigidbody>();

        _sidewaysSpeedCap = 300f;
        _forwardSpeedCap = 6000f;

        // Gets the particle systems from the player and its children
        _topLeftEngineParticles = transform.GetChild(1).GetComponent<ParticleSystem>();
        _bottomLeftEngineParticles = transform.GetChild(2).GetComponent<ParticleSystem>();
        _topRightEngineParticles = transform.GetChild(3).GetComponent<ParticleSystem>();
        _bottomRightEngineParticles = transform.GetChild(4).GetComponent<ParticleSystem>();
        _leftMainEngineParticles = transform.GetChild(5).GetComponent<ParticleSystem>();
        _rightMainEngineParticles = transform.GetChild(6).GetComponent<ParticleSystem>();

        _topLeftEngineParticles.Stop();
        _bottomLeftEngineParticles.Stop();
        _topRightEngineParticles.Stop();
        _bottomRightEngineParticles.Stop();
        _leftMainEngineParticles.Stop();
        _rightMainEngineParticles.Stop();

        _player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 localVelocity = transform.InverseTransformDirection(_rb.velocity);
        sidewaysSpeed = localVelocity.x;
        

        // Moves the player forwards or backwards using the W and S keys
        if (Input.GetKey(KeyCode.W) && forwardSpeed < _forwardSpeedCap)
        {
            _rb.AddForce(transform.forward * -_forwardForce * Time.deltaTime);
            forwardSpeed += (_forwardForce * Time.deltaTime) / 2;
            backSpeed -= (_forwardForce * Time.deltaTime) / 2;
            _leftMainEngineParticles.Play();
            _rightMainEngineParticles.Play();
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            _leftMainEngineParticles.Stop();
            _rightMainEngineParticles.Stop();
        }

        if (Input.GetKey(KeyCode.S) && backSpeed < _forwardSpeedCap)
        {
            _rb.AddForce(transform.forward * _forwardForce * Time.deltaTime);
            forwardSpeed -= (_forwardForce * Time.deltaTime) / 2;
            backSpeed += (_forwardForce * Time.deltaTime) / 2;
        }

        // Rotates the player along the z-axis using the A and D keys
        if (Input.GetKey(KeyCode.D))
        {
            _rb.AddRelativeTorque(Vector3.forward * _sidewaysForce * Time.deltaTime);

        }
        if (Input.GetKey(KeyCode.A))
        {
            _rb.AddRelativeTorque(Vector3.forward * -_sidewaysForce * Time.deltaTime);
        }

        // Moves the player left and right using the arrow keys
        if (Input.GetKey(KeyCode.RightArrow) && sidewaysSpeed < _sidewaysSpeedCap)
        {
            _rb.AddForce(transform.right * -_forwardForce * Time.deltaTime);
            _topLeftEngineParticles.Play();
            _bottomLeftEngineParticles.Play();
        }
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            _topLeftEngineParticles.Stop();
            _bottomLeftEngineParticles.Stop();
        }

        if (Input.GetKey(KeyCode.LeftArrow) && sidewaysSpeed < _sidewaysSpeedCap)
        {
            _rb.AddForce(transform.right * _forwardForce * Time.deltaTime);
            _topRightEngineParticles.Play();
            _bottomRightEngineParticles.Play();
        }

        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            _topRightEngineParticles.Stop();
            _bottomRightEngineParticles.Stop();
        }

        // Tilts the player up and down relative to their current rotation
        if (Input.GetKey(KeyCode.DownArrow))
        {
            _rb.AddRelativeTorque(Vector3.right * _sidewaysForce * Time.deltaTime);
            _bottomLeftEngineParticles.Play();
            _bottomRightEngineParticles.Play();
        }
        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            _bottomLeftEngineParticles.Stop();
            _bottomRightEngineParticles.Stop();
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            _rb.AddRelativeTorque(Vector3.left * _sidewaysForce * Time.deltaTime);
            _topLeftEngineParticles.Play();
            _topRightEngineParticles.Play();
        }
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            _topLeftEngineParticles.Stop();
            _topRightEngineParticles.Stop();
        }


        // If the space bar is pressed, creates a new laser game object and adds it to the scene
        if (Input.GetKeyDown(KeyCode.Space))
        {            
            // Creates a new laser game object and adds it to the scene
            GameObject laser = Instantiate(laserPrefab, transform) as GameObject;
        }

        // If either shift key is held, the player's velocity will slow down faster than normal
        if (Input.GetKey(KeyCode.LeftShift) || (Input.GetKey(KeyCode.RightShift)))
        {
            // If the player's velocity is between 0 and 1, set it to 0
            if (_rb.velocity.magnitude <= 1)
            {
                _rb.velocity = Vector3.zero;
                forwardSpeed = 0;
                backSpeed = 0;
            }

            // Slow down the player's velocity
            _rb.velocity = _rb.velocity * 0.99f;
            // Slow down the player's angular velocity
            _rb.angularVelocity = _rb.angularVelocity * 0.99f;
            forwardSpeed *= 0.99f;
            backSpeed *= 0.99f;
        }

        // If no key is being press, the player's velocity will slowly go towards 0
        if (!Input.anyKey)
        {
            // If the player's velocity is between 0 and 1, set it to 0
            if (_rb.velocity.magnitude <= 1)
            {
                _rb.velocity = Vector3.zero;
                forwardSpeed = 0;
                backSpeed = 0;
            }

            if(_rb.velocity.magnitude > 0)
            {
                _rb.velocity = _rb.velocity * 0.99999f;
                _rb.angularVelocity = _rb.angularVelocity * 0.99999f;
                forwardSpeed *= 0.99999f;
                backSpeed *= 0.99999f;
            }
            // Stops all particle systems from playing
            _player.GetComponent<ParticleSystem>().Stop(true);
        }
    }
}
