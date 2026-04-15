using UnityEngine;
using UnityEngine.SceneManagement;

namespace Code.UI
{
    public class SceneChanger : MonoBehaviour
    {
        public void ChangeScene(string sceneName) => SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }
}