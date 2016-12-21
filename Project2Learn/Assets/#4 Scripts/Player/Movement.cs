using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {

     public float speedMoving = 2;
     public float jumpHeight = 2;
     public float speedRotating = 5;
     public float gravityMultiply = 1;

    private CharacterController character;

     private Transform characterBody;
     private Vector3 currentPosition;

     private float currentSpeedX = 0f;
     private float currentSpeedZ = 0f;

     private float currentJumpHeight = 0f;
     private float currentRotateFix = 0f;

    private Transform currentCamera;

     private Vector3 cameraForward;
     private Vector3 currentXZPositionCamera;



    void Start () {
        if (Camera.main != null)
            currentCamera = Camera.main.transform;
        else UnityEngine.Debug.Log("[WARNING] Main.Camera not found!");

        character = this.GetComponent<CharacterController>();
        characterBody = this.GetComponentInChildren<MeshRenderer>().transform;

        currentXZPositionCamera = new Vector3(currentCamera.position.x, characterBody.position.y, currentCamera.position.z);
    }

	void Update () {
        if (character.isGrounded) {
            this.getMovingKeys();

            if (currentSpeedZ >= 1f || currentSpeedZ <= -1f || currentSpeedX >= 1f || currentSpeedX <= -1f) {
                characterBody.rotation = Quaternion.Slerp(characterBody.rotation, Quaternion.LookRotation(characterBody.position - currentXZPositionCamera), speedRotating * Time.deltaTime);

                //characterBody.Rotate(Vector3.up * currentRotateFix);
            }
        } else currentJumpHeight += (Physics.gravity).y * gravityMultiply * Time.deltaTime;
    }

    void LateUpdate() {
        currentXZPositionCamera = new Vector3(currentCamera.position.x, characterBody.position.y, currentCamera.position.z);

        cameraForward = Vector3.Scale(currentCamera.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 playerMove = (currentSpeedZ * cameraForward) + (currentSpeedX * currentCamera.right) + (new Vector3(0, currentJumpHeight, 0));

        character.Move(playerMove * Time.deltaTime); //TODO
    }


    private void getMovingKeys() {
        currentSpeedX = Input.GetAxis("Horizontal") * speedMoving;
        currentSpeedZ = Input.GetAxis("Vertical") * speedMoving;

        if (!Input.GetKey(KeyCode.W)){
            currentSpeedZ /= 2f;
        }


        if (Input.GetKeyDown(KeyCode.W)){
            currentRotateFix = 0f;
        }
        else if (Input.GetKeyDown(KeyCode.S)){
            currentRotateFix = 180f;
        }
        else if (Input.GetKeyDown(KeyCode.A)){
            currentRotateFix = 270f;
        }
        else if (Input.GetKeyDown(KeyCode.D)){
            currentRotateFix = 90f;
        }

        else if (Input.GetKey(KeyCode.LeftShift)){ //running
            currentSpeedX *= 2f;
            currentSpeedZ *= 2f;
        }
        else if (Input.GetKey(KeyCode.LeftControl)){ //walking
            currentSpeedX *= 0.5f;
            currentSpeedZ *= 0.5f;
        }

        

        if (Input.GetKeyDown(KeyCode.Space)) {
            currentJumpHeight = jumpHeight;
        }
    }
}
