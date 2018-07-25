using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile
{
	public enum TileType { Empty, Floor }

	TileType type = TileType.Empty;

	LooseObject looseObject;
	InstalledObject installedObject;

	World world;
	Action<Tile> cbTileChanged; 

	public int X { get; protected set; }
	public int Y { get; protected set; }

	public TileType Type
	{
		get
		{
			return type;
		}
		set
		{
			TileType oldType = type;
			type = value;
			if(cbTileChanged != null && oldType != type)
				cbTileChanged(this);
		}
	}

	public Tile(World world, int x, int y)
	{
		this.world = world;
		this.X = x;
		this.Y = y;
	}

	public void RegisterTileTypeChanged(Action<Tile> callback)
	{
		this.cbTileChanged += callback;
	}

	public void UnregisterTileTypeChanged(Action<Tile> callback)
	{
		this.cbTileChanged -= callback;
	}
}
