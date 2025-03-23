using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb2d;
    [SerializeField] private Vector2 movement;
    public float speed = 7f;
    public GameObject gameOverScreen; // Referenz für den Game Over Screen
    public GameObject projectilePrefab; // Referenz für das Projektil-Prefab
    public float projectileSpeed = 10f; // Geschwindigkeit des Projektils
    public float projectileLifetime = 5f; // Lebensdauer des Projektils in Sekunden

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Wenn das Spiel angehalten ist, nichts tun
        if (Time.timeScale == 0)
        {
            return; // Stoppt alle Bewegungs- und Drehungsberechnungen
        }

        // MOVEMENT
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        movement = new Vector2(x, y).normalized; // Immer normalisieren
        rb2d.linearVelocity = movement * speed; // Korrektur: "velocity" statt "linearVelocity"

        // ROTATION ZUM MAUSZEIGER
        Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouse.z = 0;
        Vector3 direction = mouse - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);

        // Projektil schießen bei Linksklick
        if (Input.GetMouseButtonDown(0)) // Linksklick (0)
        {
            ShootProjectile();
        }
    }

    private void ShootProjectile()
    {
        // Berechne die Position der Spitze des Dreiecks (abhängig von der Rotation)
        Vector3 spawnPosition = transform.position + (transform.up * 0.5f); // 'up' ist die nach oben zeigende Richtung des Dreiecks

        // Instanziiere das Projektil
        GameObject projectile = Instantiate(projectilePrefab, spawnPosition, transform.rotation);

        // Berechne die Richtung zum Mauszeiger
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0; // Setze die z-Koordinate auf 0, um sicherzustellen, dass es im 2D-Raum bleibt

        // Berechne die Richtung des Projektils
        Vector2 direction = (mousePosition - transform.position).normalized;

        // Gib dem Projektil eine konstante Geschwindigkeit
        Rigidbody2D projectileRb = projectile.GetComponent<Rigidbody2D>();
        projectileRb.linearVelocity = direction * projectileSpeed; // Setze die Geschwindigkeit mit einer konstanten Geschwindigkeit

        // Starte Coroutine, um das Projektil nach der festgelegten Zeit zu zerstören
        Destroy(projectile, projectileLifetime);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Time.timeScale = 0; // Spiel anhalten
            gameOverScreen.SetActive(true); // Game Over Screen anzeigen
        }
    }
}
