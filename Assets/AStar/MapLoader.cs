using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public abstract class MapLoader : MonoBehaviour
{
    int _height;
    int _width;
    int[,] _mapData;

    public int[,] MapData => _mapData;

    public int GetHeight() { return _height; }
    public int GetWidth() { return _width;}

    public void SetMapData()
    {
        var csvData = Resources.Load("CSV/MapData") as TextAsset;
        StringReader render = new StringReader(csvData.text);
        render.ReadLine();
        render.ReadLine();
        int[] mapSize = Array.ConvertAll(render.ReadLine().Split(','), int.Parse);
        _height = mapSize[0];
        _width = mapSize[1];
        _mapData = new int[_height, _width];
        for(int h = 0; h < _height; h++)
        {
            int[] lineData = Array.ConvertAll(render.ReadLine().Split(','), int.Parse);
            for(int w = 0; w < _width; w++)
            {
                _mapData[h, w] = lineData[w];
            }
        }
    }
}
