using System.Collections;
using UnityEngine;

public class Status_effects : MonoBehaviour{
    [SerializeField] public GameObject p_cursed;
    public bool p_cursed_active = false;
    private GameObject[] p_curseds = new GameObject[4];
    private float[] angles = new float[4];
    
    
    void Start(){
        for (int i = 0; i < angles.Length; i++){
            angles[i] = i * 90f;
        }
    }
    
    void Update() {
        if (GetComponent<Gun>().cursed && !p_cursed_active){
            p_curseds[0] = Instantiate(p_cursed, new Vector3(transform.position.x + 1f, transform.position.y + 1f, -0.01f), Quaternion.identity);
            p_curseds[1] = Instantiate(p_cursed, new Vector3(transform.position.x + 1f, transform.position.y - 1f, -0.01f), Quaternion.identity);
            p_curseds[2] = Instantiate(p_cursed, new Vector3(transform.position.x - 1f, transform.position.y + 1f, -0.01f), Quaternion.identity);
            p_curseds[3] = Instantiate(p_cursed, new Vector3(transform.position.x - 1f, transform.position.y - 1f, -0.01f), Quaternion.identity);
            p_cursed_active = true;
        } else if (!GetComponent<Gun>().cursed && p_cursed_active){
            foreach (GameObject p in p_curseds){
                Destroy(p);
            }
            p_cursed_active = false;
        }
    }

    void LateUpdate(){
        if (p_cursed_active){
            for (int i = 0; i < p_curseds.Length; i++){
                angles[i] += 20f * Time.deltaTime;
                float rad = angles[i] * Mathf.Deg2Rad;
                float x = transform.position.x + Mathf.Cos(rad) * 1.4f;
                float y = transform.position.y + Mathf.Sin(rad) * 1.4f;
                p_curseds[i].transform.position = new Vector3(x, y, -0.01f);
            }
        }
    }
}
