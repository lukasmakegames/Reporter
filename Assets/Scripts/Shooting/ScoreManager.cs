using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public GameObject scoreDisplayPrefab;
    public Transform scoreDisplayParent;
    public Text totalScoreText;
    public float scoreAnimationDuration = 1f;
    public float displayTime = 1.5f;

    private int score = 0;

    [HideInInspector]
    public static ScoreManager Instance { get; private set; }
    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    void Start()
    {
        // Initialize the score manager.
        scoreDisplayParent = scoreDisplayParent != null ? scoreDisplayParent : transform;
    }

    public void HitTarget(Vector3 hitPosition, int pointsPerHit, Color color)
    {
        // Create a new score display instance.
        GameObject scoreDisplay = Instantiate(scoreDisplayPrefab, scoreDisplayParent);
        Text scoreText = scoreDisplay.GetComponent<Text>();
        scoreText.text = "+" + pointsPerHit.ToString();
        scoreDisplay.transform.position = hitPosition;

        scoreDisplay.GetComponent<Text>().color = color;

        // Animate the score display using a coroutine.
        StartCoroutine(AnimateScoreDisplay(scoreDisplay));

        // Increase the score.
        score += pointsPerHit;
        totalScoreText.text = "Score:" + (score).ToString().PadLeft(5, '0');
    }

    private IEnumerator AnimateScoreDisplay(GameObject scoreDisplay)
    {
        // Move the score display upwards.
        float elapsedTime = 0f;
        Vector3 startPosition = scoreDisplay.transform.position;
        Vector3 targetPosition = startPosition + Vector3.up * 1f;

        while (elapsedTime < scoreAnimationDuration)
        {
            scoreDisplay.transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / scoreAnimationDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Wait for a short duration.
        yield return new WaitForSeconds(displayTime);

        // Fade out the score display.
        Color startColor = scoreDisplay.GetComponent<Text>().color;
        Color targetColor = new Color(startColor.r, startColor.g, startColor.b, 0f);
        elapsedTime = 0f;

        while (elapsedTime < scoreAnimationDuration)
        {
            float alpha = Mathf.Lerp(startColor.a, targetColor.a, elapsedTime / scoreAnimationDuration);
            scoreDisplay.GetComponent<Text>().color = new Color(startColor.r, startColor.g, startColor.b, alpha);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Destroy the score display.
        Destroy(scoreDisplay);
    }
}
