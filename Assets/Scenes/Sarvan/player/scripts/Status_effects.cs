using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class Status_effects : MonoBehaviour{
    [SerializeField] public GameObject pCursed; 
    public bool pCursedActive = false;
    private GameObject[] _pCurseds = new GameObject[4];
    private float[] _angles = new float[4];
    
    
    void Start(){
        for (int i = 0; i < _angles.Length; i++){
            _angles[i] = i * 90f;
        }
    }
    
    void Update() {
        if (GetComponent<Gun>().cursed && !pCursedActive){
            _pCurseds[0] = Instantiate(pCursed, new Vector3(transform.position.x + 1f, transform.position.y + 1f, -0.01f), Quaternion.identity);
            _pCurseds[1] = Instantiate(pCursed, new Vector3(transform.position.x + 1f, transform.position.y - 1f, -0.01f), Quaternion.identity);
            _pCurseds[2] = Instantiate(pCursed, new Vector3(transform.position.x - 1f, transform.position.y + 1f, -0.01f), Quaternion.identity);
            _pCurseds[3] = Instantiate(pCursed, new Vector3(transform.position.x - 1f, transform.position.y - 1f, -0.01f), Quaternion.identity);
            pCursedActive = true;
        } else if (!GetComponent<Gun>().cursed && pCursedActive){
            foreach (GameObject p in _pCurseds){
                Destroy(p);
            }
            pCursedActive = false;
        }
    }

    void LateUpdate(){
        if (pCursedActive){
            for (int i = 0; i < _pCurseds.Length; i++){
                _angles[i] += 20f * Time.deltaTime;
                float rad = _angles[i] * Mathf.Deg2Rad;
                float x = transform.position.x + Mathf.Cos(rad) * 1.4f;
                float y = transform.position.y + Mathf.Sin(rad) * 1.4f;
                _pCurseds[i].transform.position = new Vector3(x, y, -0.01f);
            }
        }
    }
}
