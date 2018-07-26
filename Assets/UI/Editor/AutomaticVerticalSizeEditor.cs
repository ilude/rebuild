﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(AutoSize))]
public class AutomaticVerticalSizeEditor : Editor {

	public override void OnInspectorGUI() {
		DrawDefaultInspector();
		if(GUILayout.Button("Recalc Size")) {
			((AutoSize)target).AdjustSize();
		}
	}
}