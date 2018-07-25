using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldController : MonoBehaviour {
	public Sprite floorSprite;

	public World World { get; protected set; }

	Dictionary<Tile, GameObject> tileGameObjectMap = new Dictionary<Tile, GameObject>();


	public static WorldController Instance { get; protected set;}

	// Use this for initialization
	void Start () {
		if(Instance != null)
			Debug.LogError("There should only ever be one WorldController.");

		Instance = this;

		World = new World();
		

		// create gameobjects for each tile
		for (int x = 0; x < World.Width; x++)
		{
			for (int y = 0; y < World.Height; y++)
			{
				Tile tile_data = World.GetTileAt(x, y);

				GameObject tile_go = new GameObject();
				tile_go.name = string.Format("Tile_{0}_{1}", x, y);
				tile_go.transform.position = new Vector3(tile_data.X, tile_data.Y, 0);
				tile_go.transform.SetParent(this.transform, true);

				tile_go.AddComponent<SpriteRenderer>();
				tileGameObjectMap[tile_data] = tile_go;
				tile_data.RegisterTileTypeChanged((tile)=> { OnTileChanged(tile, tile_go); });
			}
		}

		World.RandomizeTiles();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTileChanged(Tile tile_data, GameObject tile_go)
	{
		if(tile_data.Type == Tile.TileType.Floor)
		{
			tile_go.GetComponent<SpriteRenderer>().sprite = floorSprite;
		}
		else if(tile_data.Type == Tile.TileType.Empty)
		{
			tile_go.GetComponent<SpriteRenderer>().sprite = null;
		}
		else
		{
			Debug.Log("OnTileChanged - Unrecognized tile type.");
		}
	}

	public Tile GetTileAtWorldCoord(Vector3 coord) {
		int x = Mathf.FloorToInt(coord.x);
		int y = Mathf.FloorToInt(coord.y);

		return WorldController.Instance.World.GetTileAt(x,y);
	}
}
