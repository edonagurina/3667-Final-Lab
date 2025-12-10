using UnityEngine;

public class PopOnContact : MonoBehaviour
{
    public AudioClip popSound;
    private AudioSource audioSource;
    private GameManager gameManager;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        // Use the modern API to find the GameManager
        gameManager = Object.FindFirstObjectByType<GameManager>();
    }

    void OnTriggerEnter2D(Collider2D other)
{
    if (other.CompareTag("Balloon"))
    {
        if (popSound != null)
            AudioSource.PlayClipAtPoint(popSound, transform.position);

        var balloon = other.GetComponent<BalloonGrowth>();
        if (balloon != null) balloon.Pop();

        Destroy(gameObject); 
    }
}

}
