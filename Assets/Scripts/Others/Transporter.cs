using UnityEngine;

namespace Others
{
    /// <summary>
    /// Transporter class for data
    /// </summary>
    public class Transporter : MonoBehaviour
    {
        public long ScoreboardId { get; set; }
        public bool IsUpdate { get; set; } = false;
        public static Transporter Instance { get; private set; }

        /// <summary>
        /// Unity Awake Event
        /// </summary>
        private void Awake() => SetupSingleton();

        /// <summary>
        /// Setup singleton
        /// </summary>
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