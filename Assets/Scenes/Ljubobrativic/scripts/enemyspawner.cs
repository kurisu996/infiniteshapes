using UnityEngine;

public class enemyspawner : MonoBehaviour
{
    public GameObject enemy;
    public float spawnrate = 2;
    private float timer = 0;
    private float hardtimer = 0;
    public float offset = 10;

    public GameObject player;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timer < spawnrate)
        {
            timer += (float) (Time.deltaTime * 1.5);
        }
        else
        {
            spawnEnemy();
            timer = 0;
        }
        if (hardtimer > 20 && spawnrate > 1)
        {
            spawnrate--;
            hardtimer = 0;
        } else
        {
            hardtimer += Time.deltaTime;
        }
    }

    void spawnEnemy()
    {
        float minx = transform.position.x - offset;
        float maxx = transform.position.x + offset;
        float miny = transform.position.y - offset;
        float maxy = transform.position.y + offset;

        GameObject newEnemy = Instantiate(enemy, new Vector3(Random.Range(minx,maxx), Random.Range(miny,maxy), 0), transform.rotation);

        newEnemy.GetComponent<enemy>().target = player;


    }
}
