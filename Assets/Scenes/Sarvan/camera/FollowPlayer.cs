using UnityEngine;

public class FollowPlayer : MonoBehaviour {
    void LateUpdate() {
        transform.rotation = Quaternion.Euler(0f, 0f, 0f);
    }
}
