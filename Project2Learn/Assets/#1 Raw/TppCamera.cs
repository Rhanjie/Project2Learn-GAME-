using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TppCamera : MonoBehaviour
{
    public GameObject target_ = null;
    public float radius_ = 1.7f;
    public float sensitivity_ = 3.0f;
    public Vector2 camera_location_ = new Vector3(1.0f, 1.0f);

    Vector3 angle_ = new Vector3();

    void LateUpdate()
    {
        ReadInput();
        ApplyTransform();
    }

    void ReadInput()
    {
        angle_.x += Input.GetAxis("Mouse X") * sensitivity_;
        angle_.y -= Input.GetAxis("Mouse Y") * sensitivity_;
        radius_ -= Input.GetAxis("Mouse ScrollWheel") * 4.0f;
    }

    void ApplyTransform()
    {
        Quaternion rotation = Quaternion.Euler(angle_.y, angle_.x, 0.0f);
        transform.position = rotation * new Vector3(camera_location_.x, camera_location_.y, -radius_) + target_.transform.position;
        transform.rotation = rotation;
    }
}
