using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(AirVRServerAudioOutputRouter))]
public class AirVRServerAudioOutputRouterEditor : Editor {
    private SerializedProperty propInput;
    private SerializedProperty propOutput;
    private SerializedProperty propTargetAudioMixer;
    private SerializedProperty propExposedRendererIDParameterName;
    private SerializedProperty propTargetAirVRClient;

    void OnEnable() {
        propInput = serializedObject.FindProperty("input");
        propOutput = serializedObject.FindProperty("output");
        propTargetAudioMixer = serializedObject.FindProperty("targetAudioMixer");
        propExposedRendererIDParameterName = serializedObject.FindProperty("exposedRendererIDParameterName");
        propTargetAirVRClient = serializedObject.FindProperty("targetAirVRClient");
    }

    public override void OnInspectorGUI() {
        serializedObject.Update();

        EditorGUILayout.PropertyField(propInput);
        if (propInput.enumValueIndex == (int)AirVRServerAudioOutputRouter.Input.AudioPlugin) {
            EditorGUILayout.BeginVertical("Box");
            EditorGUILayout.PropertyField(propTargetAudioMixer);
            EditorGUILayout.PropertyField(propExposedRendererIDParameterName);
            EditorGUILayout.EndVertical();
        }

        EditorGUILayout.PropertyField(propOutput);
        if (propOutput.enumValueIndex == (int)AirVRServerAudioOutputRouter.Output.One) {
            EditorGUILayout.BeginVertical("Box");
            EditorGUILayout.PropertyField(propTargetAirVRClient);
            EditorGUILayout.EndVertical();
        }

        serializedObject.ApplyModifiedProperties();
    }
}
