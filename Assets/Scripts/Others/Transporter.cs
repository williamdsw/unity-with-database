using UnityEngine;

namespace Others
{
    public class Transporter : MonoBehaviour
    {
        // Properties

        public int ScoreboardId { get; set; }
        public static Transporter Instance { get; private set; }

        private void Awake()
        {
            SetupSingleton();
        }

        private void SetupSingleton()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}