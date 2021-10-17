using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeRenderer : MonoBehaviour
{
    [SerializeField]
    private int width = 10;

    [SerializeField]
    private int height = 10;

    [SerializeField]
    private Transform wallPrefab;

    [SerializeField]
    private float cellSize = 1;
    // Start is called before the first frame update
    void Start()
    {
        WallState[,] maze = MazeGenerator.Generate(width, height);
        Draw(maze);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Draw(WallState[,] maze)
    {
        for(int i = 0; i < width; i++)
        {
            for(int j = 0; j < height; j++)
            {
                
                var cell = maze[i, j];
                var position = new Vector3(-width / 2 + i, 0 , -height / 2 + j);

                if (cell.HasFlag(WallState.UP))
                {
                    var topWall = Instantiate(wallPrefab, transform) as Transform;
                    topWall.position = position + new Vector3(0,0,cellSize/2);
                    topWall.localScale = new Vector3(cellSize,topWall.localScale.y, topWall.localScale.z);
                }
                if (cell.HasFlag(WallState.LEFT))
                {
                    var leftWall = Instantiate(wallPrefab, transform) as Transform;
                    leftWall.position = position + new Vector3(-cellSize / 2, 0, 0);
                    leftWall.localScale = new Vector3(cellSize, leftWall.localScale.y, leftWall.localScale.z);
                    leftWall.eulerAngles = new Vector3(0, 90, 0);
                }

                if(i == width - 1)
                {
                    if (cell.HasFlag(WallState.RIGHT))
                    {
                        var rightWall = Instantiate(wallPrefab, transform) as Transform;
                        rightWall.position = position + new Vector3(cellSize / 2, 0, 0);
                        rightWall.localScale = new Vector3(cellSize, rightWall.localScale.y, rightWall.localScale.z);
                        rightWall.eulerAngles = new Vector3(0, 90, 0);
                    }
                }
                if(j == 0)
                {
                    if (cell.HasFlag(WallState.DOWN))
                    {
                        var downWall = Instantiate(wallPrefab, transform) as Transform;
                        downWall.position = position + new Vector3(0, 0, -cellSize / 2);
                        downWall.localScale = new Vector3(cellSize, downWall.localScale.y, downWall.localScale.z);
                    }
                }
            }
        }
    }
}
