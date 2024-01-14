using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardCell : MonoBehaviour
{
    public int mineNum;
    public int cellType;
    public int gridIndex;
    private int xPos;
    private int yPos;
    public bool revealed;
    public bool locked;
    public bool update;

    public SpriteRenderer render;
    public Sprite defaultCell;
    public Sprite flag;
    public Sprite block_1;
    public Sprite block_2;
    public Sprite block_3;
    public Sprite block_4;
    public Sprite block_5;
    public Sprite block_6;
    public Sprite block_7;
    public Sprite block_8;

    public List<GameObject> board;
    public GameHandler combat;

    public PlayerStats player;

    // Start is called before the first frame update
    void Start()
    {
        mineNum = 0;
        cellType = 0;
        gridIndex = 0;
        xPos = 0;
        yPos = 0;
        revealed = false;
        locked = false;
        update = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (update)
        {
            xPos = gridIndex % 8;
            yPos = gridIndex / 8;
            if (transform.position.x != xPos || transform.position.y != -yPos)
            {
                transform.position = new Vector3(xPos, -yPos, transform.position.z);
            }

            if (locked)
            {
                render.material.color = new Vector4(255f, 255f, 255f, 255f);
                render.sprite = flag;
            }
            else if (revealed)
            {
                switch (cellType)
                {
                    case 0:
                        render.sprite = defaultCell;
                        render.material.color = new Vector4(255f, 0f, 0f, 255f);
                        break;

                    /*case 1:
                        render.material.color = new Vector4(0f, 255f, 0f, 255f);
                        break;

                    case 2:
                        render.material.color = new Vector4(0f, 0f, 255f, 255f);
                        break;*/

                    default:
                        render.material.color = new Vector4(255f, 255f, 255f, 255f);
                        blockDisplay();
                        break;
                }
            }
            else
            {
                render.sprite = defaultCell;
                render.material.color = new Vector4(100f, 100f, 100f, 255f);
            }

            neighbor2();
        }

    }

    void blockDisplay()
    {
        switch (mineNum)
        {
            case 1:
                render.sprite = block_1;
                break;

            case 2:
                render.sprite = block_2;
                break;

            case 3:
                render.sprite = block_3;
                break;

            case 4:
                render.sprite = block_4;
                break;

            case 5:
                render.sprite = block_5;
                break;

            case 6:
                render.sprite = block_6;
                break;

            case 7:
                render.sprite = block_7;
                break;

            case 8:
                render.sprite = block_8;
                break;

            default:
                render.sprite = defaultCell;
                render.material.color = new Vector4(0f, 255f, 0f, 255f);
                break;
        }
    }

    void OnMouseOver()
    {
        if(Input.GetMouseButtonDown(0) && !locked && !revealed && player.moves > 0)
        {
            player.move();
            reveal();
        }

        if (Input.GetMouseButtonDown(1) && !revealed)
        {
            if (!locked)
            {
                locked = true; 
            }
            else
            {
                locked = false;
            }
        }

        /*if (Input.GetMouseButtonDown(2))
        {
            if (cellType == 0)
            {
                cellType = 1;
            }
            else
            {
                cellType = 0;
            }
        }*/
    }

    /*void OnMouseExit()
    {
        revealed = false;
    }*/

    void neighbor2()
    {
        mineNum = 0;

        if(gridIndex == 0)
        {
            mineNum += mineCheck(board[gridIndex + 1]);
            mineNum += mineCheck(board[gridIndex + 8]);
            mineNum += mineCheck(board[gridIndex + 9]);
        }

        else if(gridIndex == 7)
        {
            mineNum += mineCheck(board[gridIndex - 1]);
            mineNum += mineCheck(board[gridIndex + 8]);
            mineNum += mineCheck(board[gridIndex + 7]);
        }

        else if(gridIndex == 56)
        {
            mineNum += mineCheck(board[gridIndex + 1]);
            mineNum += mineCheck(board[gridIndex - 8]);
            mineNum += mineCheck(board[gridIndex - 7]);
        }

        else if(gridIndex == 63)
        {
            mineNum += mineCheck(board[gridIndex - 1]);
            mineNum += mineCheck(board[gridIndex - 8]);
            mineNum += mineCheck(board[gridIndex - 9]);
        }

        else if(xPos == 0 && yPos != 0)
        {
            mineNum += mineCheck(board[gridIndex + 1]);
            mineNum += mineCheck(board[gridIndex + 8]);
            mineNum += mineCheck(board[gridIndex + 9]);
            mineNum += mineCheck(board[gridIndex - 7]);
            mineNum += mineCheck(board[gridIndex - 8]);
        }

        else if (xPos == 7)
        {
            mineNum += mineCheck(board[gridIndex - 1]);
            mineNum += mineCheck(board[gridIndex + 8]);
            mineNum += mineCheck(board[gridIndex - 9]);
            mineNum += mineCheck(board[gridIndex + 7]);
            mineNum += mineCheck(board[gridIndex - 8]);
        }

        else if (yPos == 0 && xPos != 0)
        {
            mineNum += mineCheck(board[gridIndex + 1]);
            mineNum += mineCheck(board[gridIndex -1]);
            mineNum += mineCheck(board[gridIndex + 9]);
            mineNum += mineCheck(board[gridIndex + 7]);
            mineNum += mineCheck(board[gridIndex + 8]);
        }

        else if (yPos == 7)
        {
            mineNum += mineCheck(board[gridIndex + 1]);
            mineNum += mineCheck(board[gridIndex - 1]);
            mineNum += mineCheck(board[gridIndex - 9]);
            mineNum += mineCheck(board[gridIndex - 7]);
            mineNum += mineCheck(board[gridIndex - 8]);
        }

        else if(gridIndex != 0)
        {
            mineNum += mineCheck(board[gridIndex + 1]);
            mineNum += mineCheck(board[gridIndex + 8]);
            mineNum += mineCheck(board[gridIndex + 9]);
            mineNum += mineCheck(board[gridIndex + 7]);
            mineNum += mineCheck(board[gridIndex - 7]);
            mineNum += mineCheck(board[gridIndex - 1]);
            mineNum += mineCheck(board[gridIndex - 8]);
            mineNum += mineCheck(board[gridIndex - 9]);
        }
    }

    int mineCheck(GameObject cell2)
    {
        if(cell2.gameObject.GetComponent<BoardCell>().cellType == 0)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }

    public void reveal()
    {
        if (!combat.boardStart)
        {
            cellType = 1;
            combat.boardStart = true;
        }

        if (!revealed)
        {
            locked = false;
            revealed = true;

            if (cellType != 0)
            {
                player.gainEnergy(1);
                combat.cellAttack();

                if (mineNum == 0)
                {
                    if (gridIndex == 0)
                    {
                        board[gridIndex + 1].gameObject.GetComponent<BoardCell>().reveal();
                        board[gridIndex + 8].gameObject.GetComponent<BoardCell>().reveal();
                        board[gridIndex + 9].gameObject.GetComponent<BoardCell>().reveal();
                    }

                    else if (gridIndex == 7)
                    {
                        board[gridIndex - 1].gameObject.GetComponent<BoardCell>().reveal();
                        board[gridIndex + 8].gameObject.GetComponent<BoardCell>().reveal();
                        board[gridIndex + 7].gameObject.GetComponent<BoardCell>().reveal();
                    }

                    else if (gridIndex == 56)
                    {
                        board[gridIndex + 1].gameObject.GetComponent<BoardCell>().reveal();
                        board[gridIndex - 8].gameObject.GetComponent<BoardCell>().reveal();
                        board[gridIndex - 7].gameObject.GetComponent<BoardCell>().reveal();
                    }

                    else if (gridIndex == 63)
                    {
                        board[gridIndex - 1].gameObject.GetComponent<BoardCell>().reveal();
                        board[gridIndex - 8].gameObject.GetComponent<BoardCell>().reveal();
                        board[gridIndex - 9].gameObject.GetComponent<BoardCell>().reveal();
                    }

                    else if (xPos == 0 && yPos != 0)
                    {
                        board[gridIndex + 1].gameObject.GetComponent<BoardCell>().reveal();
                        board[gridIndex + 8].gameObject.GetComponent<BoardCell>().reveal();
                        board[gridIndex + 9].gameObject.GetComponent<BoardCell>().reveal();
                        board[gridIndex - 7].gameObject.GetComponent<BoardCell>().reveal();
                        board[gridIndex - 8].gameObject.GetComponent<BoardCell>().reveal();
                    }

                    else if (xPos == 7)
                    {
                        board[gridIndex - 1].gameObject.GetComponent<BoardCell>().reveal();
                        board[gridIndex + 8].gameObject.GetComponent<BoardCell>().reveal();
                        board[gridIndex - 9].gameObject.GetComponent<BoardCell>().reveal();
                        board[gridIndex + 7].gameObject.GetComponent<BoardCell>().reveal();
                        board[gridIndex - 8].gameObject.GetComponent<BoardCell>().reveal();
                    }

                    else if (yPos == 0 && xPos != 0)
                    {
                        board[gridIndex + 1].gameObject.GetComponent<BoardCell>().reveal();
                        board[gridIndex - 1].gameObject.GetComponent<BoardCell>().reveal();
                        board[gridIndex + 9].gameObject.GetComponent<BoardCell>().reveal();
                        board[gridIndex + 7].gameObject.GetComponent<BoardCell>().reveal();
                        board[gridIndex + 8].gameObject.GetComponent<BoardCell>().reveal();
                    }

                    else if (yPos == 7)
                    {
                        board[gridIndex + 1].gameObject.GetComponent<BoardCell>().reveal();
                        board[gridIndex - 1].gameObject.GetComponent<BoardCell>().reveal();
                        board[gridIndex - 9].gameObject.GetComponent<BoardCell>().reveal();
                        board[gridIndex - 7].gameObject.GetComponent<BoardCell>().reveal();
                        board[gridIndex - 8].gameObject.GetComponent<BoardCell>().reveal();
                    }

                    else if (gridIndex != 0)
                    {
                        board[gridIndex + 1].gameObject.GetComponent<BoardCell>().reveal();
                        board[gridIndex + 8].gameObject.GetComponent<BoardCell>().reveal();
                        board[gridIndex + 9].gameObject.GetComponent<BoardCell>().reveal();
                        board[gridIndex + 7].gameObject.GetComponent<BoardCell>().reveal();
                        board[gridIndex - 7].gameObject.GetComponent<BoardCell>().reveal();
                        board[gridIndex - 1].gameObject.GetComponent<BoardCell>().reveal();
                        board[gridIndex - 8].gameObject.GetComponent<BoardCell>().reveal();
                        board[gridIndex - 9].gameObject.GetComponent<BoardCell>().reveal();
                    }
                }
            }

            else if (cellType == 0)
            {
                combat.detonate();
            }
        }
    }

    public void checkReveal()
    {
        if (cellType != 0 && mineNum == 0)
        {
            if (gridIndex == 0)
            {
                board[gridIndex + 1].gameObject.GetComponent<BoardCell>().reveal();
                board[gridIndex + 8].gameObject.GetComponent<BoardCell>().reveal();
                board[gridIndex + 9].gameObject.GetComponent<BoardCell>().reveal();
            }

            else if (gridIndex == 7)
            {
                board[gridIndex - 1].gameObject.GetComponent<BoardCell>().reveal();
                board[gridIndex + 8].gameObject.GetComponent<BoardCell>().reveal();
                board[gridIndex + 7].gameObject.GetComponent<BoardCell>().reveal();
            }

            else if (gridIndex == 56)
            {
                board[gridIndex + 1].gameObject.GetComponent<BoardCell>().reveal();
                board[gridIndex - 8].gameObject.GetComponent<BoardCell>().reveal();
                board[gridIndex - 7].gameObject.GetComponent<BoardCell>().reveal();
            }

            else if (gridIndex == 63)
            {
                board[gridIndex - 1].gameObject.GetComponent<BoardCell>().reveal();
                board[gridIndex - 8].gameObject.GetComponent<BoardCell>().reveal();
                board[gridIndex - 9].gameObject.GetComponent<BoardCell>().reveal();
            }

            else if (xPos == 0 && yPos != 0)
            {
                board[gridIndex + 1].gameObject.GetComponent<BoardCell>().reveal();
                board[gridIndex + 8].gameObject.GetComponent<BoardCell>().reveal();
                board[gridIndex + 9].gameObject.GetComponent<BoardCell>().reveal();
                board[gridIndex - 7].gameObject.GetComponent<BoardCell>().reveal();
                board[gridIndex - 8].gameObject.GetComponent<BoardCell>().reveal();
            }

            else if (xPos == 7)
            {
                board[gridIndex - 1].gameObject.GetComponent<BoardCell>().reveal();
                board[gridIndex + 8].gameObject.GetComponent<BoardCell>().reveal();
                board[gridIndex - 9].gameObject.GetComponent<BoardCell>().reveal();
                board[gridIndex + 7].gameObject.GetComponent<BoardCell>().reveal();
                board[gridIndex - 8].gameObject.GetComponent<BoardCell>().reveal();
            }

            else if (yPos == 0 && xPos != 0)
            {
                board[gridIndex + 1].gameObject.GetComponent<BoardCell>().reveal();
                board[gridIndex - 1].gameObject.GetComponent<BoardCell>().reveal();
                board[gridIndex + 9].gameObject.GetComponent<BoardCell>().reveal();
                board[gridIndex + 7].gameObject.GetComponent<BoardCell>().reveal();
                board[gridIndex + 8].gameObject.GetComponent<BoardCell>().reveal();
            }

            else if (yPos == 7)
            {
                board[gridIndex + 1].gameObject.GetComponent<BoardCell>().reveal();
                board[gridIndex - 1].gameObject.GetComponent<BoardCell>().reveal();
                board[gridIndex - 9].gameObject.GetComponent<BoardCell>().reveal();
                board[gridIndex - 7].gameObject.GetComponent<BoardCell>().reveal();
                board[gridIndex - 8].gameObject.GetComponent<BoardCell>().reveal();
            }

            else if (gridIndex != 0)
            {
                board[gridIndex + 1].gameObject.GetComponent<BoardCell>().reveal();
                board[gridIndex + 8].gameObject.GetComponent<BoardCell>().reveal();
                board[gridIndex + 9].gameObject.GetComponent<BoardCell>().reveal();
                board[gridIndex + 7].gameObject.GetComponent<BoardCell>().reveal();
                board[gridIndex - 7].gameObject.GetComponent<BoardCell>().reveal();
                board[gridIndex - 1].gameObject.GetComponent<BoardCell>().reveal();
                board[gridIndex - 8].gameObject.GetComponent<BoardCell>().reveal();
                board[gridIndex - 9].gameObject.GetComponent<BoardCell>().reveal();
            }
        }
    }
}
