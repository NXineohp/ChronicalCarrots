using UnityEngine;
using UnityEngine.SceneManagement;

public class VictorySceneReturn : MonoBehaviour
{
    void Update()
    {
        if (Input.anyKeyDown || Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene(0);
        }
    }
}
