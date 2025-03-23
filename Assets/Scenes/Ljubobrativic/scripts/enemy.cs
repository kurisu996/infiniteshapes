using UnityEngine;

public class enemy : MonoBehaviour
{
    public GameObject target;
    public float speed = 2;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(target);

        transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed*Time.deltaTime);
    }
}
