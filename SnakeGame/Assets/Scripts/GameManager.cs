using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SA
{
    public class GameManager : MonoBehaviour
    {
        // map size
        public int maxHeight = 16;
        public int maxwidth = 17;

        // colors player and map
        public Color color1;
        public Color color2;
        public Color player = Color.black;
        public Color apple = Color.red;

        //map necessities
        public Node[,] grid;
        private GameObject mapObject;
        private SpriteRenderer mapRenderer;
        private List<Node> availableNodes = new List<Node>();

        // direction enum
        public enum Direction { up,down,left,right};
        Direction currDirection;
        bool up, down, left, right;

        //player
        Node playerNode;
        GameObject playerObject;

        //Apple
        Node appleNode;
        GameObject appleObj;

        //Camera
        public Transform cameraHolder;

        //timer 
        float timer;
        public float moveRate = 0.5f;


        #region Init
        private void Start()
        {
            CreateMap();
            PlacePlayer();
            PlaceCamera();
            CreateApple();
        }

        private void CreateMap()
        {
            mapObject = new GameObject("MapObject");
            mapRenderer = mapObject.AddComponent<SpriteRenderer>();

            grid = new Node[maxwidth, maxHeight];

            Texture2D txt = new Texture2D(maxwidth,maxHeight);
            for(int x = 0; x < maxwidth; x++)
            {
                for(int y=0 ; y<maxHeight ; y++)
                {
                    #region visual
                    if ( x % 2 != 0)
                    {
                       if( y % 2 !=0)
                       {
                            txt.SetPixel(x, y, color1);
                       }
                       else
                       {
                            txt.SetPixel(x, y, color2);
                       }
                    }
                    else
                    {
                       if (y % 2 != 0)
                       {
                           txt.SetPixel(x, y, color2);
                       }
                       else
                       {
                           txt.SetPixel(x, y, color1);
                       }
                    }
                    #endregion
                    Vector3 tp = Vector3.zero;
                    tp.x = x;
                    tp.y = y;

                    Node n = new Node() { 
                        x = x, 
                        y = y,
                        worldPosition = tp};

                    grid[x, y] = n;

                    availableNodes.Add(n);
                }
            }
            txt.filterMode = FilterMode.Point;
            txt.Apply();

            Rect rec = new Rect(0, 0, maxwidth, maxHeight);
            Sprite sprite = Sprite.Create(txt, rec, Vector2.zero ,1,0,SpriteMeshType.FullRect);
            mapRenderer.sprite = sprite;
        }

        private void PlacePlayer()
        {
            playerObject = new GameObject("Player");
            SpriteRenderer playerRenderer = playerObject.AddComponent<SpriteRenderer>();
            playerRenderer.sprite = CreateSprite(player);
            playerRenderer.sortingOrder = 1;
            playerNode = GetNode(3, 3);
            playerObject.transform.position = playerNode.worldPosition;
            
        }

        void PlaceCamera()
        {
            Node n = GetNode(maxwidth / 2, maxHeight / 2);
            Vector3 p = n.worldPosition;
            p += Vector3.one * 0.5f;

            cameraHolder.position = p;
        }

        void CreateApple()
        {
            appleObj = new GameObject("Apple");
            SpriteRenderer appleRenderer = appleObj.AddComponent<SpriteRenderer>();
            appleRenderer.sprite = CreateSprite(apple);
            appleRenderer.sortingOrder = 1;
            PlaceApple();
        }
        #endregion

        #region Updates

        private void Update()
        {
            GetInput();
            SetPlayerDirection();

            timer += Time.deltaTime;
            if( timer >= moveRate)
            {
                timer = 0;
                MovePlayer();
            }
        }

        private void GetInput()
        {
            up = Input.GetButtonDown("Up");
            down = Input.GetButtonDown("Down");
            left = Input.GetButtonDown("Left");
            right = Input.GetButtonDown("Right");
        }

        private void SetPlayerDirection()
        {
            if (up)
            {
                currDirection = Direction.up;  
            }
            else if (down)
            {
                currDirection = Direction.down;
            }
            else if (left)
            {
                currDirection = Direction.left;
            }
            else if (right)
            {
                currDirection = Direction.right;
            }
        }

        private void MovePlayer()
        {
 
            int x = 0;
            int y = 0;
            switch (currDirection)
            {
                case Direction.up:
                    y = 1;
                    break;
                case Direction.down:
                    y = -1;
                    break;
                case Direction.left:
                    x = -1;
                    break;
                case Direction.right:
                    x = 1;
                    break;
            }

            Node targetNode = GetNode(playerNode.x + x, playerNode.y + y);
            if(targetNode == null)
            {
                //gameover
            }
            else
            {
                bool isScore = false;

                if(targetNode == appleNode)
                {
                    isScore = true;
                }

                availableNodes.Remove(playerNode);
                playerObject.transform.position = targetNode.worldPosition;
                playerNode = targetNode;
                availableNodes.Add(playerNode);

                //move tail

                if (isScore)
                {
                    if (availableNodes.Count > 0)
                    {
                        PlaceApple();
                    }
                    else
                    {
                        //youve won
                    }
                }
            }
        }

        #endregion

        #region Utilities
        Node GetNode(int x,int y)
        {
            if (x < 0 || x > maxwidth - 1 || y < 0 || y > maxHeight - 1)
                return null;

            return grid[x, y];
        }

        Sprite CreateSprite(Color targetColor)
        {
            Texture2D txt = new Texture2D(1, 1);
            txt.SetPixel(0, 0, targetColor);
            txt.Apply();
            txt.filterMode = FilterMode.Point;
            Rect rect = new Rect(0, 0, 1, 1);

            return Sprite.Create(txt, rect, Vector2.zero , 1, 0, SpriteMeshType.FullRect);
        }

        void PlaceApple()
        {
            int ran = Random.Range(0, availableNodes.Count);
            Node n = availableNodes[ran];
            appleObj.transform.position = n.worldPosition;
            appleNode = n;
        }
        #endregion

    }
}