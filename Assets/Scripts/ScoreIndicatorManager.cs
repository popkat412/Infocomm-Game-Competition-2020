using UnityEngine;
using UnityEngine.UI;

public class ScoreIndicatorManager : MonoBehaviour
{
    public GameObject scoreIndicator;
    public Text scoreIndicatorLabel;

    Player player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        player.OnPlayerDeath += OnGameOver;

        scoreIndicator.SetActive(true);
    }

    void Update()
    {
        scoreIndicatorLabel.text = Utilities.FormatSecondsAsTiming(Time.timeSinceLevelLoad, true);
    }

    void OnGameOver()
    {
        scoreIndicator.SetActive(false);
    }
}
