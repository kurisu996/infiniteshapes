using System;
using UnityEngine;
using UnityEngine.Serialization;

public class Gun : MonoBehaviour {
    [SerializeField] private GameObject _bulletprefab;
    [SerializeField] private Transform _firingpoint;
    public float firerate = 0.15f;
    [SerializeField] private float timer;
    [SerializeField] public bool cursed = false;
    [SerializeField] public bool corrupted = false;
    [SerializeField] public bool fastfire = false;
    [SerializeField] public bool pierce = false;
    public Quaternion rotation;
    private GameObject _player;

    void Start(){
        _player = GameObject.FindGameObjectWithTag("Player");
    }
    
    private void Update() {
        timer -= Time.deltaTime;
        
        if (corrupted){
            rotation = UnityEngine.Random.rotation;
        } else{
            rotation = _firingpoint.rotation;
        }
        if (fastfire){
            firerate = 0.15f / 2f;
        } else {
            firerate = 0.15f;
        }
        if (Input.GetMouseButton(0) && timer <= 0f && !cursed && !_player.GetComponent<Movement>().dead){
            Instantiate(_bulletprefab, _firingpoint.position, rotation);
            timer = firerate;
        }
    }
}