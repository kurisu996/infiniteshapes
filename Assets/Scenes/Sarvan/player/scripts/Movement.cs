using System;
using System.Collections;
using UnityEngine;

public class Movement : MonoBehaviour{
    public float speed = 10f;
    private Rigidbody2D rb;

    [SerializeField] public float deathtimer = 3f;
    [SerializeField] public bool confused = false;
    [SerializeField] public bool bleeding = false;
    [SerializeField] public bool canMove = true;
    [SerializeField] public bool invincible = false;
    [SerializeField] public bool corrupted = false;
    [SerializeField] public bool speedboost = false;
    private int cursedtimer = 0;
    [SerializeField] public GameObject deathcam;
    [SerializeField] public Sprite normal;
    [SerializeField] public Sprite white;

    //[SerializeField] public GameObject player;
    //[SerializeField] public GameObject bullet;
    Vector2 movement = Vector2.zero;
    private GameObject cameramain;
    SpriteRenderer sr;

    private void Start(){
        rb = GetComponent<Rigidbody2D>();
        cameramain = GameObject.Find("Camera");
        sr = GetComponent<SpriteRenderer>();
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
            else if (speedboost & bleeding){
                speed = 10f;
            }

            if (confused){
                movement = new Vector2(movex * -1, movey * -1);
            }
            else{
                movement = new Vector2(movex, movey);
            }

            rb.linearVelocity = movement * speed;

            Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouse.z = 0;
            Vector3 direction = mouse - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
            if (corrupted){
                transform.Rotate(new Vector3(0, 0, UnityEngine.Random.Range(-10, 10)));
            }
        }

        if (gameObject.GetComponent<Gun>().cursed){
            String cure = "test";
            if (cursedtimer < cure.Length && Input.anyKeyDown){
                if (Input.inputString[0] == cure[cursedtimer]){
                    Debug.Log($"Correct: {Input.inputString[0]} - {cure[cursedtimer]}\n{cursedtimer + 1}/{cure.Length}");
                    cursedtimer++;
                } else if (Input.inputString[0] != cure[cursedtimer]){
                    Debug.Log($"False: {Input.inputString[0]} - {cure[cursedtimer]}\n{cursedtimer + 1}/{cure.Length}");
                }
            } else if (cursedtimer == cure.Length){
                gameObject.GetComponent<Gun>().cursed = false;
            }
        } else {
            cursedtimer = 0;
        }

        rb.angularVelocity = 0f;
    }

    private void OnCollisionEnter2D(Collision2D collision){
        if (collision.gameObject.CompareTag("Obstacle")){
            // Debug.Log(gameObject.name + " collided with: " + collision.gameObject.name);
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

    private IEnumerator Death(){
        canMove = false;
        rb.linearVelocity = Vector2.zero;
        sr.sprite = white;
        yield return new WaitForSeconds(0.01f);
        sr.color = new Color(1f, 1f, 1f, 0f);
        sr.sprite = normal;
        yield return new WaitForSeconds(deathtimer);
        sr.color = new Color(1f, 1f, 1f, 1f);
        transform.position = Vector3.zero;
        canMove = true;
        StartCoroutine(Respawn());
    }

    private IEnumerator Respawn(){
        invincible = true;
        yield return new WaitForSeconds(0.1f);
        for (int i = 0; i < 5; i++){
            sr.color = new Color(1f, 1f, 1f, 0f);
            yield return new WaitForSeconds(0.1f);
            sr.color = new Color(1f, 1f, 1f, 1f);
            yield return new WaitForSeconds(0.1f);
        }

        invincible = false;
    }
}


