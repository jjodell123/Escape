using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public Transform player;
    public float movementSpeed = 2f;
    public AudioClip enemySound;
    public float startAttackDistance = 17f;
    public float startAttackHeightRange = .1f;

    bool attackMode = false;
    private float yDifference = .175f;
    // Start is called before the first frame update
    void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null && !gameObject.GetComponent<Rigidbody>().isKinematic &&
            player.transform.position.y > 0 && attackMode)
        {
            transform.LookAt(player);
            //transform.Rotate(transform.forward, 360 * Time.deltaTime);
            transform.position = Vector3.MoveTowards(transform.position, player.position, movementSpeed * Time.deltaTime);
        }

        // Only begin attacking if the player is within startAttackDistance and if the player's height is within
        // startAttackHeightRange of the enemy's height
        if (!attackMode && player != null &&
            (player.transform.position - transform.position).magnitude <= startAttackDistance &&
            Mathf.Abs(player.transform.position.y - yDifference - transform.position.y) < startAttackHeightRange)
        {
            attackMode = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && !LevelManager.isGameOver)
        {
            gameObject.GetComponent<Animator>().SetTrigger("doDie");
            AudioSource.PlayClipAtPoint(enemySound, Camera.main.transform.position);
            Destroy(gameObject, 0.5f);
        }
        if (collision.gameObject.CompareTag("Player") && !gameObject.GetComponent<Rigidbody>().isKinematic)
        {
            gameObject.GetComponent<Rigidbody>().isKinematic = true;
            gameObject.GetComponent<Animator>().SetTrigger("doAttack");
        }

    }
}
