using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


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
        public Color playerColor = Color.black;
        public Color appleColor = Color.red;

        //map necessities
        public Node[,] grid;
        private GameObject mapObject;
        private SpriteRenderer mapRenderer;
        private List<Node> availableNodes = new List<Node>();

        // direction enum
        public enum Direction { up,down,left,right};
        Direction targetDirection;
        Direction currDirection;
        bool up, down, left, right;

        //player
        Node playerNode;
        GameObject playerObject;
        Sprite playerSprite;

        //Apple
        Node appleNode;
        GameObject appleObj;

        //Camera
        public Transform cameraHolder;

        //timer 
        float timer;
        public float moveRate = 0.5f;

        //score
        int currScore;
        int HighScore;
        public Text currentScoreText;
        public Text HighScoreText;

        //tail
        List<SpecialNode> tail = new List<SpecialNode>();
        GameObject tailparent;

        //Events
        public UnityEvent onScore;
        public UnityEvent onStart;
        public UnityEvent onGameOver;
        public UnityEvent onFirstInput;
        public bool isGameOver;
        public bool isFirstInput;


        #region Init
        private void Start()
        {
            onStart.Invoke();
        }

        public void StartNewGame()
        {
            ClearReferences();
            CreateMap();
            PlacePlayer();
            PlaceCamera();
            CreateApple();
            targetDirection = Direction.right;
            isGameOver = false;
            currScore = 0;

            UpdateScoreText();
        }

        void ClearReferences()
        {
            if(mapObject !=null)    
                Destroy(mapObject);
            if (playerObject != null)
                Destroy(playerObject);
            if (appleObj != null)
                Destroy(appleObj);

            foreach (var item in tail)
            {
                if(item.obj != null)
                Destroy(item.obj);
            }
            tail.Clear();
            availableNodes.Clear();
            grid = null;
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
            playerSprite = CreateSprite(playerColor);
            playerRenderer.sprite = playerSprite;
            playerRenderer.sortingOrder = 1;
            int nodeX = Random.Range(0, maxwidth - 1);
            int nodeY = Random.Range(0, maxHeight - 1);
            playerNode = GetNode(nodeX, nodeY);

            PlacePlayerObject(playerObject, playerNode.worldPosition);
            playerObject.transform.localScale = Vector3.one *1.2f;

            tailparent = new GameObject("Tail Parent");            
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
            appleRenderer.sprite = CreateSprite(appleColor);
            appleRenderer.sortingOrder = 1;
            PlaceApple();
        }
        #endregion

        #region Updates

        private void Update()
        {
            if (isGameOver)
            {
                if (Input.GetKeyDown(KeyCode.R))
                {
                    onStart.Invoke();
                }
                return;
            }
            
            GetInput();
           
            if (isFirstInput)
            {
                SetPlayerDirection();

                timer += Time.deltaTime;
                if (timer >= moveRate)
                {
                    timer = 0;
                    currDirection = targetDirection;
                    MovePlayer();
                }
            }
            else
            {
                if (up || down || left || right)
                {
                    onFirstInput.Invoke();
                    isFirstInput = true;
                }
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
                SetDirection(Direction.up);
            }
            else if (down)
            {
                SetDirection(Direction.down);
            }
            else if (left)
            {
                SetDirection(Direction.left);
            }
            else if (right)
            {
                SetDirection(Direction.right);
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
                onGameOver.Invoke();
                //gameover
            }
            else
            {
                if (IsTailNode(targetNode))
                {
                    onGameOver.Invoke();
                    //gameover
                }
                else
                {
                    bool isScore = false;

                    if (targetNode == appleNode)
                    {
                        isScore = true;
                    }

                    Node previousNode = playerNode;
                    availableNodes.Add(previousNode);


                    if (isScore)
                    {
                        tail.Add(CreateTailNode(previousNode.x, previousNode.y));
                        availableNodes.Remove(previousNode);
                    }


                    //move tail
                    MoveTail();

                    PlacePlayerObject(playerObject, targetNode.worldPosition);
                    playerNode = targetNode;
                    availableNodes.Remove(playerNode);

                    if (isScore)
                    {
                        currScore++;
                        if(currScore >= HighScore)
                        {
                            HighScore = currScore;
                        }

                        onScore.Invoke();

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
        }

        void MoveTail()
        {
            Node prevNode = null;

            for(int i = 0; i < tail.Count; i++)
            {
                SpecialNode p = tail[i];
                availableNodes.Add(p.node);

                if (i == 0)
                {
                    prevNode = p.node;
                    p.node = playerNode;
                }
                else
                {
                    Node prev = p.node;
                    p.node = prevNode;
                    prevNode = prev;
                }

                availableNodes.Remove(p.node);
                PlacePlayerObject(p.obj, p.node.worldPosition);
                

            }
        }

        #endregion

        #region Utilities

        public void UpdateScoreText()
        {
            currentScoreText.text = "Current Score : "+currScore.ToString();
            HighScoreText.text = "High Score : " + HighScore.ToString();
        }

        public void GameOver()
        {
            isGameOver = true;
            isFirstInput = false;
        }

        private void SetDirection(Direction d)
        {
            if (!OppositeDirection(d))
                targetDirection = d;
        }

        bool OppositeDirection(Direction d)
        {
            switch (d)
            {
                default: 

                case Direction.up:

                    if (currDirection == Direction.down)
                        return true;
                    else
                        return false;
                    
                case Direction.down:

                    if (currDirection == Direction.up)
                        return true;
                    else
                        return false;

                case Direction.left:

                    if (currDirection == Direction.right)
                        return true;
                    else
                        return false;

                case Direction.right:

                    if (currDirection == Direction.left)
                        return true;
                    else
                        return false;
            }
        }

        void PlacePlayerObject(GameObject obj, Vector3 pos)
        {
            pos += Vector3.one * .5f;
            obj.transform.position = pos;
        }

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

            return Sprite.Create(txt, rect, Vector2.one * 0.5f , 1, 0, SpriteMeshType.FullRect);
        }

        void PlaceApple()
        {
            int ran = Random.Range(0, availableNodes.Count);
            Node n = availableNodes[ran];
            PlacePlayerObject(appleObj, n.worldPosition);
            
            appleNode = n;
        }

        SpecialNode CreateTailNode(int x,int y)
        {
            SpecialNode s = new SpecialNode();
            s.node = GetNode(x, y);
            s.obj = new GameObject();
            s.obj.transform.parent = tailparent.transform;
            s.obj.transform.position = s.node.worldPosition;
            s.obj.transform.localScale = Vector3.one * 0.95f;

            SpriteRenderer sr = s.obj.AddComponent<SpriteRenderer>();
            sr.sprite = playerSprite;
            sr.sortingOrder = 1;


            return s;
        }

        bool IsTailNode(Node n)
        {
            for(int i = 0; i < tail.Count; i++)
            {
                if (tail[i].node == n)
                    return true;
            }

            return false;
        }
        #endregion

    }
}