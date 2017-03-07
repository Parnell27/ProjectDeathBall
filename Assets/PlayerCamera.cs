using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : PlayerController {

	// Use this for initialization
	void Start () {

        player = this.transform.parent.gameObject;
        //specifies that the player object is the parent of the object this scrript is attached to

	}
	
	// Update is called once per frame
	void Update () {

        CameraMove();

    }

    Vector2 cameraMove;
    Vector2 smoothLook;
    public float sensitivity = 4.0f;
    public float smoothing = 2.0f;

    GameObject player;

    void CameraMove()
    {
        var mouseDelta = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        mouseDelta = Vector2.Scale(mouseDelta, new Vector2(sensitivity * smoothing, sensitivity * smoothing));

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
