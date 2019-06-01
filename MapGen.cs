using SadConsole;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using GoRogue;
using GoRogue.MapViews;
using System.Linq;

namespace Wang
{
    public class MapGen
    {
        Dictionary<int, List<int[,]>> blob;

        int tileSize;
        int mapSize;
        int size;
        int rand;
        bool overLap;

        ArrayMap<int> overlayMap;
        LambdaMapView<int> indexMap;
        ArrayMap<bool> walkMap;

        public MapGen(Dictionary<int, List<int[,]>> tileBlob)
        {
            blob = tileBlob;

            tileSize = blob.GetValueOrDefault(0)[0].GetLength(0);

        }

        public ArrayMap<Cell> GenerateMap(int mapSizeTiles, int Openness = 10, bool overLapTiles = true)
        {
            mapSize = mapSizeTiles;
            overLap = overLapTiles;
            rand = Openness;

            size = overLap == true ? (mapSize * tileSize) - mapSize + 1 : mapSize * tileSize;

            overlayMap = BuildOverlayMap();

            //We can use a LamdaMapView to extract the indexes
            indexMap = new LambdaMapView<int>(mapSizeTiles, mapSizeTiles, pos => overlayMap[(pos.X * 2) + 1, (pos.Y * 2) + 1]);

            ArrayMap<bool> walkMap = BuildWalkMap();

            ArrayMap<Cell> tiles = new ArrayMap<Cell>(size, size);

            //Flood the cells with walls to be on the safe side.
            FloodWalls(tiles);

            for (int x = 0; x < walkMap.Width; x++)
            {
                for (int y = 0; y < walkMap.Height; y++)
                {
                    if (walkMap[x, y])
                    {
                        tiles[x, y] = new Cell(Color.Gray, Color.Black, ' ');
                    }
                    else
                    {
                        tiles[x, y] = new Cell(Color.Gray, Color.Black, '#');
                    }
                }
            }

            return tiles;
        }

        ArrayMap<int> BuildOverlayMap()
        {
            int overlaySize = (mapSize * 2) + 1;
            overlayMap = new ArrayMap<int>(overlaySize, overlaySize);


            PerfectMaze();

            //Set indexes 
            for (int x = 0; x < overlayMap.Width; x++)
            {
                for (int y = 0; y < overlayMap.Height; y++)
                {
                    //If both numbers are odd it's the center
                    if (x % 2 != 0 && y % 2 != 0)
                    {
                        overlayMap[x, y] = (
                        overlayMap[x, y - 1]
                        + (overlayMap[x + 1, y - 1] * 2)
                        + (overlayMap[x + 1, y] * 4)
                        + (overlayMap[x + 1, y + 1] * 8)
                        + (overlayMap[x, y + 1] * 16)
                        + (overlayMap[x - 1, y + 1] * 32)
                        + (overlayMap[x - 1, y] * 64)
                        + (overlayMap[x - 1, y - 1] * 128)
                        );
                        // top + 2*topRight + 4*right + 8*bottomRight + 16*bottom + 32*bottomLeft + 64*left + 128* topLeft
                    }
                }
            }

            return overlayMap;
        }

        void PerfectMaze()
        {
            List<Coord> visted = new List<Coord>();
            Coord startCoord = new Coord(GoRogue.Random.SingletonRandom.DefaultRNG.Next(2, mapSize - 2), GoRogue.Random.SingletonRandom.DefaultRNG.Next(2, mapSize - 2));

            visted.Add(startCoord);
            overlayMap[Helpers.Overlaycoord(startCoord)] = 1;

            RunPefectMaze(visted);

        }

