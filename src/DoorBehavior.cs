using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBehavior : MonoBehaviour
{
    public float moveDistance = 1f;
    public float openTime = 2f;
    public AudioClip doorOpenSound;

    Vector3 startPosition, endPosition;
    bool doOpenDoor = false;
    float t = 0;
    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        endPosition = startPosition - transform.right * moveDistance;
    }

    // Update is called once per frame
    void Update()
    {
        if (doOpenDoor)
        {
            t += Time.deltaTime;
            transform.position = Vector3.Lerp(startPosition, endPosition, t / openTime);
            if (t >= openTime)
            {
                doOpenDoor = false;
            }
        }
    }

    public void OpenDoor()
    {
        doOpenDoor = true;
        AudioSource.PlayClipAtPoint(doorOpenSound, Camera.main.transform.position);
    }
}
