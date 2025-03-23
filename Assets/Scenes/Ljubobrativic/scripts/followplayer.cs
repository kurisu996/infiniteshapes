using UnityEngine;

public class followplayer : MonoBehaviour
{
    [SerializeField] Transform target; // Setze dein Spieler-Objekt hier rein
    [SerializeField] Vector3 offset = new Vector3(0, 0, -10); // Kamera-Offset

    private void LateUpdate()
    {
        if (target != null)
        {
            transform.position = target.position + offset; // Folgt dem Spieler
        }
    }
}
