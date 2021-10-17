using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;



[Flags]
public enum WallState
{
    // NO WALLS --> 0000
    // ALL WALLS --> 1111
    LEFT = 1, //0001
    RIGHT = 2, //0010
    UP = 4, //0100
    DOWN = 8, //1000

    VISITED = 128, //1000 0000
}

public struct Position
{
    public int X;
    public int Y;
}

public struct Neighbour
{
    public Position position;
    public WallState SharedWall;
}



public class MazeGenerator {

    private static WallState GetOppositeWall(WallState wall)
    {
        switch (wall)
        {
            case WallState.LEFT: return WallState.RIGHT;

            case WallState.RIGHT: return WallState.LEFT;

            case WallState.UP: return WallState.DOWN;

            case WallState.DOWN: return WallState.UP;

            default: return WallState.LEFT;
        }
    }

    public static WallState[,] ApplyRecursiveBackTracker(WallState[,] maze,int width, int height)
    {
        var stackPos = new Stack<Position>();
        var rng = new System.Random(/*seed*/);
        var pos = new Position { X = rng.Next(0, width), Y = rng.Next(0, height) };

        maze[pos.X, pos.Y] |= WallState.VISITED;
        stackPos.Push(pos);

        while (stackPos.Count > 0)
        {
            var current = stackPos.Pop();
            var neighbours = GetUnvisitedNeighbours(current, maze, width, height);

            if (neighbours.Count > 0)
            {
                stackPos.Push(current);

                var randIndex = rng.Next(0, neighbours.Count);
                var randomNeighbour = neighbours[randIndex];

                var nPosition = randomNeighbour.position;
                maze[current.X, current.Y] &= ~randomNeighbour.SharedWall;
                maze[nPosition.X, nPosition.Y] &= ~GetOppositeWall(randomNeighbour.SharedWall);
                maze[nPosition.X, nPosition.Y] |= WallState.VISITED;

                stackPos.Push(nPosition);
            }

        }
        return maze;
    }

    public static List<Neighbour> GetUnvisitedNeighbours(Position p,WallState[,] maze,int width,int height)
    {
        var list = new List<Neighbour>();

        if(p.X > 0)//have a left neighbour
        {
            if (!maze[p.X - 1, p.Y].HasFlag(WallState.VISITED))
            {
                list.Add(new Neighbour
                {
                    position = new Position
                    {
                        X = p.X - 1,
                        Y = p.Y
                    },
                    SharedWall = WallState.LEFT
                });
            }
        }
        if (p.Y > 0)//have a bottom neighbour
        {
            if (!maze[p.X, p.Y - 1].HasFlag(WallState.VISITED))
            {
                list.Add(new Neighbour
                {
                    position = new Position
                    {
                        X = p.X ,
                        Y = p.Y - 1,
                    },
                    SharedWall = WallState.DOWN,
                });
            }
        }
        if (p.X < width-1)//have a left neighbour
        {
            if (!maze[p.X + 1, p.Y].HasFlag(WallState.VISITED))
            {
                list.Add(new Neighbour
                {
                    position = new Position
                    {
                        X = p.X + 1,
                        Y = p.Y
                    },
                    SharedWall = WallState.RIGHT
                });
            }
        }
        if (p.Y < height-1)//have a left neighbour
        {
            if (!maze[p.X, p.Y + 1].HasFlag(WallState.VISITED))
            {
                list.Add(new Neighbour
                {
                    position = new Position
                    {
                        X = p.X ,
                        Y = p.Y + 1
                    },
                    SharedWall = WallState.UP,
                });
            }
        }

        return list;
    }

    public static WallState[,] Generate(int width,int height)
    {
        WallState[,] maze = new WallState[width,height];
        WallState initial = WallState.DOWN | WallState.UP | WallState.LEFT | WallState.RIGHT;


        for(int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                maze[i, j] = initial;
            }
        }

        return ApplyRecursiveBackTracker(maze, width,height) ;
    }

}
