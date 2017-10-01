using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class MainMenuGenerator : MonoBehaviour {

	public RectTransform Button;

	private Button[] buttons;

	private Transform thisTransform;

	void Awake()
	{
		thisTransform = GetComponent<Transform>();

		List<LevelInfo> levels = GameManager.Instance.GetLevels();

		DrawButtons(levels);

		UpdateButtonState();
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private void DrawButtons(List<LevelInfo> levels)
	{
		this.buttons = new Button[levels.Count];

		for (int i = 0; i < levels.Count; i++)
		{
			int level = (i + 1);

			RectTransform buttonTransform = null;

			if (i < 7)
				buttonTransform = Instantiate(Button, new Vector3(-450 + i * 150, -90), Quaternion.identity) as RectTransform;
			else
				buttonTransform = Instantiate(Button, new Vector3(-450 + (i - 7) * 150, -210), Quaternion.identity) as RectTransform;

			buttonTransform.SetParent(thisTransform, false);

			buttonTransform.name = "level" + level.ToString() + "Button";

			Button button = buttonTransform.GetComponent<Button>();

			button.onClick.AddListener(delegate { GameManager.Instance.LoadLevel(level); });
			Text text = button.GetComponentInChildren(typeof(Text)) as Text;
			text.text = level.ToString();

			LevelButtonGraphics graphics = button.GetComponent<LevelButtonGraphics>();
			graphics.Level = level;

			this.buttons[i] = button;
		}
	}

	private void UpdateButtonState()
	{
		List<LevelInfo> levels = GameManager.Instance.GetLevels();

		for (int i = 0; i < this.buttons.Length; i++)
			this.buttons[i].enabled = ! levels[i].Block;
	}
}
