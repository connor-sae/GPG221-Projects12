using UnityEngine;
using UnityEditor;

namespace GPG221.AI.Editors
{
public class NavEditorTools : EditorWindow {
    
    //[MenuItem("Navigation")]
    static void Init()
    {
        EditorWindow window = GetWindow(typeof(EditorGUILayout));
        window.Show();
    }

    // int index = 0;
    // string[] options =
    // {
    //     "Hello"
    // };

    // void OnGUI()
    // {
    //     index = EditorGUILayout.Popup(index, options);
    //     if (GUILayout.Button("Say Hello"))
    //         Debug.Log("Hello World");
    // }
}
}