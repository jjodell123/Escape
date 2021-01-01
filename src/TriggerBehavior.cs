using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerBehavior : MonoBehaviour
{
    public Material activeMaterial, inactiveMaterial;
    bool changedMaterial = false;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Renderer>().material = inactiveMaterial;
    }

    // Update is called once per frame
    void Update()
    {
        if (PickupBehavior.pickupCount == 0 && !changedMaterial)
        {
            gameObject.GetComponent<Renderer>().material = activeMaterial;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && PickupBehavior.pickupCount == 0)
        {
            LevelManager.isGameOver = true;
            FindObjectOfType<LevelManager>().LevelBeat();
        }
    }
}
