using UnityEngine;
using System.Collections;
using Assets;

public class TerrainGeneration : MonoBehaviour
{
    public void generateTerrain(float frequency, int octaves, float redistribution)
    {
        Terrain terrain = (Terrain)GetComponent(typeof(Terrain));

        TerrainData terrainData = terrain.terrainData;
        int tW = terrainData.heightmapWidth;
        int tH = terrainData.heightmapHeight;
        terrainData.SetHeights(0, 0, PerlinNoiseGenerator.RedistributionNoise(tW, tH, frequency, octaves, redistribution));
    }
}
