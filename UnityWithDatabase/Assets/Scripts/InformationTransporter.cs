using UnityEngine;

public class InformationTransporter : MonoBehaviour
{
    // State
    private int scoreID;
    public static InformationTransporter instance;

    //-----------------------------------------------------------------------------//
    // GETTERS / SETTERS

    public int GetScoreID () { return this.scoreID; }
    public void SetScoreID (int scoreID) { this.scoreID = scoreID; }

    //-----------------------------------------------------------------------------//

    private void Awake () 
    {
        SetupSingleton ();
    }

    //-----------------------------------------------------------------------------//

    private void SetupSingleton ()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad (this.gameObject);
        }
        else 
        {
            Destroy (this.gameObject);
        }
    }
}