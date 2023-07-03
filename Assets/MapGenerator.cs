using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapGenerator : MapLoader
{
    [SerializeField] int _cellSizeX;
    [SerializeField]int _cellSizeY;
    [SerializeField] Sprite _sprite;
    Vector2 _cellPosition;
    void Start()
    {
        SetMapData();
        _cellPosition = new Vector2(GetWidth() / 2 * -1, GetHeight() / 2);
        for(int h = 0; h < GetHeight(); h++)
        {
            for(int w = 0; w < GetWidth(); w++)
            {
                var cell = new GameObject($"{h}{w}");
                cell.transform.position = _cellPosition;
                cell.AddComponent<SpriteRenderer>();
                cell.GetComponent<SpriteRenderer>().sprite = _sprite;
                cell.transform.localScale = new Vector2(_cellSizeX, _cellSizeY);
                if (MapData[h, w] == 0)
                {
                    cell.GetComponent<SpriteRenderer>().color = Color.gray;
                }
                else if (MapData[h, w] == 1)
                {
                    cell.GetComponent<SpriteRenderer>().color = Color.white;
                }
                else if (MapData[h, w] == 2)
                {
                    cell.GetComponent<SpriteRenderer>().color = Color.blue;
                }
                else if (MapData[h, w] == 3)
                {
                    cell.GetComponent<SpriteRenderer>().color = Color.red;
                }
                _cellPosition.x += 1;
            }
            _cellPosition.x = GetWidth() / 2 * -1;
            _cellPosition.y -= 1;
        }
    }
}
