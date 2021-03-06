﻿namespace TestPerlinNoise
{
  using UnityEngine;
  using UnityEngine.UI;
  using System.Collections;
  using System.Collections.Generic;
  using System;
  using Random = UnityEngine.Random;

  using UnityEngine.Events;
  using UnityEngine.EventSystems;

  public class Test_by_seed: MonoBehaviour
  {
    public int pixWidth;
    public int pixHeight;
    public float scale = 1.0F;
    private Texture2D noiseTex;
    private Color[] pix;
    private Renderer rend;

    public long seed = 0;
    private long lastSeed = 1;
    private float lastXOrg;
    private float lastYOrg;

    void Start ()
    {
      rend = GetComponent<Renderer> ();
      noiseTex = new Texture2D (pixWidth, pixHeight);
      pix = new Color[noiseTex.width * noiseTex.height];
      rend.material.mainTexture = noiseTex;
    }

    void CalcNoise (long seed )
    {
      float xOrg;
      float yOrg;

      if (seed != lastSeed) {
        xOrg = (seed * 15013 + 4585787) % 50000;
        yOrg = (seed * 8627 + 196051) % 50000;
      } else {
        xOrg = lastXOrg;
        yOrg = lastYOrg;
      }
      lastXOrg = xOrg;
      lastYOrg = yOrg;
      lastSeed = seed;

      float y = 0.0F;
      while (y < noiseTex.height) {
        float x = 0.0F;
        while (x < noiseTex.width) {
          float xCoord = xOrg + (x / noiseTex.width) * scale;
          float yCoord = yOrg + (y / noiseTex.height) * scale;
          float sample = Mathf.PerlinNoise (xCoord, yCoord);
          pix [(int)(y * noiseTex.width + x)] = new Color (sample, sample, sample);
          x++;
        }
        y++;
      }
      noiseTex.SetPixels (pix);
      noiseTex.Apply ();
    }

    void Update ()
    {
      CalcNoise (seed);
    }
  }
}

