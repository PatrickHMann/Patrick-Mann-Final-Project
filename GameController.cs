using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject[] hazards;

    public Vector3 spawnValues;
    public Vector3 spawnValuesH;
    public Vector3 horizontalSpawn;
    public Quaternion hSpawn;
    

    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;

    public Text ScoreText;
    private int score;

    public Text restartText;
    public Text gameOverText;
    public Text ShieldText;

    private bool gameOver;
    private bool restart;

    public Mover Mover;

    public int shield;

    public AudioClip musicClipVictory;

    public AudioClip musicClipFailure;

    public AudioSource musicSource;

    public bool accelerate;

    public bool hardMode;
    public bool flank;
    public float hWait;


    void Start()
    {
        accelerate = false;
        hardMode = false;
        flank = false;
        gameOver = false;
        restart = false;
        restartText.text = "Press H for Hard Mode";
        gameOverText.text = "";
        ShieldText.text = "Press F for Flank Attack Mode";
        score = 0;
        UpdateScore();
        StartCoroutine(SpawnWaves());
        shield = 0;
    }
   public void Update()
    {
        if (Input.GetKey("escape"))
            Application.Quit();

        if (restart)
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                SceneManager.LoadScene("SampleScene");
            }
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            if (hardMode == false)
            {
                hardMode = true;
                restartText.text = "Hard Mode Active!";
                waveWait = 0;
                spawnWait = 0.2f;
            }
            else
            {
                hardMode = false;
                restartText.text = "Press H for hard mode";
                waveWait = 4;
                spawnWait = 0.5f;
            }

        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            if(flank == false)
            {
                flank = true;
                ShieldText.text = "Flank Attack Mode active!";
                StartCoroutine(FlankAttack());
            }
            else
            {
                flank = false;
                ShieldText.text = "Press F for Flank Attack Mode";
                StopCoroutine(FlankAttack());
            }
        }
    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (true) { 
            for (int i = 0; i < hazardCount; i++)
            {
                GameObject hazard = hazards[Random.Range(0, hazards.Length)];
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);

            if (gameOver)
            {
                restartText.text = "Press 'P' for restart";
                restart = true;
                break;
            }
        }
    }


    IEnumerator FlankAttack()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                GameObject hazard = hazards[Random.Range(0, hazards.Length)];
                Vector3 horizontalSpawn = new Vector3(Random.Range (- spawnValuesH.x, spawnValues.x), spawnValuesH.y, spawnValuesH.z);

                Quaternion hSpawn = new Quaternion(0.0f, 90.0f, 0.0f, 1);

                Instantiate(hazard, horizontalSpawn, hSpawn);

                yield return new WaitForSeconds(hWait);
            }
            yield return new WaitForSeconds(waveWait);

            if (gameOver)
            {
                restartText.text = "Press 'P' for restart";
                restart = true;
                break;
            }
        }
    }


    public void UpdateScore()
    {
        ScoreText.text = "Points: " + score;
        if (score >= 300)
        {
            musicSource.clip = musicClipVictory;
            musicSource.Play();
            accelerate = true;
            gameOverText.text = "You win!  Game created by Patrick Mann";
            gameOver = true;
            restart = true;
        }

    }
    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore();
    }

    public void GameOver()
    {
        musicSource.clip = musicClipFailure;
        musicSource.Play();
        gameOverText.text = "Game Over!  Game created by Patrick Mann";
        gameOver = true;
    }

}
