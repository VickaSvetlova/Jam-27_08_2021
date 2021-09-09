using UnityEngine;
using UnityEngine.SceneManagement;

namespace DefaultNamespace
{
    public class ReloadScene : MonoBehaviour
    {
        public void OnPress()
        {
            
            SceneManager.LoadScene(1);
        }
    }
}