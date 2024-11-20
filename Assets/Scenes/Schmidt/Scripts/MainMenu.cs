using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("Assets/Scenes/Schmidt/SchmidtTestRange.unity");
    }
}
