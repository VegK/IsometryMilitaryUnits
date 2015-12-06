using UnityEngine;

public static class VectorExtension
{
	public static Coord ToCoord(this Vector2 vector, float sizeX, float sizeY)
	{
		var x = vector.x / sizeX + vector.y / sizeY;
		var y = vector.y / sizeY - vector.x / sizeX;

		return new Coord(Mathf.RoundToInt(x), Mathf.RoundToInt(y));
	}

	public static Coord ToCoord(this Vector3 vector, float sizeX, float sizeY)
	{
		var vector2 = new Vector2(vector.x, vector.y);

		return vector2.ToCoord(sizeX, sizeY);
	}
}