using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    bool hasLost = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -.5f && !hasLost && !LevelManager.isGameOver)
        {
            LevelManager.isGameOver = true;
            FindObjectOfType<LevelManager>().LevelLost();
            hasLost = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            LevelManager.isGameOver = true;
            FindObjectOfType<LevelManager>().LevelLost();
            Destroy(gameObject, .4f);
        }
    }
}
