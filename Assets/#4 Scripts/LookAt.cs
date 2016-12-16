using UnityEngine;
using System.Collections;

public class LookAt : MonoBehaviour{

     public GameObject target = null;
     public Vector2 cameraPosition = new Vector2(1f, 1f);
     public int speedX = 10, speedY = 5;

     private float distance = 4.0f;
     private Vector3 radius, targetCenter;

    void Update(){
        radius.x += Input.GetAxis("Mouse X") * speedX;
        radius.y -= Input.GetAxis("Mouse Y") * speedY;
        distance -= Input.GetAxis("Mouse ScrollWheel");

       //Ustawienie centrum obiektu w środku XZ i na samej wysokości Y (głowa).
        targetCenter = (target.GetComponent<Renderer>().bounds).center;
        targetCenter.y = (target.GetComponent<Renderer>().bounds).max.y;

       //Ograniczenie wartości.
        distance = Mathf.Clamp(distance, 2.5f, 5.0f);
        radius.y = Mathf.Clamp(radius.y, -25.0f, 40.0f);
    }

    void LateUpdate(){
        Vector3 direction = new Vector3(cameraPosition.x, cameraPosition.y, -distance);
        Quaternion rotation = Quaternion.Euler(radius.y, radius.x, 0.0f);

        transform.position = rotation * direction + target.transform.position;
        transform.rotation = rotation;
    }
}
