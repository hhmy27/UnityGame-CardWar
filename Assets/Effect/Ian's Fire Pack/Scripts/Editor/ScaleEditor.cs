using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(Scale))]
public class ScaleEditor : Editor
{
 public override void OnInspectorGUI()
 {
        Scale scaleBeingInspected = target as Scale;
        base.OnInspectorGUI();
        if (GUILayout.Button("Update Scale"))
        {
           scaleBeingInspected.UpdateScale();
        }
 }


}

