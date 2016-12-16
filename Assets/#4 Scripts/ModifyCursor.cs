using UnityEngine;
using System.Collections;

public class ModifyCursor : MonoBehaviour {

    public Texture2D crosshair;

     private Rect position;
     private bool drawCrosshair;


	void Start () {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        drawCrosshair = true;
        position = new Rect((Screen.width - crosshair.width) / 2, (Screen.height - crosshair.height) / 2, crosshair.width, crosshair.height);
	}
	
	void Update () {
        if (Input.GetKeyDown(KeyCode.Tab)) {
            Cursor.visible = !Cursor.visible;
            drawCrosshair = !drawCrosshair;

            if (Cursor.lockState == CursorLockMode.Locked)
             Cursor.lockState = CursorLockMode.None;
            else Cursor.lockState = CursorLockMode.Locked;
        }
	}

    void OnGUI() {
        if(drawCrosshair)
         GUI.DrawTexture(position, crosshair);
    }
}
