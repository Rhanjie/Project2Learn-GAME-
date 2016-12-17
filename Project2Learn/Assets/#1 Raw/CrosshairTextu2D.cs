using UnityEngine;
using System.Collections;

/**
 * Klasa odpowiedzialna za narysowanie/wyświetlenie celownika przy użyciu jego tekstury.
 */
public class CrosshairTextu2D : MonoBehaviour {

	/* Pod tą zmienną podstawiamy obrazek*/
	public Texture2D crosshairTexture; 
	/*Pozycja naszego celownika.*/
	public Rect position; 
	/** Czy wyświetlić celownik.*/
	public bool widoczny = true;
	
	void Start(){
		//Ustawienie pozycji dla celownika.
		position = new Rect(
			(Screen.width - crosshairTexture.width) / 2, 
			(Screen.height - crosshairTexture.height) /2, 
			crosshairTexture.width, crosshairTexture.height);
	}

	/**
	 * Metoda pozwala rejestrować zdarzenia interfejsu użytkownika.	 * 
	 */
	void OnGUI(){
		//Czy pokazać celownik.
		if (widoczny == true) {
			//Rysowanie celownika.
			GUI.DrawTexture (position, crosshairTexture); 
		}
	}
}
