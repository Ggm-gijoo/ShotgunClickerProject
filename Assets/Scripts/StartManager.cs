using UnityEngine;
using UnityEngine.SceneManagement;

public class StartManager : MonoBehaviour
{
    public void onClickStart()
    {
        SceneManager.LoadScene("Main");
    }
}

