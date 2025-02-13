using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Cloudfade : MonoBehaviour{
    private SpriteRenderer _sr;

    void Start(){
        _sr = GetComponent<SpriteRenderer>();
    }
    
    /*private IEnumerator Fade(){
        for (int i = 1000; i >= 0; i--){
            _sr.color.a = i / 1000f;
            
        }

        yield return null;
    }*/
}
