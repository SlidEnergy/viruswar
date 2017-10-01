using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CellGraphics : MonoBehaviour
{
	private Cell cell;

	private Image thisImage;

	void Awake()
	{
		thisImage = GetComponent<Image>();
		this.cell = GetComponent<Cell>();
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (this.cell.highlighted)
			this.thisImage.color = new Color(0.5f, 0.5f, 0.5f, 0.5f); // Color.gray
		else
			thisImage.color = new Color(1, 1, 1, 0.5f);// Color.white;

		if (this.cell.focused)
			this.transform.localScale = new Vector3(1.1f, 1.1f);
		else
			this.transform.localScale = new Vector3(1.0f, 1.0f);
	}
}
