using System;
using UnityEngine;

public class Movement : MonoBehaviour {
    public float speed = 10f;
    private Rigidbody2D rb;
    [SerializeField] public bool confused = false;
    [SerializeField] public bool bleeding = false;
    Vector2 movement = Vector2.zero;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        // Debug.Log("hi");
    }

    private void Update() {
        float movex = Input.GetAxisRaw("Horizontal");
        float movey = Input.GetAxisRaw("Vertical");
        if (bleeding){
            speed = 4f;
        }
        if (confused) {
            movement = new Vector2(movex * -1, movey * -1);
        } else {
            movement = new Vector2(movex, movey);
        }
        rb.linearVelocity = movement * speed;
        rb.angularVelocity = 0f;

        Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouse.z = 0;
        Vector3 direction = mouse - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Obstacle")) {
            // Debug.Log(gameObject.name + " collided with: " + collision.gameObject.name);
            rb.linearVelocity = new Vector2(0, 0);
        }
        if(collision.gameObject.CompareTag("Clear")) {
            confused = false;
            bleeding = false;
            speed = 10f;
            gameObject.GetComponent<Gun>().cursed = false;
            gameObject.GetComponent<Gun>().corrupted = false;
        }
        if(collision.gameObject.CompareTag("Enemy")) {
            Destroy(gameObject);
        }
    }
}
