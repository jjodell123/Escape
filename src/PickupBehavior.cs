using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupBehavior : MonoBehaviour
{
    public static int pickupCount = 0;

    public AudioClip pickupSound;

    // Start is called before the first frame update
    void Start()
    {
        pickupCount++;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !LevelManager.isGameOver)
        {
            AudioSource.PlayClipAtPoint(pickupSound, Camera.main.transform.position);

            gameObject.GetComponent<Animator>().SetTrigger("pickupDestroyed");

            FindObjectOfType<LevelManager>().IncreaseScore();
            pickupCount--;

            Destroy(gameObject, .4f);
        }
    }


    public static void ResetPickupCount()
    {
        pickupCount = 0;
    }
}
