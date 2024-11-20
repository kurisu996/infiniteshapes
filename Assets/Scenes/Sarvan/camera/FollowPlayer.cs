using Unity.VisualScripting;
using UnityEngine;

public class FollowPlayer : MonoBehaviour{
    [SerializeField] public bool corrupted = false;
    GameObject player;

    void Start(){
        player = GameObject.Find("Player");
    }
    
    void LateUpdate() {
        if (!corrupted){
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
    }

    void Update(){
        if (corrupted){
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
    }
}
