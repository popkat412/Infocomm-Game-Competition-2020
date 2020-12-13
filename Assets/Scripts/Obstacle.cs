using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public float maxMoveSpeed = 20, minMoveSpeed = 15;
    public float epsilon = 30;

    float currentMoveSpeed;
    float currentMoveVel;
    bool isSlowingDown = false;
    public float slowDownDuration = 0.1f;

    Player player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        player.OnPlayerDeath += StopMovingGradually;
    }

    void Update()
    {
        if (!isSlowingDown)
            currentMoveSpeed = Mathf.Lerp(minMoveSpeed, maxMoveSpeed, Difficulty.GetDifficultyPercent());
        else
            currentMoveSpeed = Mathf.SmoothDamp(currentMoveSpeed, 0, ref currentMoveVel, slowDownDuration);

        transform.Translate(Vector3.forward * currentMoveSpeed * Time.deltaTime, Space.World);

        if (transform.position.z > Camera.main.transform.position.z + epsilon)
            Destroy(gameObject);
    }

    void StopMovingGradually()
    {
        isSlowingDown = true;
    }

    float EaseOutCubic(float x)
    {
        return 1 - Mathf.Pow(1 - x, 3);
    }
}
