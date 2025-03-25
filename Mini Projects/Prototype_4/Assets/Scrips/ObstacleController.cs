using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    public float obstacleSpeed = 10.0f;
    public PlayerController playerController;
    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.FindAnyObjectByType<PlayerController>();

    }

    // Update is called once per frame
    void Update()
    {
        if(playerController.gameover == false)
        {
            transform.Translate(Vector3.left * obstacleSpeed * Time.deltaTime, Space.World);
        }
    }

    private void FixedUpdate()
    {
        if(transform.position.x < -10)
        {
            Destroy(gameObject);
        }
    }
}
