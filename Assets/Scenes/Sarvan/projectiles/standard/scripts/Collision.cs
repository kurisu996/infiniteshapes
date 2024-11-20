using UnityEngine;

public class Collision : MonoBehaviour {
    GameObject player;
    private GameObject cameramain;
    
    void Start(){
        player = GameObject.Find("Player");
        cameramain = GameObject.Find("Camera");
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Obstacle")) {
            // Debug.Log(gameObject.name + " collided with: " + collision.gameObject.name);
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Confusion")){
            player.GetComponent<Movement>().confused = true;
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Clear")){
            player.GetComponent<Movement>().confused = false;
            player.GetComponent<Movement>().bleeding = false;
            player.GetComponent<Movement>().speed = 10f;
            player.GetComponent<Gun>().corrupted = false;
            cameramain.GetComponent<FollowPlayer>().corrupted = false;
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Cursed")){
            player.GetComponent<Gun>().cursed = true;
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Bleeding")){
            player.GetComponent<Movement>().bleeding = true;
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Corrupted")){
            player.GetComponent<Gun>().corrupted = true;
            cameramain.GetComponent<FollowPlayer>().corrupted = true;
            Destroy(gameObject);
        }
    }
}
