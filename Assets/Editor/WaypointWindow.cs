using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;

public class WaypointWindow : EditorWindow
{
    [MenuItem("Tools/Waypoint Editor")]
    public static void Open()
    {
        GetWindow<WaypointWindow>();
    }

    public Transform waypointRoots;

    private void OnGUI()
    {

        SerializedObject obj = new SerializedObject(this);

        EditorGUILayout.PropertyField(obj.FindProperty("waypointRoots"));

        if (waypointRoots == null)
        {
            EditorGUILayout.HelpBox("Root transform must be selected. Please assign a root transform", MessageType.Warning);
        }
        else
        {
            EditorGUILayout.BeginVertical("box");
            DrawButton();
            EditorGUILayout.EndVertical();
        }

        obj.ApplyModifiedProperties();
    }

    void DrawButton()
    {
        if (GUILayout.Button("Create Waypoint"))
        {
            CreateWaypoint();
        }
        if (Selection.activeGameObject != null && Selection.activeGameObject.GetComponent<Waypoint>())
        {
            if (GUILayout.Button("Create Waypoint Branch"))
            {
                CreateBranch();
            }
            if (GUILayout.Button("Create Waypoint Before"))
            {
                CreateWaypointBefore();
            }
            if (GUILayout.Button("Create Waypoint After"))
            {
                CreateWaypointAfter();
            }
            if (GUILayout.Button("Remove Waypoint"))
            {
                RemoveWaypoint();
            }
        }
    }

    private void CreateBranch()
    {
        GameObject waypointObj = new GameObject("Waypoint" + waypointRoots.childCount, typeof(Waypoint));
        waypointObj.transform.SetParent(waypointRoots, false);

        Waypoint waypoint = waypointObj.GetComponent<Waypoint>();
        Waypoint branchedFrom = Selection.activeGameObject.GetComponent<Waypoint>();
        branchedFrom.branches.Add(waypoint);

        waypoint.transform.position = branchedFrom.transform.position;
        waypoint.transform.forward = branchedFrom.transform.forward;

        Selection.activeGameObject = waypointObj.gameObject;

    }

    private void CreateWaypoint()
    {
        GameObject waypointObj = new GameObject("Waypoint" + waypointRoots.childCount, typeof(Waypoint));
        waypointObj.transform.SetParent(waypointRoots, false);

        Waypoint waypoint = waypointObj.GetComponent<Waypoint>();
        if (waypointRoots.childCount > 1)
        {
            waypoint.PreviousWaypoint = waypointRoots.GetChild(waypointRoots.childCount - 2).GetComponent<Waypoint>();
            waypoint.PreviousWaypoint.NextWaypoint = waypoint;
            //place the waypoint at the last position
            waypoint.transform.position = waypoint.PreviousWaypoint.transform.position;
            waypoint.transform.forward = waypoint.PreviousWaypoint.transform.forward;

        }

        Selection.activeGameObject = waypoint.gameObject;

    }


    private void CreateWaypointBefore()
    {
        GameObject waypointObj = new GameObject("Waypoint " + waypointRoots.childCount, typeof(Waypoint));
        waypointObj.transform.SetParent(waypointRoots, false);

        Waypoint newWaypoint = waypointObj.GetComponent<Waypoint>();
        Waypoint selectedWaypoint = Selection.activeGameObject.GetComponent<Waypoint>();

        waypointObj.transform.position = selectedWaypoint.transform.position;
        waypointObj.transform.forward = selectedWaypoint.transform.forward;

        if(selectedWaypoint.PreviousWaypoint != null)
        {
            newWaypoint.PreviousWaypoint = selectedWaypoint.PreviousWaypoint;
            selectedWaypoint.PreviousWaypoint.NextWaypoint = newWaypoint;
        }

        newWaypoint.NextWaypoint = selectedWaypoint;
        selectedWaypoint.PreviousWaypoint = newWaypoint;
        newWaypoint.transform.SetSiblingIndex(selectedWaypoint.transform.GetSiblingIndex());
        Selection.activeGameObject = newWaypoint.gameObject;

    }

    private void CreateWaypointAfter()
    {
        GameObject waypointObj = new GameObject("Waypoint " + waypointRoots.childCount, typeof(Waypoint));
        waypointObj.transform.SetParent(waypointRoots, false);

        Waypoint newWaypoint = waypointObj.GetComponent<Waypoint>();
        Waypoint selectedWaypoint = Selection.activeGameObject.GetComponent<Waypoint>();

        waypointObj.transform.position = selectedWaypoint.transform.position;
        waypointObj.transform.forward = selectedWaypoint.transform.forward;

        newWaypoint.PreviousWaypoint = selectedWaypoint;

        if(selectedWaypoint.NextWaypoint != null)
        {
           selectedWaypoint.NextWaypoint.PreviousWaypoint= newWaypoint;
            newWaypoint.NextWaypoint = selectedWaypoint.NextWaypoint;
        }

        selectedWaypoint.NextWaypoint = newWaypoint;
        newWaypoint.transform.SetSiblingIndex(selectedWaypoint.transform.GetSiblingIndex());

        Selection.activeGameObject = newWaypoint.gameObject;

    }

    private void RemoveWaypoint()
    {
        Waypoint selectedWaypoint = Selection.activeGameObject.GetComponent<Waypoint>();

        if(selectedWaypoint.NextWaypoint != null)
        {
            selectedWaypoint.NextWaypoint.PreviousWaypoint = selectedWaypoint.PreviousWaypoint;
        }
        if(selectedWaypoint.PreviousWaypoint != null)
        {
            selectedWaypoint.PreviousWaypoint.NextWaypoint = selectedWaypoint.NextWaypoint;
            Selection.activeGameObject = selectedWaypoint.PreviousWaypoint.gameObject;
        }

        DestroyImmediate(selectedWaypoint.gameObject);
    }
}
