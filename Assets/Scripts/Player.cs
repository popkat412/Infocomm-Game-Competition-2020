using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject explosion;
    public float minMoveSpeed = 9, maxMoveSpeed = 50, timeToMaxSpeed = 1;
    public float maxTiltAngle = 60, timeToMaxTiltAngle = 0.3f, tiltSpeed = 10;
    float angle;

    float startMoveTime = -1;

    bool hasExploded = false;

    public event System.Action OnPlayerDeath;

    public AudioClip explosionClip;

    void Update()
    {

        float inputX = Input.GetAxisRaw("Horizontal");

        // Calculate how long key has been held down
        if (inputX != 0f)
        {
            if (startMoveTime == -1)
                startMoveTime = Time.timeSinceLevelLoad;
        }
        else
            startMoveTime = -1;

        float keyDownDuration = 0;
        if (startMoveTime != -1)
            keyDownDuration = Time.timeSinceLevelLoad - startMoveTime;
        float tiltKeyDownDurationPercent = Mathf.Clamp01(keyDownDuration / timeToMaxTiltAngle);
        float speedKeyDownDurationPercent = Mathf.Clamp01(keyDownDuration / timeToMaxSpeed);


        // Movement
        float difficultyMoveSpeed = Mathf.Lerp(minMoveSpeed, maxMoveSpeed, Difficulty.GetDifficultyPercent());
        float keyDownDurationMoveSpeed = Mathf.Lerp(minMoveSpeed, maxMoveSpeed, speedKeyDownDurationPercent);
        float moveSpeed = difficultyMoveSpeed + keyDownDurationMoveSpeed;
        float velocity = inputX * moveSpeed;
        transform.Translate(Vector3.left * velocity * Time.deltaTime, Space.World);

        // Tilt
        float targetAngle = Mathf.Lerp(0, maxTiltAngle, tiltKeyDownDurationPercent);
        if (inputX > 0) targetAngle = -targetAngle;
        angle = Mathf.LerpAngle(angle, targetAngle, Time.deltaTime * tiltSpeed);
        transform.eulerAngles = new Vector3(0, 180, angle); // y has to be 180 because everything is facing in the opposite way

    }

    void OnTriggerEnter(Collider triggerCollider)
    {
        if (triggerCollider.tag == "Obstacle")
        {
            // Game over!
            if (!hasExploded)
            {
                print("Hit!");

                GameObject newExplosion = Instantiate(explosion, transform.position, Quaternion.identity);
                newExplosion.transform.localScale = Vector3.one * 40;
                hasExploded = true;

                if (OnPlayerDeath != null)
                    OnPlayerDeath();
                
                Destroy(gameObject);
            }
        }
    }
}
