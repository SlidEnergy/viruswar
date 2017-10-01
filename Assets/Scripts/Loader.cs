using UnityEngine;
using System.Collections;

public class Loader : MonoBehaviour {

	public GameObject GameManagerPrefab;
	//public GameObject soundManager;  

	public GameObject MenuPrefab;

	void Awake()
	{
		//if (GameManager.Instance == null)
		GameObject gameManager = Instantiate(GameManagerPrefab) as GameObject;
		gameManager.name = "GameManager";

		GameObject menu = Instantiate(MenuPrefab) as GameObject;
		menu.name = "Menu";
		menu.transform.SetParent(this.transform);
		//((RectTransform)menu.transform).localPosition = new Vector3(0, 0);
		//((RectTransform)menu.transform).position = new Vector3(0, 0);
		//((RectTransform)menu.transform).rect.Set(0, 0, 0, 0);
		//((RectTransform)menu.transform).Translate(new Vector3(0, 0));
		//((RectTransform)menu.transform).anchorMin = new Vector2(0f, 0f);
		//((RectTransform)menu.transform).anchorMax = new Vector2(1f, 1f);
		((RectTransform)menu.transform).offsetMin = new Vector2(0f, 0f);
		((RectTransform)menu.transform).offsetMax = new Vector2(0f, 0f);

		//if (SoundManager.instance == null)
		//Instantiate(soundManager);
	}
}
