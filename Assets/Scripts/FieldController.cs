using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldController : MonoBehaviour
{
	private const float SizeX = 1.25f;
	private const float SizeY = 0.8f;

	#region Properties
	#region Public
	public CellController PrefabCell;
	[Header("Parameters")]
	public int Width = 100;
	public int Height = 100;
	#endregion
	#region Private

	#endregion
	#endregion

	#region Methods
	#region Public

	#endregion
	#region Private
	private void Start()
	{
		for (int x = 1; x <= Width; x++)
		{
			CreateCell(x, 1);
			CreateCell(x, Height);
		}

		for (int y = 1; y <= Height; y++)
		{
			CreateCell(1, y);
			CreateCell(Width, y);
		}
	}

	private void Update()
	{
		if (Input.GetMouseButton(0))
		{
			var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			CreateCell(mousePosition);
		}
	}

	private CellController CreateCell(Vector2 position)
	{
		var x = position.x / SizeX + position.y / SizeY;
		var y = position.y / SizeY - position.x / SizeX;

		return CreateCell(Mathf.RoundToInt(x), Mathf.RoundToInt(y));
	}

	private CellController CreateCell(int x, int y)
	{
		var cell = Instantiate(PrefabCell);
		cell.name = x + ":" + y;
		cell.transform.SetParent(transform);

		var pos = new Vector2();
		pos.x = (x - y) * SizeX / 2;
		pos.y = (x + y) * SizeY / 2;
		cell.transform.localPosition = pos;

		return cell;
	}
	#endregion
	#endregion
}