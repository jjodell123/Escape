using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public float levelDuration = 100f;
    public float scoreValue = 1f;

    public Text timerText, gameText, scoreText;

    public AudioClip gameOverSound;
    public AudioClip gameWonSound;

    public string nextLevel;

    public static bool isGameOver = false;
    
    private float score = 0f;
    private float countDown, countDownStart;

    // Start is called before the first frame update
    void Start()
    {
        countDown = levelDuration;
        countDownStart = countDown;

        isGameOver = false;

        SetTimer();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isGameOver)
        {
            if (countDown > 0) // Counts down game
                countDown -= Time.deltaTime;
            else // Game lost
            {
                countDown = 0.0f;
                isGameOver = true;
                LevelLost();
            }

            SetTimer();
        }
        scoreText.text = score.ToString();
    }

    private void SetTimer()
    {
        timerText.text = countDown.ToString("f2");
    }

    public void IncreaseScore()
    {
        if (countDown >= (countDownStart / 2))
            score += 2 * scoreValue;
        else
            score += scoreValue;
    }

    public void LevelLost()
    {
        gameText.text = "Level Failed!";
        gameText.gameObject.SetActive(true);

        //Camera.main.GetComponent<AudioSource>().pitch = 1f;
        AudioSource.PlayClipAtPoint(gameOverSound, Camera.main.transform.position);

        Invoke("LoadCurrentLevel", 2);
    }

    public void LevelBeat()
    {
        gameText.gameObject.SetActive(true);

        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
            enemy.GetComponent<Rigidbody>().isKinematic = true;

        AudioSource.PlayClipAtPoint(gameWonSound, Camera.main.transform.position);

        if (!string.IsNullOrEmpty(nextLevel))
        {
            gameText.text = "Level Passed!";
            Invoke("LoadNextLevel", 2);
        } else
        {
            gameText.text = "You Beat The Game!";
        }
            
    }

    void LoadNextLevel()
    {
        PickupBehavior.ResetPickupCount();
        SceneManager.LoadScene(nextLevel);
    }

    void LoadCurrentLevel()
    {
        PickupBehavior.ResetPickupCount();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
