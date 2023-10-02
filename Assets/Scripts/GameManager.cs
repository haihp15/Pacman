using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public Ghost[] ghosts;
    public Pacman pacman;
    public Transform pellets;

    public GameObject gameOver;
    public int ghostMutiplier { get; private set; } = 1;
    public int score { get; private set; }
    public int lives { get; private set; }
    public int highScore { get; private set; }
    public int maxScore { get; private set; }

    public TMP_Text livesDisplay;
    public TMP_Text scoreDisplay;
    public TMP_Text highScoreDisplay;

    public AudioSource pelletMusic;
    public AudioSource powerPelletMusic;
    public AudioSource deathMusic;
    public AudioSource ghostMusic;
    public AudioSource gameOverMusic;
    public AudioSource victoryMusic;

    public GameObject pauseScreen;




    private void Awake()
    {
        //pauseScreen.SetActive(false);
        gameOver.SetActive(false);
    }
    private void Start()
    {
        NewGame();

    }

    private void NewGame()
    {
        SetScore(0);
        SetLives(3);
        NewRound();
    }

    private void Update()
    {
        livesDisplay.text = ("x " + lives.ToString());
        scoreDisplay.text = score.ToString();
        if (this.lives <= 0 && Input.anyKeyDown)
        {
            NewGame();
        }
        //Pausescreen();
        //CheckHighScore();


    }

    private void NewRound()
    {
        foreach (Transform pellet in this.pellets)
        {
            pellet.gameObject.SetActive(true);
        }

        ResetState();
    }

    private void ResetState()
    {
        gameOver.SetActive(false);
        ResetGhostMutiplier();
        for (int i = 0; i < this.ghosts.Length; i++)
        {
            this.ghosts[i].ResetState();
        }

        this.pacman.ResetState();

    }

    private void SetScore(int score)
    {
        this.score = score;
    }

    private void SetLives(int lives)
    {
        this.lives = lives;
    }

    private void GameOver()
    {

        gameOverMusic.Play();
        for (int i = 0; i < this.ghosts.Length; i++)
        {

            this.ghosts[i].gameObject.SetActive(false);
        }

        this.pacman.gameObject.SetActive(false);

        gameOver.SetActive(true);
    }

    public void GhostEaten(Ghost ghost)
    {
        ghostMusic.Play();
        SetScore(this.score + (ghost.points * this.ghostMutiplier));
        this.ghostMutiplier++;
    }

    public void PacmanEaten()
    {
        //deathMusic.Play();
        this.pacman.gameObject.SetActive(false);

        SetLives(this.lives - 1);

        if (this.lives > 0)
        {
            Invoke(nameof(ResetState), 3.0f);
        }
        else
        {
            GameOver();
        }
    }

    public void PelletEaten(Pellet pellet)
    {
        pelletMusic.Play();
        pellet.gameObject.SetActive(false);
        SetScore(this.score + pellet.points);
        if (!HasRemainingPellet())
        {
            victoryMusic.Play();
            this.gameObject.SetActive(false);
            Invoke(nameof(NewRound), 3.0f);
        }
    }

    public void PowerPelletEaten(PowerPellet pellet)
    {
        powerPelletMusic.Play();
        for (int i = 0; i < ghosts.Length; i++)
        {
            this.ghosts[i].frightened.Enable(pellet.duration);
        }

        PelletEaten(pellet);
        CancelInvoke();
        Invoke(nameof(ghostMutiplier), pellet.duration);

    }

    private bool HasRemainingPellet()
    {
        foreach (Transform pellet in this.pellets)
        {
            if (pellet.gameObject.activeSelf)
            {
                return true;
            }
        }

        return false;
    }

    private void ResetGhostMutiplier()
    {
        this.ghostMutiplier = 1;
    }
    public void ResumeGame()
    {
        Time.timeScale = 1f;
        pauseScreen.SetActive(false);
    }
    //public void Pausescreen()
    //{
    //    if (Input.GetKeyDown(KeyCode.Escape))
    //    {
    //        Time.timeScale = 0f;
    //        pauseScreen.SetActive(true);
    //    }
    //}
    //private void CheckHighScore()
    //{
    //    //if (score > maxScore)
    //    //{
    //    //    maxScore = score;
    //    //}
    //    //highScore = maxScore;
    //    //PlayerPrefs.SetInt("HighScore", highScore);
    //    //highScoreDisplay.text = ("HighScore: " + PlayerPrefs.GetInt("HighScore", highScore).ToString());

    //}

}