using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDetection : MonoBehaviour{
    public bool playerDetected;
    public Vector2 dir;
    [SerializeField] private float _dist;
    private GameObject _player;
    [SerializeField] Image _icon;
    public bool sigma;
    
   private void Awake() {
        _player = GameObject.FindGameObjectWithTag("Player");
   }
    
    void Update() {
        Vector2 etp = _player.transform.position - transform.position;
        dir = etp.normalized;
        if (etp.magnitude <= _dist && !_player.GetComponent<Movement>().dead){
            playerDetected = true;
            StartCoroutine(Flash());
        } else{
            sigma = false;
            playerDetected = false;
        }
    }

    void LateUpdate(){
        _icon.transform.position = transform.position + Vector3.up * 1.25f;
        _icon.transform.rotation = Quaternion.identity;
    }

    private IEnumerator Flash(){
        if (!sigma){
            _icon.color = new Color(1f, 1f, 1f, 1f);
            yield return new WaitForSeconds(0.5f);
            _icon.color = new Color(1f, 1f, 1f, 0f);
        }
        sigma = true;
    }
}
