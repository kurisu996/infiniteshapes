using Unity.VisualScripting;
using UnityEngine;

public class FollowPlayer : MonoBehaviour{
    public GameObject player;

    void Start(){
        player = GameObject.Find("Player");
    }
    void Update(){
        if (player.GetComponent<Movement>().corrupted){
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
    }
    
    void LateUpdate(){
        if (!player.GetComponent<Movement>().corrupted){
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
    }
}
