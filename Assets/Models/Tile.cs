using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile {
	public enum TileType { Empty, Floor }

	LooseObject looseObject;
	InstalledObject installedObject;

	World world;
	public int X { get; protected set; }
	public int Y { get; protected set; }

	public TileType Type { get; set; }

	public Tile(World world, int x, int y)
	{
		this.Type = TileType.Empty;
		this.world = world;
		this.X = x;
		this.Y = y;
	}
}
