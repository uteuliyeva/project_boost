using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadFirstLevel()
    {
        SceneManager.LoadScene("1-Over");
    }

    public void LoadFirstMenu()
    {
        SceneManager.LoadScene("0-StartMenu");
    }
}
