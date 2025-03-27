using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    private Animator playerAnim;
    public float jumpForce = 10.0f;

    private bool isGrounded = true;
    public bool gameover = false;

    public ParticleSystem explosionParticle;
    public ParticleSystem dirtsplatter;

    private AudioSource playerSFX;
    public AudioClip jumpSFX;
    public AudioClip crashSFX;
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        playerSFX = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && isGrounded == true) 
        {
            playerAnim.SetTrigger("Jump_trig");
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
            dirtsplatter.Stop();
            playerSFX.PlayOneShot(jumpSFX);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Ground"))
        {
            isGrounded = true;
            dirtsplatter.Play();
        }
        else if (collision.transform.CompareTag("Obstacle"))
        {
            gameover = true;
            playerAnim.SetBool("Death_b",true);
            playerAnim.SetInteger("DeathType_int", 1);
            explosionParticle.Play();
            dirtsplatter.Stop();
            playerSFX.PlayOneShot(crashSFX);
        }
    }
}
