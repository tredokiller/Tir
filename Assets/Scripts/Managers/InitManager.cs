using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers
{
    public class InitManager : MonoBehaviour
    {
        private void Start()
        {
            SceneManager.LoadScene(1);
        }
    }
}
