using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenecMove : MonoBehaviour
{
    public void SettingScene()
    {
        SceneManager.LoadScene("Setting");
    }

    public void MainScene()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
