using UnityEngine;

public class Shield : MonoBehaviour{
    [SerializeField] public GameObject player;
    [SerializeField] public bool shieldactive;
    private Rigidbody2D _rb;
    private SpriteRenderer _sr;
    
    void Start(){
        _rb = GetComponent<Rigidbody2D>();
        _sr = GetComponent<SpriteRenderer>();
    }

    private void LateUpdate(){
        transform.localRotation = Quaternion.Euler(0f,0f,0f);
        if (shieldactive){
            Debug.Log("Shield Active: " + shieldactive);
            _sr.color = new Color(_sr.color.r, _sr.color.g, _sr.color.b, 1f);
        } else {
            _sr.color = new Color(_sr.color.r, _sr.color.g, _sr.color.b, 0f);
        }
    }
}
