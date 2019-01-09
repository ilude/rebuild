using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World {

	Tile[,] tiles;

	public int Width { get; protected set; }
	public int Height { get; protected set; }

	public World(int width = 100, int height = 100)
	{
		this.Width = width;
		this.Height = height;

		tiles = new Tile[Width, Height];

		for (int x = 0; x < Width; x++)
		{
			for (int y = 0; y < Height; y++)
			{
				tiles[x, y] = new Tile(this, x, y);
			}
		}

		Debug.LogFormat("World Created with {0} tiles", Width * Height);
	}

	public Tile GetTileAt(int x, int y)
	{
		if(x > Width || x < 0 || y > Height || y < 0)
		{
			Debug.LogErrorFormat("Tile ({0},{1}) is out of range.", x, y);
			return null;
		}

		return tiles[x, y];
	}

	public void RandomizeTiles()
	{
		for (int x = 0; x < Width; x++)
		{
			for (int y = 0; y < Height; y++)
			{
				if (Random.Range(0, 2) == 0)
				{
					tiles[x, y].Type = TileType.Empty;
				}
				else
				{
					tiles[x, y].Type = TileType.Floor;
				}
			}
		}
	}
}
