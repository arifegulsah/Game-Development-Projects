using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    [Range(5, 100)] //cok buyuk bir maze olmasini istemiom isteseydim 20'yi artir
    public int mazeWidth = 5, mazeHeight = 5; //0 0 olmasini engelle, dimension of maze
    public int startX, startY; //Algoritmanin calismaya baslayacagi koordinatlar. 0 0 demek sol asagi noktadna baslasin demek
    MazeCell[,] maze; //hucreler icin array

    Vector2Int currentCell; //o an icinde oldugumuz cell

    public MazeCell[,] GetMaze()
    {
        maze = new MazeCell[mazeWidth, mazeHeight]; //degerleri ve

        //icini doldur void birakma
        for(int x = 0; x < mazeWidth; x++)
        {
            for(int y = 0; y < mazeHeight; y++)
            {
                maze[x, y] = new MazeCell(x, y);
            }
        }

        CarvePath(startX, startY);

        return maze;
    }

    List<Direction> directions = new List<Direction>
    {
        Direction.Up, Direction.Down, Direction.Left, Direction.Right,
    };

    //directions isimli listimizden random return edecek funci
    //24 secenek var random birini donduruyor gunun sonunda
    List<Direction> GetRandomDirections()
    {
        //ana listenin kopyasý cunku analisteyi bozmak istemiyoruz
        List<Direction> dir = new List<Direction>(directions);

        //random secenekler geldiginde tutmak istedigimiz list
        List<Direction> rndDir = new List<Direction>();

        //dir den random bir eleman secip onu rndDir e koyup sonra siliyoruz
        while(dir.Count > 0)
        {
            int rnd = Random.Range(0, dir.Count);
            rndDir.Add(dir[rnd]);
            dir.RemoveAt(rnd);
        }

        return rndDir;
    }
    
    //visited oldu mu olmadi mi kontrol
    bool IsCellValid(int x, int y)
    {
        if (x < 0 || y < 0 || x > mazeWidth - 1 || y > mazeHeight - 1 || maze[x, y].visited) return false;
        else return true;
    }

    Vector2Int CheckNeighbours()
    {
        List<Direction> rndDir = GetRandomDirections();

        for(int i = 0; i < rndDir.Count; i++)
        {
            Vector2Int neighbour = currentCell;

            switch (rndDir[i])
            {
                case Direction.Up:
                    neighbour.y++;
                    break;
                case Direction.Down:
                    neighbour.y--;
                    break;
                case Direction.Right:
                    neighbour.x++;
                    break;
                case Direction.Left:
                    neighbour.x--;
                    break;
            }

            if (IsCellValid(neighbour.x, neighbour.y)) return neighbour;
        }

        return currentCell;
    }

    void BreakWalls(Vector2Int primaryCell, Vector2Int secondaryCell)
    {
        if(primaryCell.x > secondaryCell.x) //1. hucre sagda
        {
            maze[primaryCell.x, primaryCell.y].leftWall = false;
        }
        else if(primaryCell.x < secondaryCell.x) //1. hucre sol tarafta
        {
            maze[secondaryCell.x, secondaryCell.y].leftWall = false;
        }
        else if (primaryCell.y < secondaryCell.y) //1. hucre asagida
        {
            maze[primaryCell.x, primaryCell.y].topWall = false;
        }
        else if (primaryCell.y > secondaryCell.y) //1. hucre yukarida
        {
            maze[secondaryCell.x, secondaryCell.y].topWall = false;
        }
    }

    //x ve y degerlerini ekle, "deadend" bulana kadar mazede ilerle
    //dead end den kasit komsuus olmayan noklata yani cikmaz sokak
    void CarvePath(int x, int y)
    {
        //baslangic noktalarimiz haritanin icinde mi disinda mi kaliyo kontrol
        //eger uymuyorsa 0 0 default deger ata
        if( x < 0 || y < 0 || x > mazeWidth -1 || y > mazeHeight - 1)
        {
            x = y = 0;
            Debug.LogWarning("Baslangic noktasi gecersiz. 0 , 0 noktasina ayarlaniyor.");
        }

        //currentcelli baslama noktamiz olarak ayarlayalim
        currentCell = new Vector2Int(x, y);

        //backtacking yaparken kullanacagimiz liste. gectigimiz yollari icerisinde tutacak.
        List<Vector2Int> path = new List<Vector2Int>();

        //dead end bulana kadar dongu devam etsin
        bool deadEnd = false;
        while (!deadEnd)
        {
            //ilerlemye calisalim bir sonraki hucreye dogru
            Vector2Int nextCell = CheckNeighbours();

            //eger gecerli bir komsu hucre yoksa dead end demek 
            if(nextCell == currentCell)
            {
                for(int i = path.Count -1; i>=0; i--)
                {
                    currentCell = path[i];
                    path.RemoveAt(i);
                    nextCell = CheckNeighbours();

                    if (nextCell != currentCell) break;
                }

                if (nextCell == currentCell) deadEnd = true;
            }
            else
            {
                BreakWalls(currentCell, nextCell);
                maze[currentCell.x, currentCell.y].visited = true;
                currentCell = nextCell;
                path.Add(currentCell);
            }
        }
    }


}

public enum Direction
{
    Up,
    Down,
    Left,
    Right
}

//her bir mazecellin sahsi verisini tutmak için olusturdugumuz class.
public class MazeCell
{
    public bool visited; //hücre ziyaret edildi mi edilmedi mi
    public int x, y; //lokasyonlar 

    public bool topWall;
    public bool leftWall;

    //VEctor2Int tipinde x ve yleri döndür
    public Vector2Int position
    {
        get
        {
            return new Vector2Int(x, y);
        }
    }

    //consructor
    public MazeCell(int x, int y)
    {
        //Bu hücrenin labirentteki kordinatlarý
        this.x = x;
        this.y = y;

        //defualt olarak not visited
        visited = false;

        //Butun duvarlar algortima onlarý yýkana kadar aktif
        topWall = leftWall = true;
    }
}