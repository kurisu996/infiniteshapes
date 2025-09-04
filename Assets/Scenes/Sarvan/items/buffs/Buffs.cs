using System.Collections;
using UnityEngine;

public class Buffs : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start(){
        StartCoroutine(despawn());
    }

    private IEnumerator despawn(){
        yield return new WaitForSeconds(15);
        Destroy(gameObject);
    }
}
