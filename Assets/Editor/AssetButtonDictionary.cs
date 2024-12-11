using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

namespace Odonata {
	[System.Serializable] public class AddAssetKeyValuePair {
		public string dirname;
		public Object copyFrom;
	}

	public class AssetButtonDictionary : ScriptableObject {
		[SerializeField] List<AddAssetKeyValuePair> m_Assets = new();
		static AssetButtonDictionary m_Instance;
		
		public static AddAssetKeyValuePair Find(string dirpath) {
			if (m_Instance == null) {
				string[] dictGUIDs = AssetDatabase.FindAssets(string.Format($"t:{nameof(AssetButtonDictionary)}"));
				if (dictGUIDs.Length == 0) {
					m_Instance = new AssetButtonDictionary();
					AssetDatabase.CreateAsset(m_Instance, "Assets/AssetButtons.asset");
				} else {
					m_Instance = AssetDatabase.LoadAssetAtPath<AssetButtonDictionary>(AssetDatabase.GUIDToAssetPath(dictGUIDs[0]));
				}
			}
			return m_Instance._Find(dirpath);
		}

		AddAssetKeyValuePair _Find(string dirpath) {
			string name = Path.GetFileNameWithoutExtension(dirpath);
			foreach (var item in m_Assets) {
				if (item.dirname == name) return item;
			}
			return null;
		}
	}
}