using UnityEngine;
using System.Collections;
using System.IO;
using Assets;
using NoiseTest;
using UnityEditor;

public class SkyboxSetter : MonoBehaviour
{
    public static Material CreateSkyboxMaterial(SkyboxManifest manifest)
    {
        Material result = new Material(Shader.Find("RenderFX/Skybox"));
        result.SetTexture("_FrontTex", manifest.textures[0]);
        result.SetTexture("_BackTex", manifest.textures[1]);
        result.SetTexture("_LeftTex", manifest.textures[2]);
        result.SetTexture("_RightTex", manifest.textures[3]);
        result.SetTexture("_UpTex", manifest.textures[4]);
        result.SetTexture("_DownTex", manifest.textures[5]);
        
        return result;
    }

    public Texture2D[] textures;
    readonly Color _blueColor = new Color(47f / 255.0f, 81f / 255.0f, 113f / 255.0f);

    void OnEnable()
    {
        int width = 512;
        int height = 512;
        Texture2D[] textures = new Texture2D[6];

        textures[0] = getFrontTexture(512);
        textures[1] = getBackTexture(512);
        textures[2] = getLeftTexture(512);
        textures[3] = getRightTexture(512);
        textures[4] = getUpTexture(512);
        textures[5] = GetBlueField(512, 512);
        for (int i = 0; i < 6; i++)
            textures[i].wrapMode = TextureWrapMode.Clamp;

        for (int i = 0; i < 6; i++)
            AssetDatabase.CreateAsset(textures[i], "Assets/text" + i + ".mat");

        SkyboxManifest manifest = new SkyboxManifest(textures);
        Material material = CreateSkyboxMaterial(manifest);
        AssetDatabase.CreateAsset(material, "Assets/kek.mat");
        SetSkybox(material);
        enabled = false;
    }

    void SetSkybox(Material material)
    {
        GameObject camera = Camera.main.gameObject;
        Skybox skybox = camera.GetComponent<Skybox>();
        if (skybox == null)
            skybox = camera.AddComponent<Skybox>();
        skybox.material = material;
    }

