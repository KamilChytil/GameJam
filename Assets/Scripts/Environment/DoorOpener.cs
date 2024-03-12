using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

[RequireComponent(typeof(InteractionZone))]
public class DoorOpener : MonoBehaviour
{
	public Door[] doors;
	// Start is called before the first frame update
	void Start()
	{
	}

	// Update is called once per frame
	void Update()
	{

	}

	public void ButtonPush()
	{
		foreach (var door in doors)
		{
			door.Open();
		}
	}
}

#if UNITY_EDITOR
[CustomEditor(typeof(DoorOpener))]
class DoorOpenerEditor : Editor
{
	private SerializedProperty m_Property;

	private void OnEnable()
	{
		m_Property = serializedObject.FindProperty("doors");
	}

	public override void OnInspectorGUI()
	{
		DoorOpener doorOpener = (DoorOpener)target;
		if (PrefabUtility.IsPartOfPrefabInstance(doorOpener))
		{
			if (GUILayout.Button("Assign all doors in scene", GUILayout.MinHeight(30)))
			{
				Door[] doors = (Door[])FindObjectsOfType(typeof(Door));
				m_Property.ClearArray();
				for (int x = 0; x < doors.Length; x++)
				{
					m_Property.InsertArrayElementAtIndex(x);
					SerializedProperty property = m_Property.GetArrayElementAtIndex(x);
					property.objectReferenceValue = doors[x];
				}
				//serializedObject.Update();
			}
		}
		EditorGUILayout.PropertyField(m_Property, new GUIContent("Doors"), true);
		serializedObject.ApplyModifiedProperties();
	}
}
#endif