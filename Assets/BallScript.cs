using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BallScript : MonoBehaviour {

    [SerializeField]
    public static BallScript singleton;
    
    // Use this for initialization
	void Start () {
        singleton = this;
	}
}
