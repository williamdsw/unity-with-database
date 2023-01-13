using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Others;
using UnityEngine;
using UnityEngine.Networking;

namespace Controllers
{
    /// <summary>
    /// Controller for Initialization
    /// </summary>
    public class InitController : MonoBehaviour
    {
        /// <summary>
        /// Unity Start Event
        /// </summary>
        private IEnumerator Start()
        {
            yield return ExtractDatabase();
            yield return TableController.Instance.DrawTable();
        }

        /// <summary>
        /// Extract database to AppData folder
        /// </summary>
        private IEnumerator ExtractDatabase()
        {
            if (!File.Exists(Configuration.Properties.DatabasePath))
            {
#if UNITY_EDITOR || UNITY_STANDALONE
                File.Copy(Configuration.Properties.DatabaseStreamingAssetsPath, Configuration.Properties.DatabasePath);
#elif UNITY_ANDROID || UNITY_IOS
                yield return CopyDatabase();
#endif
            }

            yield return new WaitUntil(() => File.Exists(Configuration.Properties.DatabasePath));
        }

        /// <summary>
        /// Copy database file to AppData folder (Mobile - Android | iOS Only)
        /// </summary>
        private IEnumerator CopyDatabase()
        {
            using (UnityWebRequest request = UnityWebRequest.Get(Configuration.Properties.MobileDatabasePath))
            {
                yield return request.SendWebRequest();
                if (string.IsNullOrEmpty(request.error))
                {
                    File.WriteAllBytes(Configuration.Properties.DatabasePath, request.downloadHandler.data);
                }
            }
        }
    }
}
