using UnityEngine;

public class Gun : MonoBehaviour {
    [SerializeField] private GameObject bulletprefab;
    [SerializeField] private Transform firingpoint;
    public float firerate = 0.15f;
    [SerializeField] private float timer;
    
    private void Update() {
        timer -= Time.deltaTime;

        if (Input.GetMouseButton(0) && timer <= 0f) {
            Instantiate(bulletprefab, firingpoint.position, firingpoint.rotation);
            timer = firerate;
        }  
    }
}