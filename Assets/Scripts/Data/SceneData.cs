using UnityEngine;
using UnityEngine.SceneManagement;

namespace Platformer
{
    public class SceneData : MonoBehaviour
    {
        public CoreUI UI;
        public Camera Camera;
        public void ReloadScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
