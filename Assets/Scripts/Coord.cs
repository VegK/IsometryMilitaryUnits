using UnityEngine;

public class Coord
{
	public int X { get; set; }
	public int Y { get; set; }

	public Coord()
	{

	}

	public Coord(int x, int y)
	{
		this.X = x;
		this.Y = y;
	}

	public Vector2 ToVector(float sizeX, float sizeY)
	{
		var pos = new Vector2();
		pos.x = (X - Y) * sizeX / 2;
		pos.y = (X + Y) * sizeY / 2;
		return pos;
	}
}