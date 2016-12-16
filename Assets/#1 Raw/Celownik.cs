using UnityEngine;
using System.Collections;

public class Celownik : MonoBehaviour {

	//Czy celownik widoczny.
	public bool rysujCelownik = true;
	//Kolor celownika.
	public Color crosshairColor = Color.white;
	//Grubość celownika (grubość kreski).
	public float width = 2;
	//Rozmiar celownika.
	public float rozmiarCel = 2;

	//Zmienna pozwalająca na rysowanie.
	private Texture2D tex;
	private float newHeight;
	//Obiekt pozwalający na wykonywanie dodatkowych funkcji i operacji w UI (np kolorowanie).
	private GUIStyle lineStyle;

	void Awake () {
		//Inicjalizacja obiektu do rysowania.
		tex = new Texture2D(1, 1);//Inicjalizacja obiektu.
		lineStyle = new GUIStyle();//Inicjalizacja obiektu.
		lineStyle.normal.background = tex;//przypisanie obiektu jako tło GUIStyle.
	}


	void OnGUI () {
		//Pobranie centralnego punktu monitora.
		Vector2 punktCentralny = new Vector2(Screen.width / 2, Screen.height / 2);
		float screenRatio = Screen.height / 150;		
		newHeight = rozmiarCel * screenRatio;
		
		if (rysujCelownik) {
			GUI.Box(new Rect(punktCentralny.x - (width / 2), punktCentralny.y - (newHeight), width, newHeight), GUIContent.none, lineStyle);
			GUI.Box(new Rect(punktCentralny.x - (width / 2), (punktCentralny.y), width, newHeight), GUIContent.none, lineStyle);
			GUI.Box(new Rect((punktCentralny.x), (punktCentralny.y - (width / 2)), newHeight, width), GUIContent.none, lineStyle);
			GUI.Box(new Rect(punktCentralny.x - (newHeight), (punktCentralny.y - (width / 2)), newHeight, width), GUIContent.none, lineStyle);
		}

		kolorCelownika(tex, crosshairColor);
	}

	/** Metoda ustawia kolor celownika.*/
	void kolorCelownika(Texture2D myTexture, Color myColor) {
		for (int y = 0; y < myTexture.height; y++) {
			for (int x = 0; x < myTexture.width; x++) {
				myTexture.SetPixel(x, y, myColor);
			}
			myTexture.Apply();
		}
	}

}
