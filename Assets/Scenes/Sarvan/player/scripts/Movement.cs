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

    [SerializeField] public GameObject deathcam;

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
            StartCoroutine(Cursed());
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
        sr.color = new Color(1f, 1f, 1f, 0f);
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

    private IEnumerator Cursed(){ 
        String cure = "in the name of the father and of the son and of the holy spirit amen";
        int i = 0;
        while (i < cure.Length){
            if (Input.anyKeyDown){
                if (!string.IsNullOrEmpty(Input.inputString)){
                    char letter = Input.inputString[0];
                    if (letter == cure[i]){
                        i++;
                    } else {
                        i = Mathf.Max(0, i - 1);
                    }
                }
                yield return new WaitUntil(() => !Input.anyKeyDown);
            }
            yield return null;
        }
        Debug.Log("Curse lifted");
        gameObject.GetComponent<Gun>().cursed = false;
    }
}


