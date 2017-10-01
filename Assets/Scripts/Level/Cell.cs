using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class Cell : MonoBehaviour, ICell
{

	public byte Row { get; set; }
	public byte Col { get; set; }
	
	public bool highlighted { get; private set; }
	public bool focused { get; private set; }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Highlight()
	{
		highlighted = true;
	}

	public void Unhighlight()
	{
		highlighted = false;
	}

	public void Focus()
	{
		this.focused = true;
	}

	public void Unfocus()
	{
		this.focused = false;
	}
}
