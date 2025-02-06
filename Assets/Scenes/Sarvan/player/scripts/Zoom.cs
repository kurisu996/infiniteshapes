using UnityEngine;

public class Zoom : MonoBehaviour{
    private float min = 3.8f;
    private float max = 7.4f;
    [SerializeField] public Camera cam;
    void LateUpdate() {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0){
            cam.orthographicSize -= scroll * 3f;
            cam.orthographicSize = Mathf.Clamp(cam.orthographicSize, min, max);
        }
    }
}
