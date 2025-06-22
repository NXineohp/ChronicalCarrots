using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public void StartGamesceneOne(int index)
    {
        SceneManager.LoadScene(index);
    }
}
