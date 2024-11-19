using UnityEngine;

public class Collision : MonoBehaviour {
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Obstacle")) {
            // Debug.Log(gameObject.name + " collided with: " + collision.gameObject.name);
            Destroy(gameObject);
        }
    }
}
