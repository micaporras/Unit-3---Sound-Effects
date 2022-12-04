using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    private Animator playerAnim;
    private AudioSource playerAudio;
    public float jumpForce;
    public float gravityModifier;
    public ParticleSystem explosionParticle;
    public ParticleSystem dirtParticle;
    public AudioClip jumpSound;
    public AudioClip crashSound;
    public bool isOnGround = true;
    public bool gameOver = false;
    

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        Physics.gravity *= gravityModifier;

    }

    void Update()
    {
        // The player jumps when you press the space bar    
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround && !gameOver)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
            playerAnim.SetTrigger("Jump_trig"); // Apply a jump animation
            playerAudio.PlayOneShot(jumpSound, 1.0f); // Apply a jump audio
            dirtParticle.Stop(); // When the player jump, the dirt particle stops playing
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground")) 
        {
            isOnGround = true;
            dirtParticle.Play(); // There's a dirt particle while the player is running
        } else if (collision.gameObject.CompareTag("Obstacle")) // The game will be over if the player hits an obstacle
        {
            gameOver = true;
            Debug.Log("Game Over!");
            playerAnim.SetBool("Death_b", true); // Apply a death animation
            playerAnim.SetInteger("DeathType_int", 1);
            playerAudio.PlayOneShot(crashSound, 1.0f); // Apply a crash audio
            explosionParticle.Play(); // Apply an explosion 
            dirtParticle.Stop(); 
        }
    }
}
