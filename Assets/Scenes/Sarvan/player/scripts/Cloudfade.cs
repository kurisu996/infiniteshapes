using System.Collections;
using UnityEngine;

public class CloudFade : MonoBehaviour{
    private SpriteRenderer _sr;
    
    void Start(){
        _sr = GetComponent<SpriteRenderer>();
        _sr.color = new Color(_sr.color.r, _sr.color.g, _sr.color.b, 1f);
    }

    private IEnumerator Fade(){
        for (int i = 1000; i >= 0; i--){
            _sr.color = new Color(_sr.color.r, _sr.color.g, _sr.color.b, i/1000f);
            yield return new WaitForSeconds(0.001f);
        }

        yield return null;
    }
}
