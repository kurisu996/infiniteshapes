using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;
public class Collision : MonoBehaviour {
    GameObject player;
    private GameObject cameramain;
    public float rand;
    private Rigidbody2D rb;
    
    void Start(){
        player = GameObject.Find("Player");
        cameramain = GameObject.Find("Camera");
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        rb.AddForce (transform.right *Time.deltaTime * 1f);
    }
    
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Enemy")){
            if (!player.GetComponent<Gun>().pierce){
                Destroy(gameObject);
            }
        }
        if (collision.gameObject.CompareTag("Enemy_Triangle")){
            rand = Random.Range(0f, 1f);
            if (!player.GetComponent<Gun>().pierce && rand > 1/3f){
                Destroy(gameObject);
            } else if (rand < 1/3f){
                transform.Rotate(0f, 0f, 180f);
            }
        }
        if (collision.gameObject.CompareTag("Enemy_Square")) {
            if (!player.GetComponent<Gun>().pierce){
                Destroy(gameObject);
            }
        }
        if (collision.gameObject.CompareTag("Obstacle")) {
            // Debug.Log(gameObject.name + " collided with: " + collision.gameObject.name);
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Confusion")){
            player.GetComponent<Movement>().confused = true;
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Clear")){
            player.GetComponent<Movement>().confused = false;
            player.GetComponent<Movement>().bleeding = false;
            player.GetComponent<Movement>().speed = 10f;
            player.GetComponent<Gun>().corrupted = false;
            player.GetComponent<Gun>().fastfire = false;
            player.GetComponent<Gun>().pierce = false;
            player.GetComponent<Movement>().speedboost = false;
            Debug.Log("pierce = false");
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Cursed")){
            player.GetComponent<Gun>().cursed = true;
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Bleeding")){
            player.GetComponent<Movement>().bleeding = true;
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Corrupted")){
            player.GetComponent<Gun>().corrupted = true;
            player.GetComponent<Movement>().corrupted = true;
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("FastFire")){
            player.GetComponent<Gun>().fastfire = true;
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Pierce")){
            player.GetComponent<Gun>().pierce = true;
            Debug.Log("pierce = true");
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Speed")){
            player.GetComponent<Movement>().speedboost = true;
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Shield")){
            player.GetComponent<Movement>().shieldactive = true;
            Destroy(gameObject);
        }
    }
}
