using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    private GameObject _player;
    private float _cameraDistance = 10;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        var forward = transform.forward;
        forward.y = 0;

        // Moves the camera to the player's position
        transform.position = _player.transform.position - _player.transform.forward * _cameraDistance;
        transform.LookAt(_player.transform);


        transform.position = new Vector3(transform.position.x, transform.position.y + 10, transform.position.z);

        transform.Rotate(new Vector3(transform.rotation.x, transform.rotation.y + 180, transform.rotation.z));


    }
}
