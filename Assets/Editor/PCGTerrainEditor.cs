using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(PGCTerrain))]
[CanEditMultipleObjects]
public class PGCTerrainEditor : Editor
{

    SerializedProperty heightMapScale;
    SerializedProperty heightMapImage;
    SerializedProperty mPerlinXScale;
    SerializedProperty mPerlinYScale;
    SerializedProperty mPerlinOffsetX;
    SerializedProperty mPerlinOffsetY;
    SerializedProperty mPerlinOctaves;
    
    [Range(0f, 1f)]
    SerializedProperty mPerlinPersistance;
    SerializedProperty mPerlinHeightScale;
    SerializedProperty mPerlinLacunarity;
    
    SerializedProperty randomRangeHeight;
    
    SerializedProperty voronoiHeight;
    SerializedProperty voronoiDist;
    SerializedProperty voronoiAmount;

    void Start()
    {
        UnityEditor.SceneManagement.EditorSceneManager.MarkAllScenesDirty();
    }


    void OnEnable()
    {
        heightMapScale = serializedObject.FindProperty("heightMapScale");
        mPerlinXScale = serializedObject.FindProperty("mPerlinXScale");
        mPerlinYScale = serializedObject.FindProperty("mPerlinYScale");
        mPerlinOffsetX = serializedObject.FindProperty("mPerlinOffsetX");
        mPerlinOffsetY = serializedObject.FindProperty("mPerlinOffsetY");
        mPerlinOctaves = serializedObject.FindProperty("mPerlinOctaves");
        mPerlinPersistance = serializedObject.FindProperty("mPerlinPersistance");
        mPerlinHeightScale = serializedObject.FindProperty("mPerlinHeightScale");
        mPerlinLacunarity = serializedObject.FindProperty("mPerlinLacunarity");
        
        randomRangeHeight = serializedObject.FindProperty("randomRangeHeight");

        voronoiHeight = serializedObject.FindProperty("voronoiHeight");
        voronoiDist = serializedObject.FindProperty("voronoiDist");
        voronoiAmount = serializedObject.FindProperty("voronoiAmount");
        
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        PGCTerrain terrain = (PGCTerrain)target;

        //Generate random perlin 
        
        GUILayout.Label("Generate Random Perlin", EditorStyles.boldLabel);

            EditorGUILayout.PropertyField(randomRangeHeight); 
            
            if (GUILayout.Button("Generate Random Perlin")) terrain.RandomTerrain();
          
            
            //Generate single perlin 

            GUILayout.Label("Generate Single Perlin", EditorStyles.boldLabel);

            EditorGUILayout.PropertyField(mPerlinXScale);
            EditorGUILayout.PropertyField(mPerlinYScale);
            EditorGUILayout.PropertyField(mPerlinHeightScale);
            EditorGUILayout.PropertyField(mPerlinOffsetX);
            EditorGUILayout.PropertyField(mPerlinOffsetY); 
            
            
            if (GUILayout.Button("Generate Single Perlin")) terrain.SinglePerlinTerrain();

            //Generate multiple perlin

            GUILayout.Label("Generate Multiple Perlin", EditorStyles.boldLabel);
            
                EditorGUILayout.PropertyField(mPerlinXScale);
                EditorGUILayout.PropertyField(mPerlinYScale);
                EditorGUILayout.PropertyField(mPerlinHeightScale);
                EditorGUILayout.PropertyField(mPerlinOffsetX);
                EditorGUILayout.PropertyField(mPerlinOffsetY);
                EditorGUILayout.PropertyField(mPerlinOctaves);
                EditorGUILayout.PropertyField(mPerlinPersistance);
                EditorGUILayout.PropertyField(mPerlinLacunarity); 
                
                if (GUILayout.Button("Multiple Perlin")) terrain.MultiplePerlinTerrain();
        
                


                GUILayout.Label("Reset Terrain", EditorStyles.boldLabel);
        
        if (GUILayout.Button("Reset"))  terrain.ResetTerrain(); 
        
        serializedObject.ApplyModifiedProperties();
    }


}
