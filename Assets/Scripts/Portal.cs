using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("schonmal der Trigger");

        if (collision.gameObject.CompareTag("Armi") || collision.gameObject.CompareTag("Hasi"))
        {
            Debug.Log("Trigger ausgelöst durch Spieler");

            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            int nextSceneIndex = currentSceneIndex + 1;

            SceneManager.LoadScene(nextSceneIndex);
        }
    }
}
