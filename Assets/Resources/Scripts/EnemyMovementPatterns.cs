// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class EnemyMovementPatterns : MonoBehaviour
// {
//     public static float _sidewaysSpeedCap;
//     public static float _forwardSpeedCap;
//     public static float _forwardSpeed;
//     public static float sidewaysSpeed;
//     public static float backSpeed;
//     public static float rotationSpeed;
//     public static float _forwardForce = 2000f;
//     public static float _sidewaysForce = 2000f;
//     private EnemyMovement _enemyScript;
//     private GameObject _enemy;
//     private Rigidbody _rb;

//     // Start is called before the first frame update
//     void Start()
//     {
//         _sidewaysSpeedCap = 300f;
//         _forwardSpeedCap = 9000f;
//         rotationSpeed = 500f;
//         _enemy = GameObject.Find("Enemy");
//         _enemyScript = _enemy.GetComponent<EnemyMovement>();
//         _rb = _enemy.GetComponent<Rigidbody>();
//     }

//     // Update is called once per frame
//     void Update()
//     {
//         _forwardSpeed = _enemyScript.forwardSpeed;
//         sidewaysSpeed = _enemyScript.sidewaysSpeed;
//         backSpeed = -_enemyScript.backSpeed;
//     }

//     public static void MoveForward()
//     {
//         // Moves the enemy forward if it is not at the maximum speed
//         if (_forwardSpeed < _forwardSpeedCap)
//         {
//             _rb.AddForce(transform.forward * _forwardForce * Time.deltaTime);
//             _forwardSpeed += _forwardForce * Time.deltaTime;
//         }
//     }

//     public static void MoveBackward()
//     {
//         // Moves the enemy backward if it is not at the minimum speed
//         if (_forwardSpeed > -_forwardSpeedCap)
//         {
//             _rb.AddForce(transform.forward * -_forwardForce * Time.deltaTime);
//             _forwardSpeed -= _forwardForce * Time.deltaTime;
//         }
//     }

//     public static void MoveLeft()
//     {
//         // Moves the enemy left if it is not at the minimum speed
//         if (sidewaysSpeed > -_sidewaysSpeedCap)
//         {
//             _rb.AddForce(transform.right * -_sidewaysForce * Time.deltaTime);
//             sidewaysSpeed -= _sidewaysForce * Time.deltaTime;
//         }
//     }

//     public static void MoveRight()
//     {
//         // Moves the enemy right if it is not at the maximum speed
//         if (sidewaysSpeed < _sidewaysSpeedCap)
//         {
//             _rb.AddForce(transform.right * _sidewaysForce * Time.deltaTime);
//             sidewaysSpeed += _sidewaysForce * Time.deltaTime;
//         }
//     }

//     public static void MoveUp()
//     {
//         // Moves the enemy up if it is not at the maximum speed
//         if (_forwardSpeed < _forwardSpeedCap)
//         {
//             _rb.AddForce(transform.up * _forwardForce * Time.deltaTime);
//             _forwardSpeed += _forwardForce * Time.deltaTime;
//         }
//     }

//     public static void MoveDown()
//     {
//         // Moves the enemy down if it is not at the minimum speed
//         if (_forwardSpeed > -_forwardSpeedCap)
//         {
//             _rb.AddForce(transform.up * -_forwardForce * Time.deltaTime);
//             _forwardSpeed -= _forwardSpeedCap * Time.deltaTime;
//         }
//     }

//     public static void RotateLeft()
//     {
//         // Rotates the enemy left if it is not at the maximum speed
//         if (sidewaysSpeed > -_sidewaysSpeedCap)
//         {
//             _rb.AddRelativeTorque(Vector3.forward * -_sidewaysForce * Time.deltaTime);
//             sidewaysSpeed -= _sidewaysForce * Time.deltaTime;
//         }
//     }

//     public static void RotateRight()
//     {
//         // Rotates the enemy right if it is not at the maximum speed
//         if (sidewaysSpeed < _sidewaysSpeedCap)
//         {
//             _rb.AddRelativeTorque(Vector3.forward * _sidewaysForce * Time.deltaTime);
//             sidewaysSpeed += _sidewaysForce * Time.deltaTime;
//         }
//     }

//     public static void RotateUp()
//     {
//         // Rotates the enemy up if it is not at the maximum speed
//         if (_forwardSpeed < _forwardSpeedCap)
//         {
//             _rb.AddRelativeTorque(Vector3.right * _sidewaysForce * Time.deltaTime);
//             _forwardSpeed += _forwardForce * Time.deltaTime;
//         }
//     }

//     public static void RotateDown()
//     {
//         // Rotates the enemy down if it is not at the maximum speed
//         if (_forwardSpeed > -_forwardSpeedCap)
//         {
//             _rb.AddRelativeTorque(Vector3.right * -_sidewaysForce * Time.deltaTime);
//             _forwardSpeed -= _forwardForce * Time.deltaTime;
//         }
//     }

//     public static void Stop()
//     {
//         // Stops the enemy
//         _rb.velocity = Vector3.zero;
//         _forwardSpeed = 0f;
//         sidewaysSpeed = 0f;
//     }
// }
