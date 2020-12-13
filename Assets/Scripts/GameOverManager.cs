using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public GameObject gameOverScreen;
    public Text secondsAliveText;
    public GameObject highScoreIndicator;
    Player player;

    bool gameOver = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        player.OnPlayerDeath += OnGameOver;

        gameOverScreen.SetActive(false);
    }

    void Update()
    {
        if (gameOver && Input.GetKeyDown(KeyCode.Space)) {
            SceneManager.LoadScene(0);
        }
    }

    void OnGameOver()
    {
        gameOver = true;
        gameOverScreen.SetActive(true);

        float timing = Time.timeSinceLevelLoad;

        float highscore = PlayerPrefs.GetFloat(Constants.highscoreKey, 0);
        bool newHighscore = highscore < timing;
        print("highscore: " + highscore);
        highScoreIndicator.SetActive(newHighscore);

        secondsAliveText.text = Utilities.FormatSecondsAsTiming(timing);

        if (newHighscore)
        {
            PlayerPrefs.SetFloat(Constants.highscoreKey, timing);
            PlayerPrefs.Save();
        }
    }
}