    private Texture2D getFrontTexture(int size)
    {
        Texture2D texture = new Texture2D(size, size, TextureFormat.RGB24, false);
        
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                int x = i - size/2;
                int y = j - size/2;
                int z = size/2;
                float length = Mathf.Sqrt(x*x + y*y + z*z);

                float xx = (x/length);
                float yy = (y / length);
                float zz = (z / length);

                float noise = PerlinNoiseGenerator.GenerateSkyNoise(xx, yy, zz);

                texture.SetPixel(i, j, GetColor(_blueColor, Color.white, Mathf.Max(0.0f, noise)));
            }
        }

        return texture;
    }

    private Texture2D getBackTexture(int size)
    {
        Texture2D texture = new Texture2D(size, size, TextureFormat.RGB24, false);

        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                int x = size / 2 - i;
                int y = j - size / 2;
                int z = -size / 2;
                float length = Mathf.Sqrt(x * x + y * y + z * z);

                float xx = (x / length);
                float yy = (y / length);
                float zz = (z / length);

                float noise = PerlinNoiseGenerator.GenerateSkyNoise(xx, yy, zz);

                texture.SetPixel(i, j, GetColor(_blueColor, Color.white, Mathf.Max(0.0f, noise)));
            }
        }

        return texture;
    }

    private Texture2D getLeftTexture(int size)
    {
        Texture2D texture = new Texture2D(size, size, TextureFormat.RGB24, false);

        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                int x = size / 2;
                int y = j - size / 2;
                int z = size / 2 - i;
                float length = Mathf.Sqrt(x * x + y * y + z * z);

                float xx = (x / length);
                float yy = (y / length);
                float zz = (z / length);

                float noise = PerlinNoiseGenerator.GenerateSkyNoise(xx, yy, zz);

                texture.SetPixel(i, j, GetColor(_blueColor, Color.white, Mathf.Max(0.0f, noise)));
            }
        }

        return texture;
    }

    private Texture2D getRightTexture(int size)
    {
        Texture2D texture = new Texture2D(size, size, TextureFormat.RGB24, false);

        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                int x = -size / 2;
                int y = j - size / 2;
                int z = i- size/2;
                float length = Mathf.Sqrt(x * x + y * y + z * z);

                float xx = (x / length);
                float yy = (y / length);
                float zz = (z / length);

                float noise = PerlinNoiseGenerator.GenerateSkyNoise(xx, yy, zz);

                texture.SetPixel(i, j, GetColor(_blueColor, Color.white, Mathf.Max(0.0f, noise)));
            }
        }

        return texture;
    }

    private Texture2D getUpTexture(int size)
    {
        Texture2D texture = new Texture2D(size, size, TextureFormat.RGB24, false);

        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                int x = i - size / 2;
                int y = size / 2;
                int z = size / 2 - j;
                float length = Mathf.Sqrt(x * x + y * y + z * z);

                float xx = (x / length);
                float yy = (y / length);
                float zz = (z / length);

                float noise = PerlinNoiseGenerator.GenerateSkyNoise(xx, yy, zz);

                texture.SetPixel(i, j, GetColor(_blueColor, Color.white, Mathf.Max(0.0f, noise)));
            }
        }

        return texture;
    }

    private Texture2D getDownTexture(int size)
    {
        Texture2D texture = new Texture2D(size, size, TextureFormat.RGB24, false);

        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                int x = i - size / 2;
                int y = -size / 2;
                int z = j - size / 2 ;
                float length = Mathf.Sqrt(x * x + y * y + z * z);

                float xx = (x / length);
                float yy = (y / length);
                float zz = (z / length);

                float noise = PerlinNoiseGenerator.GenerateSkyNoise(xx, yy, zz);

                texture.SetPixel(i, j, GetColor(_blueColor, Color.white, Mathf.Max(0.0f, noise)));
            }
        }

        return texture;
    }
    //private Texture2D GenerateTexture2D(int size, int type)
    //{
    //    var texture = new Texture2D(size, size, TextureFormat.RGB24, false);

    //    float[,] alph = PerlinNoiseGenerator.GenerateSkyNoise(size, 2, 7, 0.5f, type);


    //    for (int i = 0; i < size; i++)
    //    {
    //        for (int j = 0; j < size; j++)
    //        {
    //            texture.SetPixel(i, j, GetColor(_blueColor , Color.white, Mathf.Max(0.0f,alph[i, j])));
    //        }
    //    }

    //    // Apply all SetPixel calls
    //    texture.Apply();

    //    return texture;
    //}

    private Texture2D GetFromBigTexture(Texture2D bigTexture, int x, int width, int height)
    {
        Texture2D result = GetBlueField(width, height);
        var colors = bigTexture.GetPixels(x, 0, width, bigTexture.height);
        result.SetPixels(0, 200, width, bigTexture.height, colors);
            
        result.Apply();
        return result;
    }

    private Texture2D GetBlueField(int width, int height)
    {
        Texture2D result = new Texture2D(width, height, TextureFormat.RGB24, false);
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < width; j++)
            {
                if (i < 50 && j < 50)
                    result.SetPixel(i, j, Color.white);
                else
                    result.SetPixel(i, j, _blueColor);
            }
        }
        return result;
    }

    Color GetColor(Color gradientStart, Color gradientEnd, float t)
    {
        float u = 1 - t;

        Color color = new Color((gradientStart.r * u + gradientEnd.r * t),
           (gradientStart.g * u + gradientEnd.g * t),
           (gradientStart.b * u + gradientEnd.b * t), 1.0f);

        return color;
    }
}

public struct SkyboxManifest
{
    public Texture2D[] textures;

    public SkyboxManifest(Texture2D [] text){
        textures = text;

    }

    public SkyboxManifest(Texture2D front, Texture2D back, Texture2D left, Texture2D right, Texture2D up, Texture2D down)
    {
        textures = new Texture2D[6]
        {
            front,
            back,
            left,
            right,
            up,
            down
        };
    }
}