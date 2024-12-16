using UnityEngine;

public class Particle : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void LateUpdate()
    {
        bool w = Input.GetKey(KeyCode.W);
        bool s = Input.GetKey(KeyCode.S);
        bool d = Input.GetKey(KeyCode.D);
        bool a = Input.GetKey(KeyCode.A);
        if (w) transform.rotation = Quaternion.Euler(0f, -90f, 0f);
        if (s) transform.rotation = Quaternion.Euler(0f, 90f, 0f);
        if (d) transform.rotation = Quaternion.Euler(0f, -270f, 0f);
        if (a) transform.rotation = Quaternion.Euler(0f, 90f, 0f);
        
    }
}
