using UnityEngine;

public class Shield : MonoBehaviour{
    [SerializeField] public GameObject player;
    private Rigidbody2D _rb;

    void Start(){
        _rb = GetComponent<Rigidbody2D>();
    }
}
