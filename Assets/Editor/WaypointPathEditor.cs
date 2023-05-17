using UnityEngine;
using UnityEditor;
using static WaypointPath;

[CustomEditor(typeof(WaypointPath))]
public class WaypointPathEditor : Editor
{


    public override void OnInspectorGUI()
    {
        GUIStyle style = new GUIStyle();
        style.richText = true;
        GUILayout.Label("<size=20><color=red>READ ME FIRST</color></size>", style);

        DrawDefaultInspector();

        WaypointPath myScript = (WaypointPath)target;
        //EditorGUILayout.LabelField("Comment:", myScript.comment);
        EditorGUILayout.HelpBox("Click the 'Add Waypoint' button or press 'W' to add a new waypoint at the current mouse position.", MessageType.Info);
        if (GUILayout.Button("Add Waypoint"))
        {
            AddWaypoint(myScript);
        }

        EditorGUILayout.HelpBox("Click the 'Remove Waypoint' button or press 'Q' to delete a last waypoint.", MessageType.Info);
        if (GUILayout.Button("Remove Waypoint"))
        {
            RemoveWaypoint(myScript);
        }
        GUILayout.Label("<size=20>Help with <color=red>ERRORS </color></size>", style);
        GUILayout.Label("<size=12>If you have error like this 'The object of type 'Transform' has</size>", style);
        GUILayout.Label("<size=12>been destroyed but you are still trying to access it'.</size>", style);
        GUILayout.Label("<size=12>Simple DELETE waypoints in list and gameobject in scene</size>", style);
        GUILayout.Label("<size=12>on your Scene list. </size>", style);
        GUILayout.Label("<size=12>This can happen when you use ctrl + z instead of q </size>", style);
    }


    public void AddWaypoint(WaypointPath way)
    {
        if (!EditorApplication.isPlaying)
        {

            Vector3 position = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition).origin;
            GameObject waypointObject = new GameObject("Waypoint " + (way.waypoints.Length)); //
            if (way.pathMode == PathMode.Mode2D) position.z = 0;   //ustawia pozycjê z na 0 dla trybu 2D
            waypointObject.transform.position = position;

            Transform[] newWaypoints = new Transform[way.waypoints.Length + 1];
            for (int i = 0; i < way.waypoints.Length; i++)
            {
                newWaypoints[i] = way.waypoints[i];
            }

            newWaypoints[way.waypoints.Length] = waypointObject.transform;

            way.waypoints = newWaypoints;

        }
    }

    public void RemoveWaypoint(WaypointPath way)
    {
        if (!EditorApplication.isPlaying)
        {
            if (way.waypoints.Length > 0)
            {
                Transform[] newWaypoints = new Transform[way.waypoints.Length - 1];
                for (int i = 0; i < newWaypoints.Length; i++)
                {
                    newWaypoints[i] = way.waypoints[i];
                }

                DestroyImmediate(way.waypoints[way.waypoints.Length - 1].gameObject);
                way.waypoints = newWaypoints;
            }
        }
    }

    private void OnSceneGUI()
    {
        WaypointPath myScript = (WaypointPath)target;

        Event e = Event.current;



        Handles.color = Color.white;
        for (int i = 0; i < myScript.waypoints.Length; i++)
        {
            Vector3 position = myScript.waypoints[i].position;
            EditorGUI.BeginChangeCheck();
            position = Handles.PositionHandle(position, Quaternion.identity);
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(myScript.waypoints[i], "Move Waypoint");
                myScript.waypoints[i].position = position;
            }
            GUIStyle style = new GUIStyle();
            style.normal.textColor = Color.yellow;
            style.alignment = TextAnchor.MiddleCenter;
            Handles.Label(position, "Waypoint " + i, style);
        }

        if (!EditorApplication.isPlaying)
        {
            if (e.type == EventType.KeyDown && e.keyCode == KeyCode.E)
            {
                AddWaypoint(myScript);
                e.Use();
            }

            if (e.type == EventType.KeyDown && e.keyCode == KeyCode.Q)
            {
                RemoveWaypoint(myScript);
                e.Use();
            }
        }

        if (Input.GetButtonDown("AddWaypoint"))
        {
            AddWaypoint(myScript);
        }
        else if (Input.GetButtonDown("RemoveWaypoint"))
        {
            RemoveWaypoint(myScript);
        }
    }


}
//MADE BY CZARNY_ No thanks to me :0