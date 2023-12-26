using UnityEngine;

namespace Managers
{
    public class LevelManager : MonoBehaviour
    {
        public static LevelManager instance;

        private void Awake()
        {
            instance = this;
        }
    }
}
