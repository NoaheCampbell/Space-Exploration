using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LaserScript : MonoBehaviour
{
    public float speed;
    public float timer;
    private float playerDistance;
    private float enemyDistance;
    private float speedCap;
    public Vector3 velocity;
    public float magnitude;
    private Rigidbody _rb;
    private GameObject _parent;
    private string _parentTag;

    // Start is called before the first frame update
    void Start()
    {
        _parent = transform.parent.gameObject;
        _parentTag = _parent.tag;
        playerDistance = 100f;
        enemyDistance = 50f;
        speedCap = 600f;
        _rb = GetComponent<Rigidbody>();

        if (_parent.tag == "Player")
        {
            PlayerParent();
        }
        else if (_parent.tag == "Enemy")
        {
            EnemyParent();
        }
    }

    // Update is called once per frame
    void Update()
    {
        velocity = _rb.velocity;
        magnitude = velocity.magnitude;

        // If the laser's timer is greater than 0, subtract from the timer, else destroy it
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
        }
        // Moves the laser forward as long as it isn't moving faster than the speed cap
        if (magnitude < speedCap)
        {
            if (_parentTag == "Player")
                _rb.AddForce(-transform.forward * speed * Time.deltaTime);
            else if (_parentTag == "Enemy")
                _rb.AddForce(transform.forward * speed * Time.deltaTime);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        // If the laser collides with an enemy, destroy the enemy and the laser
        if (other.gameObject.tag == "Enemy")
        {
            // Destroys the enemy and removes it from the enemy list
            EnemyScript.enemyList.Remove(other.gameObject);
            Destroy(other.gameObject);

            if (EnemyScript.enemyList.Count == 0)
            {
                GameObject winnerCanvas = Instantiate(Resources.Load("Prefabs/WinnerPrefab"), new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
            }

            Destroy(gameObject);
        }
        else if (other.gameObject.tag == "Player")
        {

            SceneManager.LoadScene("GameOverScene", LoadSceneMode.Single);
            Resources.UnloadUnusedAssets();

            Destroy(transform.gameObject);
        }
        else
        {
            // If the other object is a planet, destroy the laser
            Destroy(gameObject);
        }
    }

    public void PlayerParent()
    {
        // Sets the laser's position to the player's position
        GameObject player = GameObject.Find("Player");
        transform.position = player.transform.position - player.transform.forward * playerDistance;
        transform.rotation = player.transform.rotation;
    }

    public void EnemyParent()
    {
        // Sets the laser's position to the enemy's position
        GameObject enemy = GameObject.Find("EnemyPrefab");
        transform.position = enemy.transform.position + enemy.transform.forward * enemyDistance;
        transform.rotation = enemy.transform.rotation;
        transform.localScale = new Vector3(15, 15, 15);
    }
}
