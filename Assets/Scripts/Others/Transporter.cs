using UnityEngine;

namespace Others
{
    public class Transporter : MonoBehaviour
    {
        // Properties

        public long ScoreboardId { get; set; }
        public bool IsUpdate { get; set; } = false;
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