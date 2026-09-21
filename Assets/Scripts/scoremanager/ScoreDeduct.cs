using UnityEngine;
using TMPro;

public class ScoreDeduct : MonoBehaviour
{
    [Header("Score")]
    public ScoreTrigger scoreTrigger;

    [Header("UI")]
    public TMP_Text scoreText;
    public GameObject panel;

    [Header("Game Object To Disable")]
    public GameObject objectToDisable;

    [Header("Cooldown")]
    public float cooldown = 3f;

    private bool canDeduct = true;

    private void Start()
    {
        if (panel != null)
        {
            panel.SetActive(false);
        }

        UpdateScoreUI();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Ball"))
            return;

        if (!canDeduct)
            return;

        if (scoreTrigger == null)
            return;

        scoreTrigger.score--;

        // Don't allow score to go below 0
        if (scoreTrigger.score < 0)
        {
            scoreTrigger.score = 0;
        }

        UpdateScoreUI();

        // Only activate when score reaches 0
        if (scoreTrigger.score == 0)
        {
            if (panel != null)
            {
                panel.SetActive(true);
            }

            if (objectToDisable != null)
            {
                objectToDisable.SetActive(false);
            }
        }

        canDeduct = false;

        Invoke(nameof(ResetCooldown), cooldown);

        Debug.Log("Score: " + scoreTrigger.score);
    }

    private void ResetCooldown()
    {
        canDeduct = true;
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null && scoreTrigger != null)
        {
            scoreText.text = "Score: " + scoreTrigger.score;
        }
    }
}