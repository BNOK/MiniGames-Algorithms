using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundRepeater : MonoBehaviour
{
    public Vector3 startPos;
    public float repeatWidth;

    public float backgroundSpeed = 10.0f;
    private PlayerController playerController;
    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.FindAnyObjectByType<PlayerController>();
        startPos = transform.position;
        repeatWidth = GetComponent<BoxCollider>().size.x / 2;
    }

    // Update is called once per frame
    void Update()
    {
        if(playerController.gameover == false)
        {
            transform.Translate(Vector3.left * backgroundSpeed * Time.deltaTime);
            if (transform.position.x < startPos.x - repeatWidth)
            {
                transform.position = startPos;
            }
        }
        
    }
}
