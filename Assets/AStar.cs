using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using System.Linq;
using System;
using Unity.VisualScripting;

public class AStar : MapLoader
{
    [SerializeField] int _cellSizeX;
    [SerializeField] int _cellSizeY;
    [SerializeField] GameObject _cellPrefab;
    Vector2 _cellPosition;
    Position _start = new Position(0, 0);
    Position _goal = new Position(0, 0);
    GameObject[,] _cellData;

    void Start()
    {
        SetMapData();
        _cellPosition = new Vector2(GetWidth() / 2 * -1, GetHeight() / 2);
        _cellData = new GameObject[GetHeight(), GetWidth()];
        for (int h = 0; h < GetHeight(); h++)
        {
            for (int w = 0; w < GetWidth(); w++)
            {
                var cell =  Instantiate(_cellPrefab);
                cell.transform.position = _cellPosition;
                cell.transform.localScale = new Vector2(_cellSizeX, _cellSizeY);
                if (MapData[h, w] == (int)FloorData.Floor)
                {
                    cell.GetComponent<SpriteRenderer>().color = Color.white;
                    cell.GetComponent<Cell>().Floor = FloorData.Floor;
                    cell.GetComponent<Cell>().MyPosition = new Position(h, w);
                }
                else if (MapData[h, w] == (int)FloorData.Wall)
                {
                    cell.GetComponent<SpriteRenderer>().color = Color.gray;
                    cell.GetComponent<Cell>().Floor = FloorData.Wall;
                }
                else if (MapData[h, w] == (int)FloorData.Start)
                {
                    cell.GetComponent<SpriteRenderer>().color = Color.blue;
                    cell.GetComponent<Cell>().Floor = FloorData.Start;
                    _start = new Position(h, w);
                }
                else if (MapData[h, w] == (int)FloorData.Goal)
                {
                    cell.GetComponent<SpriteRenderer>().color = Color.red;
                    cell.GetComponent<Cell>().Floor = FloorData.Goal;
                    _goal = new Position(h, w);
                }
                _cellData[h, w] = cell;
                _cellPosition.x += 1;
            }
            _cellPosition.x = GetWidth() / 2 * -1;
            _cellPosition.y -= 1;
        }
    }

    public void Search()
    {
        Cell firstTarget = _cellData[_start.x, _start.y].GetComponent<Cell>();
        List<Cell> searchCell = new List<Cell>() { firstTarget};
        Position target = new Position(0, 0);
        bool isGoal = false;
        while(!isGoal && searchCell.Count > 0)
        {
            Cell currentCell = _cellData[target.x, target.y].GetComponent<Cell>();
            if (currentCell.Floor == FloorData.Open)
            {
                currentCell.Floor = FloorData.Close;
            }
            currentCell = searchCell[0].GetComponent<Cell>();
            searchCell.RemoveAt(0);
            target = currentCell.MyPosition;
            foreach(var dir in Enum.GetValues(typeof(Direction)))
            {
                Position nextCell = new Position(target.x, target.y);
                switch (dir)
                {
                    case Direction.Up:
                        nextCell.x--;
                        break;
                    case Direction.Down:
                        nextCell.x++;
                        break;
                    case Direction.Left:
                        nextCell.y--;
                        break;
                    case Direction.Right:
                        nextCell.y++;
                        break;
                }

                if(nextCell.x >= 0 && nextCell.x < GetHeight() && nextCell.y >= 0 && nextCell.y < GetWidth())
                {
                    Cell next = _cellData[nextCell.x, nextCell.y].GetComponent<Cell>();
                    if(next.Floor == FloorData.Floor) 
                    {
                        next.Floor = FloorData.Open;
                        next.PredictedCost = Mathf.Abs(nextCell.x - _goal.x) + Mathf.Abs(nextCell.y - _goal.y);
                        next.RealCost = Mathf.Abs(nextCell.x - _start.x) + Mathf.Abs(nextCell.y - _start.y);
                        next.BasePosition = target;
                        searchCell.Add(next);
                        if(searchCell.Count > 1 && searchCell[0].Score > searchCell[searchCell.Count - 1].Score)
                        {
                            var tmp = searchCell[0];
                            searchCell[0] = searchCell[searchCell.Count - 1];
                            searchCell[searchCell.Count - 1] = tmp;
                        }
                    }
                    if(next.Floor == FloorData.Goal)
                    {
                        next.BasePosition = target;
                        isGoal = true;
                        break;
                    }
                }
            }
            if(isGoal)
            {

            }
        }
    }

    List<Position> SetRoute()
    {
        List<Position> route = new List<Position>();
        Position routePosition = new Position(0, 0);
        while(true)
        {
            route.Add(routePosition);
            if (routePosition.x == _start.x && routePosition.y == _start.y) break;
            var basePosition = _cellData[routePosition.x, routePosition.y].GetComponent<Cell>().BasePosition;
            routePosition = basePosition;
        }
        return route;
    }

    enum Direction
    {
        Up,
        Down,
        Left,
        Right,
    }
}
