using UnityEngine;


 [RequireComponent(typeof( Rigidbody ))]
 [RequireComponent(typeof( CapsuleCollider ))]
 [RequireComponent(typeof( Animator ))]

public class ThirdPerson : MonoBehaviour {
    [SerializeField] float movingTurnSpeed = 360;
    [SerializeField] float stationaryTurnSpeed = 180;
    [SerializeField] float jumpPower = 6f;

    [Range(1f, 4f)][SerializeField] float gravityMultiplier = 2f;

    [SerializeField] float runCycleLegOffset = 0.2f;
    [SerializeField] float moveSpeedMultiplier = 1f;
    [SerializeField] float animSpeedMultiplier = 1f;
    [SerializeField] float groundCheckDistance = 0.3f;


     Rigidbody rigidbody;
     Animator animator;
     CapsuleCollider capsule;

    bool isGrounded;
    float origGroundCheckDistance;
    float capsuleHeight;

    float turnAmount;
    float forwardAmount;

    Vector3 groundNormal;
    Vector3 capsuleCenter;
    
    bool crouching;

    const float constHalf = 0.5f;


    void Start(){
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody>();
        capsule = GetComponent<CapsuleCollider>();

        capsuleHeight = capsule.height;
        capsuleCenter = capsule.center;

        rigidbody.constraints = RigidbodyConstraints.FreezeRotationX|RigidbodyConstraints.FreezeRotationY|RigidbodyConstraints.FreezeRotationZ;
        origGroundCheckDistance = groundCheckDistance;
    }

    public void Move(Vector3 move, bool crouch, bool jump){

        if (move.magnitude > 1f) move.Normalize();
        move = transform.InverseTransformDirection(move);

         CheckGroundStatus();

        move = Vector3.ProjectOnPlane(move, groundNormal);
        //((Camera.main).transform).position += move * Time.deltaTime;
        turnAmount = Mathf.Atan2(move.x, move.z);
        forwardAmount = move.z;

        ApplyExtraTurnRotation();

        if(isGrounded) HandleGroundedMovement(crouch, jump);
        else HandleAirborneMovement();

        scaleCrouching(crouch);
        PreventLowHeadroom();

         UpdateAnimator(move);
    }

    void scaleCrouching(bool crouch) //zmiana wielkości collisionboxa przy kucaniu
    {
        if(isGrounded && crouch){
            if(crouching) return;

            capsule.height = capsule.height/2f;
            capsule.center = capsule.center/2f;

             crouching = true;
        }
         else
        {
            Ray crouchRay = new Ray(rigidbody.position + Vector3.up * capsule.radius * constHalf, Vector3.up);
            float crouchRayLength = capsuleHeight - capsule.radius * constHalf;

            if(Physics.SphereCast(crouchRay, capsule.radius * constHalf, crouchRayLength, Physics.AllLayers, QueryTriggerInteraction.Ignore)){
                crouching = true; return;
            }

            capsule.height = capsuleHeight;
            capsule.center = capsuleCenter;

             crouching = false;
        }
    }

    void PreventLowHeadroom(){ //sprawdzanie, czy gracz podczas kucania nie ma za nisko sufitu, aby móc wstać
        if(!crouching){
            Ray crouchRay = new Ray(rigidbody.position + Vector3.up * capsule.radius * constHalf, Vector3.up);
            float crouchRayLength = capsuleHeight - capsule.radius * constHalf;

            if(Physics.SphereCast(crouchRay, capsule.radius * constHalf, crouchRayLength, Physics.AllLayers, QueryTriggerInteraction.Ignore)) crouching = true;
        }
    }

    void UpdateAnimator(Vector3 move){
        animator.SetFloat("Forward", forwardAmount, 0.1f, Time.deltaTime);
        animator.SetFloat("Turn", turnAmount, 0.1f, Time.deltaTime);
        animator.SetBool("Crouch", crouching);
        animator.SetBool("OnGround", isGrounded);

        if(!isGrounded) animator.SetFloat("Jump", (rigidbody.velocity).y);


        float runCycle = Mathf.Repeat(animator.GetCurrentAnimatorStateInfo(0).normalizedTime + runCycleLegOffset, 1);
        float jumpLeg = (runCycle < constHalf ? 1 : -1) * forwardAmount;

        if(isGrounded){
            animator.SetFloat("JumpLeg", jumpLeg);

            if(move.magnitude > 0)
             animator.speed = animSpeedMultiplier;
            else animator.speed = 1;
        }
    }

    void HandleAirborneMovement(){
        Vector3 extraGravityForce = (Physics.gravity * gravityMultiplier) - Physics.gravity;
        rigidbody.AddForce(extraGravityForce);

        groundCheckDistance = rigidbody.velocity.y < 0 ? origGroundCheckDistance : 0.03f;
    }

    void HandleGroundedMovement(bool crouch, bool jump){
        if (jump && !crouch && animator.GetCurrentAnimatorStateInfo(0).IsName("Grounded")){
            rigidbody.velocity = new Vector3(rigidbody.velocity.x, jumpPower, rigidbody.velocity.z);

            isGrounded = false;
            animator.applyRootMotion = false;
            groundCheckDistance = 0.1f;
        }
    }

    void ApplyExtraTurnRotation(){ //obracanie
        float turnSpeed = Mathf.Lerp(stationaryTurnSpeed, movingTurnSpeed, forwardAmount);

        transform.Rotate(0, turnAmount * turnSpeed * Time.deltaTime, 0);
    }

    public void OnAnimatorMove(){
        if (isGrounded && Time.deltaTime > 0){
            Vector3 v = (animator.deltaPosition * moveSpeedMultiplier) / Time.deltaTime;

            v.y = rigidbody.velocity.y;
            rigidbody.velocity = v;
        }
    }

    void CheckGroundStatus(){
        RaycastHit hitInfo;

        #if UNITY_EDITOR 
		 Debug.DrawLine(transform.position + (Vector3.up * 0.1f), transform.position + (Vector3.up * 0.1f) + (Vector3.down * groundCheckDistance));
        #endif


        if(Physics.Raycast(transform.position + (Vector3.up * 0.1f), Vector3.down, out hitInfo, groundCheckDistance)){
            groundNormal = hitInfo.normal;
            isGrounded = true;
            animator.applyRootMotion = true;
        }
        else
        {
            isGrounded = false;
            groundNormal = Vector3.up;
            animator.applyRootMotion = false;
        }
    }
}
