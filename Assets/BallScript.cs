using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BallScript : MonoBehaviour {

    [SerializeField]
    public static BallScript singleton; //Creates a singleton within this script
    
    // Use this for initialization
	void Start () {
        singleton = this; //Specifies that this script is the singleton
	}
}
