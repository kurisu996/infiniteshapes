using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour {
    [SerializeField] public float health = 2f;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    [SerializeField] private GameObject script;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.CompareTag("Damage")) {
            // Debug.Log("enemy damaged");
            StartCoroutine(Flash());
            StartCoroutine(Death());
        }
    }

    private IEnumerator Flash(){
        sr.color = Color.white;
        yield return new WaitForSeconds(0.05f);
        sr.color = Color.red;
        health--;
    }

    private IEnumerator Death(){
        if (health <= 1){
            sr.color = Color.white;
            yield return new WaitForSeconds(0.05f);
            Destroy(gameObject);
        }
    }
}
