using UnityEngine;

public class Spawner : MonoBehaviour{
    private Vector3[] bounds = new Vector3[2];
    [SerializeField] private GameObject enemy;
    [SerializeField] private int max;
    public int enemies;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bounds[0] = GameObject.Find("Bound1").transform.position;
        bounds[1] = GameObject.Find("Bound2").transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (enemies < max){
            Vector3 pos = new Vector3(Random.Range(bounds[0].x, bounds[1].x), Random.Range(bounds[0].y, bounds[1].y), 0);
            Instantiate(enemy, pos, Quaternion.identity);
            enemies++;
        }
    }
}
