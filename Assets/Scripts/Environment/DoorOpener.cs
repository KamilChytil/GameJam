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
			if (GUILayout.Button("Assign all doors in scene",GUILayout.MinHeight(30)))
			{
				doorOpener.doors = (Door[])FindObjectsOfType(typeof(Door));
				serializedObject.Update();
			}
		}
		EditorGUILayout.PropertyField(m_Property, new GUIContent("Doors"), true);
		serializedObject.ApplyModifiedProperties();
	}
}
