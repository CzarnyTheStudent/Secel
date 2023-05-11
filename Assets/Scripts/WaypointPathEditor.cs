using UnityEngine;
using UnityEditor;

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
            myScript.AddWaypoint();
        }

        EditorGUILayout.HelpBox("Click the 'Remove Waypoint' button or press 'Q' to delete a last waypoint.", MessageType.Info);
        if (GUILayout.Button("Remove Waypoint"))
        {
            myScript.RemoveWaypoint();
        }
        GUILayout.Label("<size=20>Help with <color=red>ERRORS </color></size>", style);
        GUILayout.Label("<size=12>If you have error like this 'The object of type 'Transform' has</size>", style);
        GUILayout.Label("<size=12>been destroyed but you are still trying to access it'.</size>", style);
        GUILayout.Label("<size=12>Simple DELETE waypoints in list and gameobject in scene</size>", style);
        GUILayout.Label("<size=12>on your Scene list. </size>", style);
        GUILayout.Label("<size=12>This can happen when you use ctrl + z instead of q </size>", style);
    }

    private void OnSceneGUI()
    {
        WaypointPath myScript = (WaypointPath)target;

        Event e = Event.current;

        if (!EditorApplication.isPlaying)
        {
            if (e.type == EventType.KeyDown && e.keyCode == KeyCode.E)
            {
                myScript.AddWaypoint();
                e.Use();
            }

            if (e.type == EventType.KeyDown && e.keyCode == KeyCode.Q)
            {
                myScript.RemoveWaypoint();
                e.Use();
            }
        }
    }
}
//MADE BY CZARNY_ No thanks to me :0