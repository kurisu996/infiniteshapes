using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Playgame : MonoBehaviour{
    [SerializeField] Camera cam;
    
    public void playgame(){
        cam.transform.position = new Vector3 (17, 0, -10);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
}
