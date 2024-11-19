using UnityEngine;

public class Health : MonoBehaviour {
    [SerializeField] public float health = 2f;
    private Rigidbody2D rb;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.CompareTag("Damage")) {
            // Debug.Log("enemy damaged");
            Destroy(collision.gameObject);
            health--;
        }
    }
    
    private void Update() {
        if(health <= 0) {
            Destroy(gameObject);
        }
    }
}
