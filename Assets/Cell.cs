using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour
{
    FloorData _floorData;
    Position _myPosition;
    Position _basePosition;
    int _predictedCost;
    int _realCost;

    public FloorData Floor { get => _floorData; set => _floorData = value; }
    public Position MyPosition { get => _myPosition; set => _myPosition = value; }
    public Position BasePosition { get => _basePosition; set => _basePosition = value; }
    public int PredictedCost { get => _predictedCost; set => _predictedCost = value; }
    public int RealCost { get => _realCost; set => _realCost = value; }
    public int Score => _predictedCost + _realCost;
}
public enum FloorData
{
    Floor,
    Wall,
    Start,
    Goal,
    Open,
    Close,
    Ruto,
}

