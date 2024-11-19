using UnityEngine;

public class Collision : MonoBehaviour {
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Obstacle") || collision.gameObject.CompareTag("Confusion") || collision.gameObject.CompareTag("Clear")) {
            // Debug.Log(gameObject.name + " collided with: " + collision.gameObject.name);
            Destroy(gameObject);
        }
    }
}
