using System;
using UnityEngine;

 [RequireComponent(typeof(ThirdPerson))]

public class PlayerKeyControler : MonoBehaviour {
    private ThirdPerson character;
    private Transform player;
    private Transform camera;
    private Vector3 cameraForward, move;

    private bool isJump;
    private float mouseUp = 0.0f, mouseSide = 0.0f;


    private void Start(){
        if (Camera.main != null) camera = Camera.main.transform;
        else UnityEngine.Debug.Log("[WARNING] Not found main camera!");

        character = GetComponent<ThirdPerson>();
        player = GameObject.FindWithTag("Player").transform;
    }
	
	private void Update(){
        //if (!isJump) isJump = CrossPlatformInputManager.GetButtonDown("Jump");

        //...
	}

    private void FixedUpdate(){
        //float h = CrossPlatformInputManager.GetAxis("Horizontal");
        //float v = CrossPlatformInputManager.GetAxis("Vertical");
        /*bool crouch = Input.GetKey(KeyCode.C);

        if (camera != null){
            cameraForward = Vector3.Scale(camera.forward, new Vector3(1, 0, 1)).normalized;

            move = v * cameraForward + h * camera.right;
        } else move = v * cameraForward + h * Vector3.right;

        if (Input.GetKey(KeyCode.LeftShift)) move *= 0.5f;

        character.Move(move, crouch, isJump);

         isJump = false;*/
    }
}
