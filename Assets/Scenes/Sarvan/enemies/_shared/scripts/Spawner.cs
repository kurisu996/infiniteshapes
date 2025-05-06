using Unity.Mathematics.Geometry;
using Unity.VisualScripting;
using UnityEngine;

public class Spawner : MonoBehaviour{
    private Vector3[] bounds = new Vector3[2];
    [SerializeField] private GameObject enemy;
    [SerializeField] private GameObject enemy_square;
    [SerializeField] private GameObject enemy_triangle;
    private GameObject[] enemiess;
    [SerializeField] private int max;
    private float temp = 1.1f;
    public int enemies;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start(){
        max = 10;
        bounds[0] = GameObject.Find("Bound1").transform.position;
        bounds[1] = GameObject.Find("Bound2").transform.position;
        enemiess = new GameObject[]{ enemy, enemy_square, enemy_triangle };
    }

    // Update is called once per frame
    void Update(){
        if (Mathf.Abs(GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>().modifier - temp) > 0.0001f || Mathf.Abs(GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>().modifier - temp) < -0.0001f){
            max = (int)(10 * GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>().modifier);
            temp = GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>().modifier;
        }
        if (enemies < max){
            Vector3 pos = new Vector3(Random.Range(bounds[0].x, bounds[1].x), Random.Range(bounds[0].y, bounds[1].y), 0);
            Instantiate(enemiess[Random.Range(0, enemiess.Length)], pos, Quaternion.identity);
            enemies++;
        }
    }
}
