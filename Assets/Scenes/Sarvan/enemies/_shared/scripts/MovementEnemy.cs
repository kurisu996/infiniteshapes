using UnityEngine;

public class MovementEnemy : MonoBehaviour{
    [SerializeField] public float speed;
    [SerializeField] public float speed_rotation;
    [SerializeField] private Vector2 vel;
    private Rigidbody2D _rb;
    private PlayerDetection _detection;
    private Vector2 _direction;
    
    private void Awake() {
        _rb = GetComponent<Rigidbody2D>();
        _detection = GetComponent<PlayerDetection>();
    }

    
    private void FixedUpdate() {
        updateDirection();
        updateRotation();
        updateSpeed();
        _rb.angularVelocity = 0f;
        vel = _rb.linearVelocity;
    }

    private void updateDirection(){
        if (_detection.playerDetected){
            _direction = _detection.dir;
        } else {
            _direction = new Vector2(Random.Range(0f, 360f), Random.Range(0f, 360f)).normalized;
        }
    }

    private void updateRotation(){
        Quaternion target = Quaternion.LookRotation(transform.forward, _direction);
        Quaternion rotation = Quaternion.RotateTowards(transform.rotation, target, Time.deltaTime * speed_rotation);
        _rb.SetRotation(rotation);
    }

    private void updateSpeed(){
        if (_direction == Vector2.zero){
            _rb.linearVelocity = _direction * (speed/10);
        }
        else{
            _rb.linearVelocity = _direction * speed;
        }
    }
}
