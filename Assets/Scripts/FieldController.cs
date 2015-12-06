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
	public int UnitsCount = 100;
	#endregion
	#region Private
	private bool[,] _field;
	private Coord _firstClick;
	private Coord _lastClick;
	private HashSet<CellController> _squad = new HashSet<CellController>();
	#endregion
	#endregion

	#region Methods
	#region Public

	#endregion
	#region Private
	private void Start()
	{
		_field = new bool[Width, Height];

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
		if (Input.GetMouseButtonDown(0))
			_firstClick = Camera.main.ScreenToWorldPoint(Input.mousePosition).ToCoord(SizeX, SizeY);
		if (Input.GetMouseButton(0))
		{
			if (_squad.Count >= UnitsCount)
				return;

			var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			var cell = CreateCell(mousePosition);
			if (cell != null)
				_squad.Add(cell);
		}
		else if (Input.GetMouseButtonUp(0))
		{
			_lastClick = Camera.main.ScreenToWorldPoint(Input.mousePosition).ToCoord(SizeX, SizeY);
			UpbuildSquad();
		}
	}

	private CellController CreateCell(Vector2 position)
	{
		var coord = position.ToCoord(SizeX, SizeY);

		return CreateCell(coord.X, coord.Y);
	}

	private CellController CreateCell(int x, int y)
	{
		if (_field[x - 1, y - 1])
			return null;

		var cell = Instantiate(PrefabCell);
		cell.name = x + ":" + y;
		cell.transform.SetParent(transform);

		var coord = new Coord(x, y);
		cell.transform.localPosition = coord.ToVector(SizeX, SizeY);

		cell.X = x;
		cell.Y = y;

		_field[x - 1, y - 1] = true;
		return cell;
	}

	private void UpbuildSquad()
	{
		var missingUnits = UnitsCount - _squad.Count;
		var upbuildX = Mathf.Abs(_firstClick.X - _lastClick.X) < Mathf.Abs(_firstClick.Y - _lastClick.Y);
		var line = 0;

		while (missingUnits > 0)
		{
			foreach (CellController unit in _squad)
			{
				if (missingUnits == 0)
					break;

				var x = unit.X;
				var y = unit.Y;
				if (upbuildX)
					x += line + 1;
				else
					y += line + 1;

				var cell = CreateCell(x, y);
				if (cell != null)
				{
					missingUnits--;
					if (missingUnits == 0)
						break;
				}


				x = unit.X;
				y = unit.Y;
				if (upbuildX)
					x -= line + 1;
				else
					y -= line + 1;

				cell = CreateCell(x, y);
				if (cell != null)
					missingUnits--;

			}
			line++;
		}

		_squad.Clear();
	}
	#endregion
	#endregion
}