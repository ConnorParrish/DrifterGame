using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using LitJson;
using System.IO;
using System;

public class ReadOnlyAttribute : PropertyAttribute { }

[CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
public class ReadOnlyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        GUI.enabled = false;
        EditorGUI.PropertyField(position, property, label, true);
        GUI.enabled = true;
    }
}
/*
public class ItemWizard : ScriptableWizard {

    [ReadOnly] public int id;
    public string title;
    public string description;
    [ReadOnly] public string slug;
    public string type;
    public float strength;
    public bool stackable;
    public float value;

    public Texture2D ItemSprite;

    [MenuItem("DrifterTools/Don't Use Me")]
    static void CreateWizard()
    {
        ScriptableWizard.DisplayWizard<ItemWizard>("Really though...","This messes things up");
    }

    private void OnWizardCreate()
    {
        Item item = new Item(id, title, description, slug, type, strength, stackable, value);
        JsonData oldData;
        JsonData itemJson;
        JsonWriter writer = new JsonWriter();
        writer.PrettyPrint = true;
        writer.IndentValue = 4;

        oldData = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/StreamingAssets/ItemList.json"));
        JsonMapper.ToJson(item,writer);
        File.WriteAllText(Application.dataPath + "/StreamingAssets/ItemList.json", File.ReadAllText(Application.dataPath + "/StreamingAssets/ItemList.json").TrimEnd(',',']') + "," + writer.ToString() + Environment.NewLine + "]");

        //ItemSprite.name = slug;

    }

    private void OnWizardUpdate()
    {
        id = JsonMapper.ToObject(File.ReadAllText(Application.dataPath +
            "/StreamingAssets/ItemList.json")).Count;

        if (title != null)
        {
            slug = title.ToLower();
            slug = slug.Replace(" ", "_");
        }
    }

}
*/