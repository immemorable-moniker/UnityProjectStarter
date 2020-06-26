using UnityEditor;
using UnityEditor.ShortcutManagement;
using UnityEngine;

public static class ShortcutCreateGround
{
    [Shortcut("Create Ground", KeyCode.G, ShortcutModifiers.Shift)]
    public static void CreateGround()
    {
        GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);
        go.transform.position = new Vector3(0, -0.5f, 0);
        go.transform.localScale = new Vector3(50, 1, 50);
        go.name = "Ground";
        go.GetComponent<Renderer>().material = AssetDatabase.LoadAssetAtPath<Material>("Assets/Starter/Materials/Green.mat");
    }
}
