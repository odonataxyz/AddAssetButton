using UnityEngine;
using UnityEditor;
using System.IO;

namespace Odonata {
	public static class AddAssetButton {

		[InitializeOnLoadMethod] static void Initialize() {
			EditorApplication.projectWindowItemOnGUI += OnProjectGUI;
		}

		static void OnProjectGUI(string guid, Rect selectionRect){
			string assetPath = AssetDatabase.GUIDToAssetPath(guid);

			var item = AssetButtonDictionary.Find(assetPath);
			if (item == null || item.copyFrom == null) return;

			Rect buttonRect = new Rect(selectionRect.x - (selectionRect.height + 14), selectionRect.y, selectionRect.height + 8, selectionRect.height);
			if (GUI.Button(buttonRect, "+", GUI.skin.label)) {
				var fpath = AssetDatabase.GetAssetPath(item.copyFrom);
				ProjectWindowUtil.CreateAsset(Object.Instantiate(item.copyFrom), $"{assetPath}/{Path.GetFileName(fpath)}");
			}
		}
	}
}