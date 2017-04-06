using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(NPCBuilderScript))]
public class NPCBuilderEditor : Editor {
    public Material mat;
    public GameObject go;
    public override void OnInspectorGUI()
    {
        //DrawDefaultInspector();

        NPCBuilderScript builderScript = (NPCBuilderScript)target;

        builderScript.SpawnNPC = (NPCBuilderScript.NPCs)EditorGUILayout.EnumPopup("NPC Type", builderScript.SpawnNPC);
        if (builderScript.SpawnNPC != NPCBuilderScript.NPCs.Select)
        {
            builderScript.Skin = (Material)EditorGUILayout.ObjectField("Skin:", builderScript.Skin, typeof(Material), false);
        }
        if (builderScript.SpawnNPC == NPCBuilderScript.NPCs.Pedestrian)
        {
            builderScript.NPCWealth = (NPCBuilderScript.Wealth)EditorGUILayout.EnumPopup("Wealth Level:", builderScript.NPCWealth);
            builderScript.Hat = (NPCBuilderScript.Hats)EditorGUILayout.EnumPopup("Hat type:", builderScript.Hat);
            builderScript.LeftHand = (NPCBuilderScript.LeftHandAccessories)EditorGUILayout.EnumPopup("Lefthand Accessory:", builderScript.LeftHand);
        }
        if (GUILayout.Button("Spawn NPC"))
            builderScript.BuildObject();
//        base.OnInspectorGUI();
    }

}
