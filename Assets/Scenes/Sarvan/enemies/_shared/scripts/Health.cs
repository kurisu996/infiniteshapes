using System.Collections;
using TMPro;
using Unity.Mathematics.Geometry;
using UnityEngine;
using UnityEngine.UIElements;

public class Health : MonoBehaviour {
    [SerializeField] public float health;
    [SerializeField] public static GameObject player;
    private Rigidbody2D _rb;
    private SpriteRenderer _sr;
    private Color _init;
    [SerializeField] public GameObject speed;
    [SerializeField] public GameObject fastfire;
    [SerializeField] public GameObject pierce;
    public GameObject[] buffs = new GameObject[3];

    void Start() {
        _rb = GetComponent<Rigidbody2D>();
        _sr = GetComponent<SpriteRenderer>();
        _init = _sr.color;
        player = GameObject.FindGameObjectWithTag("Player");
        buffs[0] = speed;
        buffs[1] = fastfire;
        buffs[2] = pierce;
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
        _sr.color = _init;
        health--;
    }

    private IEnumerator Death(){
        if (health <= 1){
            _sr.color = Color.white;
            yield return new WaitForSeconds(0.05f);
            //player.GetComponent<Movement>().victims++;
            float i = Random.Range(0f, 1f);
            Debug.Log(i);
            if (i > 0.95){
                Instantiate(buffs[Mathf.RoundToInt(Random.Range(0, buffs.Length))], transform.position, Quaternion.identity);
            }
            Destroy(gameObject);
            player.GetComponent<Movement>().enemydeaths++;
            GameObject.Find("Spawner").GetComponent<Spawner>().enemies--;
        }
    }
    
    private IEnumerator Heal(){
        _sr.color = Color.green;
        yield return new WaitForSeconds(0.05f);
        _sr.color = Color.red;
        health++;
    }
}
