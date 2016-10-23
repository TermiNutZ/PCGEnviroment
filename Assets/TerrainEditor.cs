using UnityEditor;
using UnityEngine;

namespace Assets
{
    [CustomEditor(typeof(TerrainGeneration))]
    public class TerrainEditor : Editor
    {
        public float frequency = 1.0f;
        public int octaves = 1;
        public float redistribution = 1.0f;

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PrefixLabel("Frequency");
            frequency = EditorGUILayout.Slider(frequency, 1, 16);
            EditorGUILayout.EndHorizontal();

            
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PrefixLabel("Octaves");
            octaves = EditorGUILayout.IntSlider(octaves, 1, 8);
            EditorGUILayout.EndHorizontal();

            
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PrefixLabel("Redistribution");
            redistribution = EditorGUILayout.Slider(redistribution, 0.1f, 4f);
            EditorGUILayout.EndHorizontal();

            TerrainGeneration terrainGenerator = (TerrainGeneration) target;
            if (GUILayout.Button("Generate Terrain"))
            {
                terrainGenerator.generateTerrain(frequency, octaves, redistribution);
            }
        }
    }
}