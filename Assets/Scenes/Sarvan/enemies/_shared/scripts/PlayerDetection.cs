using UnityEngine;

public class PlayerDetection : MonoBehaviour{
    public bool playerDetected;
    public Vector2 dir;
    [SerializeField] private float _dist;
    private GameObject _player;
    
   private void Awake() {
        _player = GameObject.FindGameObjectWithTag("Player");
    }
    
    void Update() {
        Vector2 etp = _player.transform.position - transform.position;
        dir = etp.normalized;
        if (etp.magnitude <= _dist && !_player.GetComponent<Movement>().dead){
            playerDetected = true;
        } else {
            playerDetected = false;
        }
    }
}
