using System;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[ExecuteInEditMode]
public class PGCTerrain : MonoBehaviour
{
    public Terrain terrain;
    
    [SerializeField]
    public TerrainData terrainData;

    public Vector3 heightMapScale;
    
    
    //values from the slides 
    public float mPerlinXScale = 0.5f;
    public float mPerlinYScale = 0.5f;
    [Range(1,50)]
    public int mPerlinOctaves = 4;
    
    public float mPerlinPersistance = 0.5f;
    public float mPerlinHeightScale = 15.0f;
    public float mPerlinLacunarity = 4.0f;
    public float mPerlinOffsetX = 10;
    public float mPerlinOffsetY = 0;
    public bool remove = false;

    public int width = 128;
    public int height = 128;
    public int depth = 0;

    public bool singlePerlin = false;
    public bool randomPerlin = false;
    public bool multiplePerlin = false;
    public bool terrainreset = false;
    

    [Range(0,10)]
    public float randomRangeHeight;
    
    void Start()
    {
        randomRangeHeight = 0.0f;
        StartCoroutine(PerlinWalk());
        // This is how the terrain walks 
    }

    void Update()
    {
        if(singlePerlin) SinglePerlinTerrain();
        if (multiplePerlin) MultiplePerlinTerrain();
        if(randomPerlin) RandomTerrain();
        if(terrainreset) ResetTerrain();

    }
    
    IEnumerator PerlinWalk()
    {

        float timePassed = 0;
        while (timePassed < 3)
        {
            mPerlinOffsetX += .03f;
            timePassed += Time.deltaTime;
            yield return null;
        }


    }
    
    void OnEnable()
    {
        terrain = this.GetComponent<Terrain>();
        terrainData = Terrain.activeTerrain.terrainData;
        terrainData.size = new Vector3(width, depth, height);
        terrainData.baseMapResolution = depth;
    }


    public void RandomTerrain() //Generate Random Terrain
    { 
        singlePerlin = false; 
        randomPerlin = true; 
        multiplePerlin = false;
        terrainreset = false;
        
        float[,] heightMap;
        heightMap = terrainData.GetHeights(0, 0, terrainData.heightmapWidth, terrainData.heightmapHeight);
        for (int x = 0; x < terrainData.heightmapWidth; x++)
        {
            for (int z = 0; z < terrainData.heightmapHeight; z++)
            {
                heightMap[x, z] += Random.Range(-randomRangeHeight, randomRangeHeight);
            }
        }
        NormieTerrian(heightMap);
        terrainData.SetHeights(0, 0, heightMap);
    }
    
    public void NormieTerrian(float[,] heightMap) // Normalizes the Terrain
    {
        float max = 20;
        float min = -20;

        for(int x = 0; x < terrainData.heightmapWidth; x++)
        {
            for (int y = 0; y < terrainData.heightmapHeight; y++) {
                float tmp = (heightMap[x, y] - min) / (max - min);
                heightMap[x, y] = tmp;
            }
        }
    }
    
    public void SinglePerlinTerrain() // Generate Single Perlin terrain
    {
        singlePerlin = true; 
        randomPerlin = false; 
        multiplePerlin = false;
        terrainreset = false;

        float z = depth;
        float[,] heightMap;
        heightMap = terrainData.GetHeights(0, 0, terrainData.heightmapWidth, terrainData.heightmapHeight);

        for (int x = 0; x < terrainData.heightmapWidth; x++)
        {
            for (int y = 0; y < terrainData.heightmapHeight; y++)
            {
                heightMap[x, y] = CalcSinglePerlin(x, y, z) * mPerlinHeightScale;
                
            }
        } 
        NormieTerrian(heightMap);
        terrainData.SetHeights(0, 0, heightMap);
    }

    float CalcSinglePerlin(float x, float y, float z)
    {

        float xCord = (float) x / width * mPerlinXScale + mPerlinOffsetX;
        float yCord = (float) y / height * mPerlinYScale + mPerlinOffsetY;
        
        return Mathf.PerlinNoise(xCord, yCord); 
    }
    
    public void MultiplePerlinTerrain()
    {
        singlePerlin = false; 
        randomPerlin = false; 
        multiplePerlin = true;
        terrainreset = false;

        
        float z = depth;
        float[,] heightMap;
        heightMap = terrainData.GetHeights(0, 0, terrainData.heightmapWidth, terrainData.heightmapHeight);

        for (int x = 0; x < terrainData.heightmapWidth; x++)
        {
            for (int y = 0; y < terrainData.heightmapHeight; y++)
            {
                heightMap[x, y] += CalcMultiplePerlin(x, y, z, mPerlinOctaves, mPerlinPersistance) * mPerlinHeightScale;
            }
        }
        NormieTerrian(heightMap);
        terrainData.SetHeights(0, 0, heightMap);
    }

    float CalcMultiplePerlin(float x, float y, float z, int octaves, float persistance)
    {

        float frequency = 1;
        float amplitude = 1;
        float maxValue = 0;
        float total = 0;
        for(int i = 0; i < octaves; i++)
        {
            total += CalcSinglePerlin(x * frequency, y * frequency, z * frequency) * amplitude;
            maxValue += amplitude;
            amplitude *= persistance;
            frequency *= mPerlinLacunarity;
        }

        return total / maxValue;
    }
    
    public void ResetTerrain() // resets the terrain 
    {
        singlePerlin = false; 
        randomPerlin = false; 
        multiplePerlin = false;
        terrainreset = true;
        float[,] heightMap;
        heightMap = new float[terrainData.heightmapWidth, terrainData.heightmapHeight];

        for (int x = 0; x < terrainData.heightmapWidth; x++)
        {
            for (int z = 0; z < terrainData.heightmapHeight; z++)
            {
                heightMap[x, z] = 0.5f;
            }
        }
        
        terrainData.SetHeights(0, 0, heightMap);
    }
}