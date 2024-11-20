using UnityEngine;

public class Tri : MonoBehaviour
{
    private Rigidbody2D rb2d;
    [SerializeField] Vector2 movement = new Vector2(0, 0);
    
    [Range(1f, 20f)]
    [SerializeField] public float speed = 7f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        
        movement = new Vector2(x, y);

        if (movement.x > 1 || movement.y > 1)
        {
            movement = movement.normalized;
        }
        rb2d.linearVelocity = movement * speed;
        rb2d.angularVelocity = 0f;
        
        
        Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouse.z = 0;
        Vector3 direction = mouse - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
    }
}
