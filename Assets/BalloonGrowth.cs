using System.Collections;
using UnityEngine;

public class BalloonGrowth : MonoBehaviour
{
    public float growRate = 0.1f;
    public float maxSize = 3f;

    private GameManager gameManager;

    void Start()
    {
        gameManager = Object.FindFirstObjectByType<GameManager>();
        InvokeRepeating(nameof(Grow), 1f, 1f);
    }

    void Grow()
    {
        transform.localScale += Vector3.one * growRate;

        if (transform.localScale.x >= maxSize)
        {
            CancelInvoke(nameof(Grow));
            if (gameManager != null) gameManager.RestartLevel();
            else Debug.LogWarning("GameManager not found!");
            Destroy(gameObject);
        }
    }

    public void Pop()
    {
        CancelInvoke(nameof(Grow));

        if (gameManager != null)
            gameManager.AddScore(transform.localScale.x);

        // Start the delay on THIS object and DON'T destroy yet
        StartCoroutine(AdvanceAfter(0.2f));
    }

    private IEnumerator AdvanceAfter(float delay)
    {
        // If you ever use Time.timeScale = 0, switch to WaitForSecondsRealtime
        yield return new WaitForSeconds(delay);

        if (gameManager != null) gameManager.LoadNextLevel();
        // No need to Destroy here; scene load will unload this object anyway
    }
}
