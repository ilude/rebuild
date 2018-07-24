using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldController : MonoBehaviour {
	public Sprite floorSprite;

	World world;

	// Use this for initialization
	void Start () {
		world = new World();
		world.RandomizeTiles();

		// create gameobjects for each tile
		for (int x = 0; x < world.Width; x++)
		{
			for (int y = 0; y < world.Height; y++)
			{
				Tile tile_data = world.GetTileAt(x, y);

				GameObject tile_go = new GameObject();
				tile_go.name = string.Format("Tile_{0}_{1}", x, y);
				tile_go.transform.position = new Vector3(tile_data.X, tile_data.Y, 0);

				var sr = tile_go.AddComponent<SpriteRenderer>();
				
				if(tile_data.Type == Tile.TileType.Floor)
				{
					sr.sprite = floorSprite;
				}

			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
