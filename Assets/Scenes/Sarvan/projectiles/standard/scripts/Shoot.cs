using UnityEngine;

public class Shoot : MonoBehaviour {
    [Range(15, 35)]
    [SerializeField] public float speed = 25f;

    private Rigidbody2D rb;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate() {
        Vector3 screenMin = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane));
        Vector3 screenMax = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, Camera.main.nearClipPlane));

        if (transform.position.x < screenMin.x || transform.position.x > screenMax.x ||
            transform.position.y < screenMin.y || transform.position.y > screenMax.y) {
            Destroy(gameObject);
        }

        rb.linearVelocity = transform.up * speed;
    }

}
