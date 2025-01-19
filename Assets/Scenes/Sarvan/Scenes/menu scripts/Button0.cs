using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button0 : MonoBehaviour{
    [SerializeField] Camera cam;
    
    public void playtest(){
        cam.transform.position = new Vector3(17, 0, -10);
        SceneManager.LoadScene(2);
    }
}
