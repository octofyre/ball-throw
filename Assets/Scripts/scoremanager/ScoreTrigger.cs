using UnityEngine;
using TMPro;

public class ScoreTrigger : MonoBehaviour
{
    [Header("Score")]
    public int score = 0;

    [Header("UI")]
    public TMP_Text scoreText;

    [Header("Cooldown")]
    public float cooldown = 3f;

    private bool canScore = true;

    private void Start()
    {
        UpdateScoreUI();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Ball"))
            return;

        if (!canScore)
            return;

        score++;

        UpdateScoreUI();

        canScore = false;

        Invoke(nameof(ResetCooldown), cooldown);

        Debug.Log("Score increased! Score: " + score);
    }

    private void ResetCooldown()
    {
        canScore = true;
    }

    public void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }
}