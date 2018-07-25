using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour {

	public GameObject circleCursorPrefab;

	Vector3 lastFramePosition;
	Vector3 dragStartPosition;

	List<GameObject> dragPreviewGameObjects;

	// Use this for initialization
	void Start() {
		dragPreviewGameObjects = new List<GameObject>();
	}

	// Update is called once per frame
	void Update() {
		var currFramePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		currFramePosition.z = 0;

		//circle cursor	
		//UpdateCursor(currFramePosition);
		UpdateDragging(currFramePosition);
		UpdateCameraPosition(currFramePosition);

		lastFramePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		lastFramePosition.z = 0;
	}

	private void UpdateDragging(Vector3 currFramePosition) {
		if(Input.GetMouseButtonDown(0)) {
			dragStartPosition = currFramePosition;
		}

		int start_x = Mathf.FloorToInt(dragStartPosition.x);
		int end_x   = Mathf.FloorToInt(currFramePosition.x);
		if(end_x < start_x) {
			int tmp = end_x;
			end_x = start_x;
			start_x = tmp;
		}

		int start_y = Mathf.FloorToInt(dragStartPosition.y);
		int end_y   = Mathf.FloorToInt(currFramePosition.y);
		if(end_y < start_y) {
			int tmp = end_y;
			end_y = start_y;
			start_y = tmp;
		}

		while(dragPreviewGameObjects.Count > 0) {
			var go = dragPreviewGameObjects[0];
			dragPreviewGameObjects.RemoveAt(0);
			SimplePool.Despawn(go);
		}

		if(Input.GetMouseButton(0)) {
			for(int x = start_x; x <= end_x; x++) {
				for(int y = start_y; y <= end_y; y++) {
					Tile tile = WorldController.Instance.World.GetTileAt(x,y);
					if(tile != null) {
						GameObject go = SimplePool.Spawn(circleCursorPrefab, new Vector3(x, y, 0), Quaternion.identity);
						go.transform.SetParent(this.transform);
						dragPreviewGameObjects.Add(go);

					}
				}
			}
		}

		if(Input.GetMouseButtonUp(0)) {
			for(int x = start_x; x <= end_x; x++) {
				for(int y = start_y; y <= end_y; y++) {
					Tile tile = WorldController.Instance.World.GetTileAt(x,y);
					if(tile != null) {
						tile.Type = Tile.TileType.Floor;
					}
				}
			}
		}
	}

	private void UpdateCameraPosition(Vector3 currFramePosition) {
		// screen dragging
		if(Input.GetMouseButton(1) || Input.GetMouseButton(2)) {
			Vector3 diff = lastFramePosition - currFramePosition;
			Camera.main.transform.Translate(diff);
		}

		Camera.main.orthographicSize -= Camera.main.orthographicSize * Input.GetAxis("Mouse ScrollWheel") * 2f;

		Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, 3f, 21f);
	}
}
