using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPadBehavior : MonoBehaviour
{
    public GameObject player;
    public float jumpAmount = 10;
    public float moveHeight;

    bool doJump = false;
    float t = 0;
    Vector3 startPosition;
    float moveStep = 0.2f, sitStep = 1f;
    // Start is called before the first frame update
    void Start()
    {
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player");
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (doJump)
        {
            t += Time.deltaTime;
            if (t <= moveStep)
            {
                transform.position = Vector3.Lerp(startPosition,
                    startPosition + transform.up * moveHeight,
                    t / moveStep
                );
            } else if (t > moveStep + sitStep && t <= (2 * moveStep) + sitStep)
            {
                transform.position = Vector3.Lerp(startPosition + transform.up * moveHeight,
                    startPosition, (t - moveStep - sitStep) / moveStep
                );
            } else if (t > (2 * moveStep) + sitStep)
            {
                t = 0;
                doJump = false;
            }

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && !doJump)
        {
            player.GetComponent<Rigidbody>().AddForce(transform.up * jumpAmount, ForceMode.Impulse);
            doJump = true;
        }
    }
}
