using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public void GoToScene(string sceneName) { // function for loading new scene
        SceneManager.LoadScene(sceneName);
    }
        
    public void QuitApp() { // function for quitting application
        Application.Quit();
        Debug.Log("Application has quit.");
    }
}
