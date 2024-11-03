using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PathNode
{
   private GridLogic<PathNode> _grid;
   public int _x;
   public int _y;
   public int gCost;
   public int hCost;
   public int fCost;
   public bool isWalkable;
   public PathNode ParentNode;
   public PathNode(GridLogic<PathNode> grid,int x,int y)
   {
      _x = x;
      _y = y;
      _grid = grid;
      var result = UnityEngine.Physics2D.OverlapPointAll(grid.GetWorldPositionFromCellPosition(_x, _y)+new Vector3(grid.GetCellSize(),grid.GetCellSize())*.5f);
      int i = 0;
      while (i<result.Length && result[i].gameObject.tag.ToString()!="Tilemap")
      {
         i = i + 1;
      }
      if (i==result.Length)
      {
         isWalkable = true;
      }
      else
      {
         isWalkable = false;
      }

   }
   
   public void CalculFCost()
   {
      fCost= gCost+hCost;
   }
   public override string ToString()
   {
      if (isWalkable)
      {
         return _x+","+_y;
      }
      else
      {
         return "WALL";
      }
      
   }
}
