using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour {

    public static PlayerCamera instance;
    
    // Use this for initialization
	void Start () {

        player = PlayerController.localPlayer.gameObject;
        //specifies that the player object is the parent of the object this scrript is attached to
        instance = this; //Specifies that there is only one existing instances of the camera

	}
	
	// Update is called once per frame
	void Update () {

        CameraMove();
        //Calls the camera movement function every frame

    }

    Vector2 cameraMove; 
    //An empty Vector2 (coordinates in x and y) to keep track of the total camera movement

    Vector2 smoothLook;
    //An empty Vector2 for the application of smoothing to the movement

    public float sensitivity = 4.0f; //Speed of camera movement
    public float smoothing = 2.0f; //Level of smoothing to the camera movement

    GameObject player; //Creates an instance of the GameObject under the name of player

    void CameraMove()
    {
        var mouseDelta = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        //Takes the position of the mouse in both the x and y axes every update

        mouseDelta = Vector2.Scale(mouseDelta, new Vector2(sensitivity * smoothing, sensitivity * smoothing));
        /* Multiplies the sensitivity and smmothing values with the changes in mouse position on both
        the x and y axes to specify the movement of the camera */

        smoothLook.x = Mathf.Lerp(smoothLook.x, mouseDelta.x, 1f / smoothing);
        smoothLook.y = Mathf.Lerp(smoothLook.y, mouseDelta.y, 1f / smoothing);
        cameraMove += smoothLook;
        //ensures that the camera moves between points rather than just changing position immediately
        //applies smoothing to the camera movement

        cameraMove.y = Mathf.Clamp(cameraMove.y, -90f, 90f);
        //restricts the camera movement when moving vertically

        transform.localRotation = Quaternion.AngleAxis(-cameraMove.y, Vector3.right);
        //applies the transformation around the x axis, allows the camera to look left and right
        player.transform.localRotation = Quaternion.AngleAxis(cameraMove.x, player.transform.up);
        //applies the transformation around the y axis, allows the camera to move up and down

    }
}
