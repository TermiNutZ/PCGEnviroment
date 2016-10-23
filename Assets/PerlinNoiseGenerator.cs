using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NoiseTest;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets
{
    class PerlinNoiseGenerator
    {

        public static float[,] CalcNoise(int width, int height, float scale)
        {
            var heights = new float[width, height];
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    float xCoord =  (float)i / width * scale;
                    float yCoord = (float)j /height * scale;
                    float sample = Mathf.PerlinNoise(xCoord, yCoord);
                    heights[i, j] = sample;
                }
            }

            return heights;
        }

        public static float[,] RedistributionNoise(int width, int height, float frequency,
            int octaves, float redistribution)
        {
            Random.InitState((int)DateTime.Now.Ticks);
            
            var heights = new float[width, height];
            float amplitude = 1.0f;
            float seed = Random.value;
            float scale = 0.25f;
            for (int oct = 1; oct <= octaves; oct++){
                
                for (int i = 0; i < width; i++)
                {
                    for (int j = 0; j < height; j++)
                    {
                        float xCoord = seed + (float)i / width * scale;
                        float yCoord = seed + (float)j / height  * scale;
                        
                        float sample = amplitude * Mathf.PerlinNoise(xCoord, yCoord);
                        heights[i, j] += sample;
                    }
                }
                amplitude /= 2f;
                scale *= frequency;
            }

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    heights[i, j] = (float)Math.Pow(heights[i,j], redistribution);
                }
            }


            return Normalise(heights);
        }

        private static OpenSimplexNoise sn = new OpenSimplexNoise((int)DateTime.Now.Ticks);
        public static float GenerateSkyNoise(float x, float y, float z)
        {
            float result = 0.0f;
            float amplitude = 1.0f;
            float total = 0.0f;

            float freq = 1.0f;
            for (int oct = 0; oct < 7; oct++)
            {
                float temp = (float) sn.Evaluate(x*freq, y*freq, z*freq);
                result += amplitude*temp;

                total += amplitude;
                amplitude /= 2f;
                freq *= 2f;
            }

            result = result/total*0.5f + 0.5f;
            result = Mathf.Max(result + 0.5f, 0) / (1.0f + 0.5f); // [0, 1.0]

            result = Mathf.Pow(result, 1 + 2 * (1 - 0.5f));

            return result;
        }



        private static float[,] Normalise(float[,] heights)
        {
            float maxH = -1.0f;
            float minH = 1000000.0f;
            for (int i = 0; i < heights.GetLength(0); i++)
            {
                for (int j = 0; j < heights.GetLength(1); j++)
                {
                    maxH = Math.Max(maxH, heights[i,j]);
                    minH = Math.Min(minH, heights[i, j]);
                }
            }
            float range = maxH - minH;

            for (int i = 0; i < heights.GetLength(0); i++)
            {
                for (int j = 0; j < heights.GetLength(1); j++)
                {
                    heights[i, j] = (heights[i, j] - minH)/range;
                }
            }

            return heights;
        }

        
    }
}
