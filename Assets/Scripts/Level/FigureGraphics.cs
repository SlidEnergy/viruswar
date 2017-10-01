using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class FigureGraphics : MonoBehaviour {

	private RectTransform thisTransform;
	private Figure figure;
	private float size;

	public Sprite BlockSprite;

	public RuntimeAnimatorController RedAnimatorController;
	public RuntimeAnimatorController BlueAnimatorController;
	public RuntimeAnimatorController GreenAnimatorController;
	public RuntimeAnimatorController PinkAnimatorController;

    void Awake () {
		this.thisTransform = transform as RectTransform;
		this.figure = GetComponent<Figure>();
		this.size = thisTransform.rect.width;
	}

	// Update is called once per frame
	void Update()
	{
		if (this.figure.IsVirus)
		{
			if (this.figure.Controller.Selected || this.figure.Controller.Focused)
				thisTransform.localScale = new Vector3(1.1f, 1.1f);
			else
				thisTransform.localScale = new Vector3(1.0f, 1.0f);
		}
	}

	public void UpdatePosition()
	{
		thisTransform.anchoredPosition = new Vector3(size / 2 + this.figure.Row * size, size / 2 + this.figure.Col * size);
	}

    public void UpdateSprite()
    {
        Image image = this.thisTransform.GetComponent<Image>();
		Animator animator = this.thisTransform.GetComponent<Animator>();

		switch (this.figure.Type)
		{
			case FigureType.Player1Virus:
			case FigureType.Player2Virus:
			case FigureType.Player3Virus:
			case FigureType.Player4Virus:

				if (this.figure.Player.Color == Color.blue)
					animator.runtimeAnimatorController = this.BlueAnimatorController;
				else if(this.figure.Player.Color == Color.red)
					animator.runtimeAnimatorController = this.RedAnimatorController;
                else if (this.figure.Player.Color == Color.green)
                    animator.runtimeAnimatorController = this.GreenAnimatorController;
                else if (this.figure.Player.Color == Color.magenta)
                    animator.runtimeAnimatorController = this.PinkAnimatorController;
                else
					throw new Exception("Изменение спрайта поддерживается только для синего, красного, зеленого и розового игрока");

				break;

			case FigureType.Block:

				image.sprite = BlockSprite;
				break;

			default:

				throw new ArgumentOutOfRangeException("this.figure.Type", "Данный тип фигуры не поддерживается");
		}
	}
}
