using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Rigidbody enemyRb;
    private GameObject player;
    public float Speed = 10.0f;
    // Start is called before the first frame update
    void Start()
    {
        transform.gameObject.tag = "Enemy";
        if(enemyRb == null)
        {
            enemyRb = GetComponent<Rigidbody>();
        }
        player = GameObject.Find("Ball");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = player.transform.position - transform.position;
        direction = Vector3.Normalize(direction);
        enemyRb.AddForce(direction * Speed * Time.deltaTime);
    }
}
