using UnityEngine;

public class MeteorFall : MonoBehaviour
{
    public float minSpeed = 3f;
    public float maxSpeed = 6f;
    public float spinSpeed = 180f;

    private float speed;
    private GameManager gm;
    private Camera cam;
    private float zDist;

    void Start()
    {
        cam = Camera.main;
        gm = Object.FindFirstObjectByType<GameManager>();
        zDist = Mathf.Abs(cam.transform.position.z - transform.position.z);
        RespawnAtTop();
    }

    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime, Space.World);
        transform.Rotate(0, 0, spinSpeed * Time.deltaTime);

        Vector3 bottom = cam.ViewportToWorldPoint(new Vector3(0.5f, -0.1f, zDist));
        if (transform.position.y < bottom.y)
            RespawnAtTop();
    }

    void RespawnAtTop()
    {
        float x = Random.Range(-7f, 7f); 
        transform.position = new Vector3(x, 5f, 0f);
        speed = Random.Range(minSpeed, maxSpeed);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PinMovement>() != null)
        {
            Destroy(other.gameObject);
            return;
        }

        if (other.GetComponent<PlayerMovement>() != null)
        {
            if (gm != null) gm.RestartLevel();
        }
    }
}
