using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
public class GridLogic<TGridObject>
{
    private int _width;
    private int _height;
    private TGridObject[,] _gridArray;
    private int _cellSize;
    private TextMesh[,] DebugTextMeshesArray;
    private Vector3 _originPosition;
    private bool DebugMode;
    public GridLogic(int width, int height, int cellSize, Vector3 originPosition, Func<GridLogic<TGridObject>, int, int, TGridObject> CreateGridObject,bool debugMode)
    {
        _width = width;
        _height = height;
        _cellSize = cellSize;
        _originPosition = originPosition;
        _gridArray = new TGridObject[_width, _height];
        DebugTextMeshesArray = new TextMesh[_width, _height];
        DebugMode = debugMode;
        
        for (int i = 0; i < _gridArray.GetLength(0); i++)
        {
            for (int j = 0; j < _gridArray.GetLength(1); j++)
            {
                _gridArray[i, j] = CreateGridObject(this,i,j);
                if (DebugMode)
                {
                    DebugTextMeshesArray[i,j]=DebugDraw( _gridArray[i, j]?.ToString(), GetWorldPositionFromCellPosition(i, j)+new Vector3(_cellSize,_cellSize)*.5f, 150, Color.black,
                        TextAnchor.MiddleCenter, TextAlignment.Left);
                    Debug.DrawLine(GetWorldPositionFromCellPosition(i,j),GetWorldPositionFromCellPosition(i,j+1),Color.black,100f);
                    Debug.DrawLine(GetWorldPositionFromCellPosition(i,j),GetWorldPositionFromCellPosition(i+1,j),Color.black,100f);
                }
                
            }
        }

        if (DebugMode)
        {
            Debug.DrawLine(GetWorldPositionFromCellPosition(0,_height),GetWorldPositionFromCellPosition(_width,_height),Color.black,100f);
            Debug.DrawLine(GetWorldPositionFromCellPosition(_width,0),GetWorldPositionFromCellPosition(_width,_height),Color.black,100f);
        }
        
        
    }
    public Vector3 GetWorldPositionFromCellPosition(int x, int y)
    {
        return new Vector3(x, y) * _cellSize +_originPosition;
    }
    public Vector2Int GetCellPositionFromWorldPosition(Vector3 WorldPosition)
    {
        return new Vector2Int(Mathf.FloorToInt((WorldPosition-_originPosition).x/ _cellSize),
            Mathf.FloorToInt((WorldPosition-_originPosition).y/ _cellSize));
    }
    public TGridObject GetGridObject(int x, int y)
    {
        if (x>=0 && y>=0 && x<_width && y<_height)
        {
            return _gridArray[x, y];
        }
        else
        {
            return default;
        }
    }

    public TGridObject GetGridObject(Vector3 WorldPosition)
    {
        Vector2Int result = GetCellPositionFromWorldPosition(WorldPosition);
        return GetGridObject(result.x,result.y);
    }
    
    public int GetWidth() {
        return _width;
    }
    public int GetCellSize() {
        return _cellSize;
    }

    public int GetHeight() {
        return _height;
    }
    
    public void SetGridObject(int x, int y, TGridObject value)
    {
        if (x>=0 && y>=0 && x<_width && y<_height)
        {
            _gridArray[x, y] = value;
            if (DebugMode)
            {
                DebugTextMeshesArray[x, y].text = _gridArray[x, y].ToString();   
            }
            
        }
       
    }
    public void SetGridObject(Vector3 WorldPosition, TGridObject Value)
    {
        Vector2Int result = GetCellPositionFromWorldPosition(WorldPosition);
        SetGridObject(result.x,result.y,Value);
    }
    
    private static TextMesh DebugDraw( string text, Vector3 localPosition, int fontSize, Color color, TextAnchor textAnchor, TextAlignment textAlignment) {
        GameObject gameObject = new GameObject("World_Text", typeof(TextMesh));
        Transform transform = gameObject.transform;
        transform.position = localPosition;
        TextMesh textMesh = gameObject.GetComponent<TextMesh>();
        textMesh.anchor = textAnchor;
        textMesh.alignment = textAlignment;
        textMesh.text = text;
        textMesh.fontSize = fontSize;
        textMesh.color = color;
        textMesh.characterSize = 0.03f;
        return textMesh;
    }
}
