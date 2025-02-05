using UnityEngine;

public class MovementEnemy : MonoBehaviour{
    [SerializeField] public float speed;
    [SerializeField] public float speed_rotation;
    [SerializeField] private Vector2 vel;
    private Rigidbody2D _rb;
    private PlayerDetection _detection;
    private Vector2 _direction;
    private bool _dirdetermined;
    
    private void Awake() {
        _rb = GetComponent<Rigidbody2D>();
        _detection = GetComponent<PlayerDetection>();
    }

    
    private void FixedUpdate() {
        updateRotation();
        if (_detection.playerDetected){
            updateDirection();
        } else if (!_dirdetermined) {
            _direction = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
            _dirdetermined = true;
        }
        updateSpeed();
        _rb.angularVelocity = 0f;
        vel = _rb.linearVelocity;
    }

    private void updateDirection(){
        _direction = _detection.dir;
        _dirdetermined = false;
    }

    private void updateRotation(){
        Quaternion target = Quaternion.LookRotation(transform.forward, _direction);
        Quaternion rotation = Quaternion.RotateTowards(transform.rotation, target, Time.deltaTime * speed_rotation);
        _rb.SetRotation(rotation);
    }

    private void updateSpeed(){
        if (!_detection.playerDetected){
            _rb.linearVelocity = _direction * (speed/3);
        }
        else{
            _rb.linearVelocity = _direction * speed;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision){
        if (collision.gameObject.CompareTag("Obstacle")){
            Debug.Log("blallbvalbvl");
            _direction = (_direction) * -1;
        }
    }
}
