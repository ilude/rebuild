using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour {

	Vector3 lastFramePosition;
	public GameObject circleCursor;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		var currFramePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		currFramePosition.z = 0;

		//circle cursor	
		Tile tileUnderMouse = GetTileAtWorldCoord(currFramePosition);
		if(tileUnderMouse != null)
		{
			Vector3 cursorPosition = new Vector3(tileUnderMouse.X, tileUnderMouse.Y, 0);
			circleCursor.transform.position = cursorPosition;
			circleCursor.SetActive(true);
		}
		else
		{
			circleCursor.SetActive(false);
		}

		if (Input.GetMouseButtonUp(0))
		{
			if(tileUnderMouse != null)
			{
				if(tileUnderMouse.Type == Tile.TileType.Empty)
				{
					tileUnderMouse.Type = Tile.TileType.Floor;
				}
				else
				{
					tileUnderMouse.Type = Tile.TileType.Empty;
				}
			}
		}


		// screen dragging
		if (Input.GetMouseButton(1) || Input.GetMouseButton(2))
		{
			Vector3 diff = lastFramePosition - currFramePosition;
			Camera.main.transform.Translate(diff);
		}
		lastFramePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		lastFramePosition.z = 0;
	}

	Tile GetTileAtWorldCoord(Vector3 coord)
	{
		int x = Mathf.FloorToInt(coord.x);
		int y = Mathf.FloorToInt(coord.y);

		return WorldController.Instance.World.GetTileAt(x, y);
	}
}
