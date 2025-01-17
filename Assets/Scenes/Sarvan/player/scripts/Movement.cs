using System;
using System.Collections;
using UnityEngine;

public class Movement : MonoBehaviour{
    public float speed = 10f;
    private Rigidbody2D rb;

    [SerializeField] private Vector2 vel;
    [SerializeField] public float deathtimer = 3f;
    [SerializeField] public bool confused = false;
    [SerializeField] public bool bleeding = false;
    [SerializeField] public bool canMove = true;
    [SerializeField] public bool invincible = false;
    [SerializeField] public bool corrupted = false;
    [SerializeField] public bool speedboost = false;
    private int _cursedtimer = 0;
    [SerializeField] public GameObject deathcam;
    [SerializeField] public Sprite normal;
    [SerializeField] public Sprite white;
    [SerializeField] public GameObject shield;
    public bool shieldactive = false;

    //[SerializeField] public GameObject player;
    //[SerializeField] public GameObject bullet;
    Vector2 _movement = Vector2.zero;
    private GameObject _cameramain;
    private Collider2D _collider;
    SpriteRenderer _sr;

    private void Start(){
        rb = GetComponent<Rigidbody2D>();
        _cameramain = GameObject.Find("Camera");
        _sr = GetComponent<SpriteRenderer>();
        _collider = GetComponent<Collider2D>();
        // Debug.Log("hi");
    }

    private void Update(){
        if (canMove && !gameObject.GetComponent<Gun>().cursed){
            float movex = Input.GetAxisRaw("Horizontal");
            float movey = Input.GetAxisRaw("Vertical");
            if (bleeding && !speedboost){
                speed = 4f;
            }
            else if (speedboost && !bleeding){
                speed = 16f;
            }
            else if (speedboost && bleeding){
                speed = 10f;
            }

            if (confused){
                _movement = new Vector2(movex * -1, movey * -1);
            }
            else{
                _movement = new Vector2(movex, movey);
            }

            rb.linearVelocity = _movement * speed;

            Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouse.z = 0;
            Vector3 direction = mouse - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }

        if (gameObject.GetComponent<Gun>().cursed){
            String cure = "test";
            if (_cursedtimer < cure.Length && Input.anyKeyDown){
                if (Input.inputString[0] == cure[_cursedtimer]){
                    Debug.Log($"Correct: {Input.inputString[0]} - {cure[_cursedtimer]}\n{_cursedtimer + 1}/{cure.Length}");
                    _cursedtimer++;
                } else if (Input.inputString[0] != cure[_cursedtimer]){
                    Debug.Log($"False: {Input.inputString[0]} - {cure[_cursedtimer]}\n{_cursedtimer + 1}/{cure.Length}");
                }
            } else if (_cursedtimer == cure.Length){
                gameObject.GetComponent<Gun>().cursed = false;
            }
        } else {
            _cursedtimer = 0;
        }

        if (shieldactive){
            
        }

        rb.angularVelocity = 0f;
        vel = rb.linearVelocity;
    }

    private void OnCollisionEnter2D(Collision2D collision){
        if (collision.gameObject.CompareTag("Obstacle")){
            Debug.Log(gameObject.name + " collided with: " + collision.gameObject.name);
            rb.linearVelocity = new Vector2(0, 0);
        }

        if (collision.gameObject.CompareTag("Clear")){
            confused = false;
            bleeding = false;
            corrupted = false;
            speedboost = false;
            canMove = true;
            speed = 10f;
            gameObject.GetComponent<Gun>().cursed = false;
            gameObject.GetComponent<Gun>().corrupted = false;
            gameObject.GetComponent<Gun>().fastfire = false;
            gameObject.GetComponent<Gun>().pierce = false;
        }

        if (collision.gameObject.CompareTag("Enemy") && !invincible){
            StartCoroutine(Death());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if (collision.gameObject.CompareTag("Damage") && !invincible){
            StartCoroutine(Death());
        }
    }

    private IEnumerator Death(){
        canMove = false;
        _collider.enabled = false;
        rb.linearVelocity = Vector2.zero;
        _sr.sprite = white;
        yield return new WaitForSeconds(0.01f);
        _sr.color = new Color(1f, 1f, 1f, 0f);
        _sr.sprite = normal;
        yield return new WaitForSeconds(deathtimer);
        _sr.color = new Color(1f, 1f, 1f, 1f);
        transform.position = Vector3.zero;
        canMove = true;
        _collider.enabled = true;
        StartCoroutine(Respawn());
    }

    private IEnumerator Respawn(){
        invincible = true;
        yield return new WaitForSeconds(0.1f);
        for (int i = 0; i < 5; i++){
            _sr.color = new Color(1f, 1f, 1f, 0f);
            yield return new WaitForSeconds(0.1f);
            _sr.color = new Color(1f, 1f, 1f, 1f);
            yield return new WaitForSeconds(0.1f);
        }

        invincible = false;
    }
}