        //Based on the process here
        //http://cr31.co.uk/stagecast/wang/perfect.html
        void RunPefectMaze(List<Coord> visted)
        {
            //We're calling this recerscveily, if there's no more items in the list we need to bubble back up.
            if (visted.Count == 0)
            {
                return;
            }

            Coord current;

            //A given percentage of the time we select randomly else select the last added, this can be adjust to effect the layout of the maze
            if (GoRogue.Random.SingletonRandom.DefaultRNG.Next(1, 100) >= rand)
            {
                current = visted[visted.Count - 1];
            }
            else
            {
                int r = GoRogue.Random.SingletonRandom.DefaultRNG.Next(0, visted.Count);
                current = visted[r];
            }

            //Check for possible moves
            Coord[] moves = new Coord[4];
            moves[0] = current + Direction.UP;
            moves[1] = current + Direction.DOWN;
            moves[2] = current + Direction.LEFT;
            moves[3] = current + Direction.RIGHT;

            List<Coord> possibleMoves = new List<Coord>();

            foreach (Coord coord in moves)
            {
                Coord check = Helpers.Overlaycoord(coord);

                if (BoundsCheck(check))
                {
                    continue;
                }

                //Check if it's been carved 
                if (overlayMap[check] == 1)
                {
                    continue;
                }

                //Not out of bounds or carved so add it to possible moves
                possibleMoves.Add(coord);
            }

            //If there are no legal moves we remove the current tile from the visted list and try again.
            if (possibleMoves.Count == 0)
            {
                visted.RemoveAll(a => a == current);
                RunPefectMaze(visted);
                //if we reached here it means we're bubbling back up
                return;
            }

            int r2 = GoRogue.Random.SingletonRandom.DefaultRNG.Next(0, possibleMoves.Count);
            Coord next = possibleMoves[r2];
            visted.Add(next);


            //Carve the path
            Coord nextOverly = Helpers.Overlaycoord(next);

            //As we're having to set the nextOverly we'll look back to the previous tile
            Direction back = Direction.GetCardinalDirection(next, current);
            Coord edge = nextOverly + back;

            //Mark the tile
            overlayMap[nextOverly] = 1;
            //Set the edge 
            overlayMap[edge] = 1;

            //For blob mazes we need to do some hocus pocus on the corners to draw out connections 
            //If we're going up/down we need to check left/right cornors 
            if (back == Direction.UP || back == Direction.DOWN)
            {
                Coord left = edge + Direction.LEFT;
                Coord right = edge + Direction.RIGHT;

                PerfectMazeCheckCorner(left, overlayMap);
                PerfectMazeCheckCorner(right, overlayMap);
            }
            else //We're going left/right so need to check to/bottom corners 
            {
                Coord top = edge + Direction.UP;
                Coord bottom = edge + Direction.DOWN;

                PerfectMazeCheckCorner(top, overlayMap);
                PerfectMazeCheckCorner(bottom, overlayMap);
            }

            //We've carved the path now try again.
            RunPefectMaze(visted);

            //if we've gotten here we're bubbling back up
        }

        //Check if cell has three or more open edges
        void PerfectMazeCheckCorner(Coord corner, ArrayMap<int> overlayMap)
        {
            int openEdgeCount = 0;

            //Check for possible moves
            Coord[] edges = new Coord[4];
            edges[0] = corner + Direction.UP;
            edges[1] = corner + Direction.DOWN;
            edges[2] = corner + Direction.LEFT;
            edges[3] = corner + Direction.RIGHT;

            foreach (Coord coord in edges)
            {
                //Check bounds
                if (BoundsCheck(coord))
                {
                    continue;
                }

                //Check if it's been carved 
                if (overlayMap[coord] == 1)
                {
                    openEdgeCount += 1;
                }

            }

            //If there are more than three open edgeds we set the corner to open as well 
            if (openEdgeCount >= 3)
            {
                overlayMap[corner] = 1;

                foreach (Coord coord in edges)
                {
                    overlayMap[coord] = 1;
                }
            }
        }

        ArrayMap<bool> BuildWalkMap()
        {
            ArrayMap<bool> walkMap = new ArrayMap<bool>(size, size);


            for (int x = 0; x < walkMap.Width; x++)
            {
                for (int y = 0; y < walkMap.Height; y++)
                {
                    walkMap[x, y] = true;
                }
            }

            for (int x = 0; x < indexMap.Width; x++)
            {
                for (int y = 0; y < indexMap.Height; y++)
                {
                    int index = indexMap[x, y];
                    int xo = (x * tileSize) + (tileSize / 2);
                    int yo = (y * tileSize) + (tileSize / 2);

                    //If we're overlaying tiles then we print them one up and back
                    xo = overLap == true ? xo - x : xo;
                    yo = overLap == true ? yo - y : yo;

                    PrintTile(walkMap, index, xo, yo);
                }
            }

            return walkMap;
        }

        void PrintTile(ArrayMap<bool> walkMap, int index, int xo, int yo)
        {
            List<int[,]> tiles = blob.GetValueOrDefault(index);
            if (tiles == null)
                return;

            //Randomly pick a tile from the list
            //These could be weighted, down the line I might want this to take into account any preivosyly placed tiles so it might add someting related 
            int r = GoRogue.Random.SingletonRandom.DefaultRNG.Next(0, tiles.Count());

            int[,] tile = tiles[r];

            int halfTileSize = (tileSize / 2);

            if (tile != null)
            {
                for (int x = 0; x < tileSize; x++)
                {
                    for (int y = 0; y < tileSize; y++)
                    {
                        int xt = x - halfTileSize;
                        int yt = y - halfTileSize;

                        //If we're stamping over tiles should we respect already placed walls?
                        //For now we will 
                        if (!walkMap[xo + xt, yo + yt])
                        {
                            continue;
                        }

                        //y,x here is a little fucky feeling, must have gotten mixed up somewhere
                        walkMap[xo + xt, yo + yt] = tile[y, x] == 1 ? true : false;

                    }

                }
            }
        }

        void FloodWalls(ArrayMap<Cell> tiles)
        {
            for (int x = 0; x < tiles.Width; x++)
            {
                for (int y = 0; y < tiles.Height; y++)
                {
                    tiles[x, y] = new Cell(Color.Gray, Color.Black, '#');
                }

            }
        }

        bool BoundsCheck(Coord check)
        {
            return check.X < 0 || check.Y < 0 || check.X >= overlayMap.Width || check.Y >= overlayMap.Height;
        }
    }
}
