using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class VirusPool : MonoBehaviour {

	public RectTransform figurePrefab;

	private Transform board;

	private List<Figure> pool = new List<Figure>();

	void Awake()
	{
		this.board = this.transform;
	}

	public void SetFigureSize(byte figureSize)
	{
		figurePrefab.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, figureSize);
		figurePrefab.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, figureSize);
	}

	public Figure CreateVirus()
	{
		RectTransform figureTransform = Instantiate(figurePrefab, new Vector3(-100, -100), Quaternion.identity) as RectTransform;

		//virusTransform.SetParent(cells[i].GetComponent<RectTransform>(), false);

		figureTransform.SetParent(board, false);

		return figureTransform.GetComponent<Figure>();
	}

	public void Put(Figure figure)
	{
		pool.Add(figure);
	}

	public Figure Get()
	{
		if (pool.Count > 0)
			return pool[0];
		else
			return CreateVirus();
	}
}
