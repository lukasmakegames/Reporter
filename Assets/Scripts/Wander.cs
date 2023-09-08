using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wander : MonoBehaviour
{
    public float speed = 2.0f; // Speed at which the UI image moves.
    public float wanderRadius = 100.0f; // Radius within which the UI image wanders.

    private RectTransform rectTransform;
    private Vector2 targetPosition;

    private float minX, maxX, minY, maxY;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        CalculateScreenBounds();
        SetRandomTarget();
    }

    void Update()
    {
        // Move the UI image towards the target position.
        rectTransform.localPosition = Vector2.MoveTowards(rectTransform.localPosition, targetPosition, speed * Time.deltaTime);

        // Check if the UI image has reached the target position.
        if (Vector2.Distance(rectTransform.localPosition, targetPosition) < 0.1f)
        {
            SetRandomTarget(); // Set a new random target position.
        }
    }

    void CalculateScreenBounds()
    {
        // Calculate screen bounds based on local coordinates within the Canvas.
        Canvas canvas = GetComponentInParent<Canvas>();
        RectTransform canvasRect = canvas.GetComponent<RectTransform>();
        
        minX = -canvasRect.rect.width / 2.0f + rectTransform.rect.width / 2.0f;
        maxX = canvasRect.rect.width / 2.0f - rectTransform.rect.width / 2.0f;
        minY = -canvasRect.rect.height / 2.0f + rectTransform.rect.height / 2.0f;
        maxY = canvasRect.rect.height / 2.0f - rectTransform.rect.height / 2.0f;
    }

    void SetRandomTarget()
    {
        // Generate a random target position within the wander radius.
        float randomX = Mathf.Clamp(Random.Range(minX, maxX), minX, maxX);
        float randomY = Mathf.Clamp(Random.Range(minY, maxY), minY, maxY);

        targetPosition = new Vector2(randomX, randomY);
    }
}
