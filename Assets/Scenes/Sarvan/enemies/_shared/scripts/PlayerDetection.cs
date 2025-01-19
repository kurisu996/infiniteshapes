using UnityEngine;

public class PlayerDetection : MonoBehaviour{
    public bool playerDetected;
    public Vector2 dir;
    [SerializeField] private float _dist;
    private Transform _player;
    
   private void Awake() {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    
    void Update() {
        Vector2 etp = _player.position - transform.position;
        dir = etp.normalized;
        if (etp.magnitude <= _dist){
            playerDetected = true;
        } else if (!_player.GetComponent<Movement>().canMove){
            playerDetected = false;
        }
    }
}
