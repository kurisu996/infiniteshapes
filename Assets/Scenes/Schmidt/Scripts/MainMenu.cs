using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Texture2D cursorTexture;
    public CursorMode cursorMode = CursorMode.ForceSoftware;
    public Vector2 hotSpot = Vector2.zero;
    void Start()
    {
        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
    }
    public void PlayGame()
    {
        SceneManager.LoadScene("Assets/Scenes/Schmidt/SchmidtTestRange.unity");
    }
}
