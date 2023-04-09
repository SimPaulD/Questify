using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(FirebaseManagerAuth))]
public class GUIEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // Get the Header attribute of the target script
        HeaderAttribute header = (HeaderAttribute)System.Attribute.GetCustomAttribute(target.GetType(), typeof(HeaderAttribute));

        // If the Header attribute exists, draw it with bold font style
        if (header != null)
        {
            GUIStyle boldStyle = new GUIStyle(GUI.skin.label);
            boldStyle.fontStyle = FontStyle.Bold;
            boldStyle.fontSize = 20;
            GUILayout.Label(header.header, boldStyle);
        }

        // Draw the default inspector
        DrawDefaultInspector();
    }
}