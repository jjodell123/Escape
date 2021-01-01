using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyBehavior : MonoBehaviour
{
    public static int keyCount = 0;

    public AudioClip keyPickupSound;

    // Start is called before the first frame update
    void Start()
    {
        keyCount++;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !LevelManager.isGameOver)
        {
            AudioSource.PlayClipAtPoint(keyPickupSound, Camera.main.transform.position);
            gameObject.GetComponent<Animator>().SetTrigger("pickupDestroyed");

            keyCount--;

            if (keyCount == 0)
                FindObjectOfType<DoorBehavior>().OpenDoor();

            Destroy(gameObject, .4f);
        }


    }
}
