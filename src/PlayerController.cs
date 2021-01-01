using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 7f;
    public float jumpAmount = 8f;

    AudioSource jumpAudio;
    Rigidbody rb;

    private bool canJump = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * 10);
        jumpAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // If they press space when (approximiately) on the ground while the game
        // isn't over, jump.
        if (!LevelManager.isGameOver && Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            rb.AddForce(0, jumpAmount, 0, ForceMode.Impulse);
            jumpAudio.Stop();
            jumpAudio.Play();
        }
    }

    private void FixedUpdate()
    {
        if (!LevelManager.isGameOver)
        {
            

            float cameraYTilt = Camera.main.transform.eulerAngles.y;

            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");

            Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical);
            movement = Quaternion.Euler(0, cameraYTilt, 0) * movement;

            rb.AddForce(movement * speed);
        }
       
        // Prevents constant jumping. Is placed here since doing it every frame caused issues
        // with sometimes not being able to jump.
        canJump = false;

    }

    private void OnCollisionStay(Collision collisionInfo)
    {
        if ((collisionInfo.gameObject.CompareTag("Floor") && transform.position.y - (transform.localScale.y / 2) + .02 >= 
            collisionInfo.gameObject.transform.position.y + (collisionInfo.gameObject.transform.localScale.y / 2)) ||
            collisionInfo.gameObject.CompareTag("Ramp"))
        {
            canJump = true;
        }
    }
}
