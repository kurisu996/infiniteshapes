using UnityEngine;

public class FollowPlayer : MonoBehaviour{
    [SerializeField] public bool corrupted = false;
    void LateUpdate() {
        transform.rotation = Quaternion.Euler(0f, 0f, 0f);
    }
}
