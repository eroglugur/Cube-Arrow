using UnityEngine;
using UnityEngine.SceneManagement;

public class InputManager : MonoBehaviour
{
    public void Restart()
    {
        SceneManager.LoadScene(0);
    }
}
