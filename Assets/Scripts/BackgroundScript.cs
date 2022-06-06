using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScript : MonoBehaviour
{
    private GameObject[] backgroundImages;
    private Quaternion[] backgroundRotations;
    private GameObject canvas;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        backgroundImages = GameObject.FindGameObjectsWithTag("Background");
        backgroundRotations = new Quaternion[backgroundImages.Length];

        for (int i = 0; i < backgroundImages.Length; i++)
        {
            backgroundRotations[i] = backgroundImages[i].transform.rotation;
        }

        canvas = GameObject.Find("Canvas");
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {

        // Resets each background image's rotation to its original rotation
        for (int i = 0; i < backgroundImages.Length; i++)
        {
            backgroundImages[i].transform.rotation = backgroundRotations[i];
        }

        // Moves the parent canvas to follow the player's movements
        Vector3 playerPos = player.transform.position; 
        Vector3 newCanvasPos = new Vector3(playerPos.x + 100, playerPos.y + 23, playerPos.z - 70000); 
        canvas.transform.position = newCanvasPos;

    }
}
