using System;
using UnityEngine;

public class Gun : MonoBehaviour {
    [SerializeField] private GameObject bulletprefab;
    [SerializeField] private Transform firingpoint;
    public float firerate = 0.15f;
    [SerializeField] private float timer;
    [SerializeField] public bool cursed = false;
    [SerializeField] public bool corrupted = false;
    public Quaternion rotation;
    
    private void Update() {
        timer -= Time.deltaTime;
        
        if (corrupted){
            rotation = UnityEngine.Random.rotation;
        } else{
            rotation = firingpoint.rotation;
        }
        
        if (Input.GetMouseButton(0) && timer <= 0f && !cursed){
            Instantiate(bulletprefab, firingpoint.position, rotation);
            timer = firerate;
        }  
    }
}