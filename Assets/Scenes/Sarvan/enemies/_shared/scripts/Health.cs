using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour {
    [SerializeField] public float health;
    private Rigidbody2D _rb;
    private SpriteRenderer _sr;

    void Start() {
        _rb = GetComponent<Rigidbody2D>();
        _sr = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.CompareTag("Damage")) {
            // Debug.Log("enemy damaged");
            if (Random.Range(0f, 1f) < 1 / 4f && CompareTag("Enemy_Square")){
                StartCoroutine(Heal());
            } else {
                StartCoroutine(Flash());
                StartCoroutine(Death());
            }
        }
    }

    private IEnumerator Flash(){
        _sr.color = Color.white;
        yield return new WaitForSeconds(0.05f);
        _sr.color = Color.red;
        health--;
    }

    private IEnumerator Death(){
        if (health <= 1){
            _sr.color = Color.white;
            yield return new WaitForSeconds(0.05f);
            Destroy(gameObject);
        }
    }
    
    private IEnumerator Heal(){
        _sr.color = Color.green;
        yield return new WaitForSeconds(0.05f);
        _sr.color = Color.red;
        health++;
    }
}