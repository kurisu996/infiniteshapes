using System;
using System.Collections;
using TMPro;
using UnityEngine;
using Random = System.Random;

public class Movement : MonoBehaviour{
    public float speed = 10f;
    private Rigidbody2D rb;
    private Collider2D _col;

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
    [SerializeField] public bool dead = false;
    [SerializeField] public float dashtimer = 0f;
    [SerializeField] public bool dashing = false;
    [SerializeField] private TextMeshProUGUI text;
    //[SerializeField] public TextMeshProUGUI text2;
    [SerializeField] public GameObject cloud;

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
        _col = gameObject.GetComponent<Collider2D>();
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

            if (_movement.magnitude > 1){
                _movement.Normalize();
            }

            if (!dashing){
                rb.linearVelocity = _movement * speed;
            }

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

        rb.angularVelocity = 0f;
        vel = rb.linearVelocity;
        dashtimer -= Time.deltaTime;
        
        if (Input.GetKeyDown(KeyCode.LeftShift) && canMove && dashtimer <= 0f){
            StartCoroutine(Dash());
        }

        if (dashtimer <= 0f){
            text.text = "Dash Active";
        }
        else{
            text.text = "";
        }
        
        //text2.text = "Enemies Killed: " + victims;
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
            if (!dashing){
                StartCoroutine(Death());
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if (collision.gameObject.CompareTag("Damage") && !invincible){
            StartCoroutine(Death());
        }

        if (collision.gameObject.layer == 3){
            rb.linearVelocity = Vector2.zero;
        }
    }

    private IEnumerator Death(){
        canMove = false;
        dead = true;
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
        dead = false;
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

    private IEnumerator Dash(){
        dashing = true;
        _col.isTrigger = true;
        gameObject.tag = "Damage";
        Instantiate(cloud, transform.position, Quaternion.identity);
        rb.linearVelocity = _movement * 30f;
        dashtimer = 3f;
        yield return new WaitForSeconds(0.15f);
        gameObject.tag = "Player";
        _col.isTrigger = false;
        dashing = false;
    }
}


