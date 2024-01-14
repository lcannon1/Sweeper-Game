using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameHandler : MonoBehaviour
{
    public PlayerStats player;
    public GameObject Square;
    public GameObject square;
    public List<GameObject> board = new List<GameObject>();
    public bool boardStart = false;
    public int gameLevel;
    private bool shuffleTrigger = false;
    private bool boardCreated = false;
    private bool boardChecked = true;
    //private int numMines = 0;
    private int mineMax = 10;
    private int currCellType;
    private int combatType;

    public GameObject enemy;
    public GameObject Enemy;
    public GameObject combatPanel;
    public GameObject merchantPanel;
    public GameObject restSpotPanel;
    public GameObject map;
    public GameObject startButton;
    public GameObject victory;
    public GameObject defeat;

    public List<GameObject> enemyList;
    public List<GameObject> combatList;

    System.Random random = new System.Random();

    public void quitGame()
    {
        Application.Quit();
    }

    // Start is called before the first frame update
    void Start()
    {
        gameLevel = 1;

        for(int i = 0; i < 64; i++)
        {
            square = Instantiate(Square);
            square.gameObject.GetComponent<BoardCell>().player = player;
            square.gameObject.GetComponent<BoardCell>().combat = this;
            board.Add(square);
        }

        initHide();
    }

    void initHide()
    {
        for (int i = 0; i < board.Count; i++)
            {
                board[i].gameObject.GetComponent<SpriteRenderer>().enabled = false;
            }
    }

    // Update is called once per frame
    void Update()
    {
        if (!boardCreated)
        {
            createBoard();
            boardCreated = true;
            boardStart = false;
        }

        if (!boardChecked)
        {
            checkBoard();
            boardChecked = true;
        }


        /*if (Input.GetKeyDown(KeyCode.Space))
        {
            createBoard();
        }*/

        if (shuffleTrigger)// || Input.GetKeyDown(KeyCode.R))
        {
            shuffleTrigger = false;
            shuffle();
        }

        if (player.health == 0)
        {
            for (int i = 0; i < board.Count; i++)
            {
                board[i].SetActive(false);
            }
            combatPanel.SetActive(false);
            defeat.SetActive(true);

            while (combatList.Count > 0)
            {
                combatList.RemoveAt(0);
            }

            player.healPlayer(1);
        }
    }

    public void createBoard()
    {
        //numMines = 0;
        for (int i = 0; i < board.Count; i++)
        {
            /*currCellType = random.Next(0, 3);
            if (currCellType == 0)
            {
                if (numMines == mineMax)
                {
                    currCellType = random.Next(1, 3);
                }
                else
                {
                    numMines += 1;
                    print(numMines);
                }
            }

            board[i].gameObject.GetComponent<BoardCell>().cellType = random.Next(0, 3);*/

            if (i < mineMax)
            {
                board[i].gameObject.GetComponent<BoardCell>().cellType = 0;
            }
            else
            {
                board[i].gameObject.GetComponent<BoardCell>().cellType = 1;
            }

            board[i].gameObject.GetComponent<BoardCell>().gridIndex = i;
            board[i].gameObject.GetComponent<BoardCell>().board = board;
            board[i].gameObject.GetComponent<BoardCell>().revealed = false;
            board[i].gameObject.GetComponent<BoardCell>().locked = false;
            board[i].gameObject.GetComponent<BoardCell>().update = true;
        }
        shuffle();
    }

    private void checkBoard()
    {
        for (int i = 0; i < board.Count; i++)
        {
            if (board[i].gameObject.GetComponent<BoardCell>().cellType != 0
                && board[i].gameObject.GetComponent<BoardCell>().mineNum == 0
                && board[i].gameObject.GetComponent<BoardCell>().revealed == true)
            {
                board[i].gameObject.GetComponent<BoardCell>().checkReveal();
            }
        }
    }

    public void shuffle()
    {
        int n = board.Count;
        while (n > 1)
        {
            n--;
            int k = random.Next(n + 1);
            GameObject value = board[k];
            board[k] = board[n];
            board[n] = value;
        }

        for (int i = 0; i < board.Count; i++)
        {
            board[i].gameObject.GetComponent<BoardCell>().gridIndex = i;
            boardChecked = false;
        }
    }

    public void triggerShuffle()
    {
        shuffleTrigger = true;
    }

    public void turn()
    {
        enemyTurn();
        player.newTurn();
    }

    public void StartCombat(int combat)
    {
        combatType = combat;

        map.SetActive(false);
        if (boardStart)
        {
            for (int i = 0; i < board.Count; i++)
            {
                board[i].SetActive(true);
            }

            createBoard();

            boardStart = false;
        }
        else
        {
            for (int i = 0; i < board.Count; i++)
            {
                board[i].gameObject.GetComponent<SpriteRenderer>().enabled = true;
            }
        }

        switch (gameLevel)
        {
            case 1:
                switch (combatType)
                {
                    case 1:
                        int numEnemies = random.Next(2, 4);
                        for (int i = 0; i < numEnemies; i++)
                        {
                            Enemy = Instantiate(enemy);
                            Enemy.gameObject.GetComponent<ThisBehavior>().combatList = this;
                            Enemy.gameObject.GetComponent<ThisBehavior>().player = player;
                            Enemy.gameObject.GetComponent<ThisBehavior>().index = combatList.Count;
                            combatList.Add(Enemy);
                        }
                        break;

                    case 2:
                        for (int i = 0; i < 4; i++)
                        {
                            Enemy = Instantiate(enemy);
                            Enemy.gameObject.GetComponent<ThisBehavior>().combatList = this;
                            Enemy.gameObject.GetComponent<ThisBehavior>().player = player;
                            Enemy.gameObject.GetComponent<ThisBehavior>().index = combatList.Count;
                            combatList.Add(Enemy);
                        }

                        //gameLevel++;
                        startButton.gameObject.GetComponent<Button>().enabled = true;
                        break;

                    default:
                        break;
                }
                break;

            default:
                for (int i = 0; i < 1; i++)
                {
                    Enemy = Instantiate(enemy);
                    Enemy.gameObject.GetComponent<ThisBehavior>().combatList = this;
                    Enemy.gameObject.GetComponent<ThisBehavior>().player = player;
                    Enemy.gameObject.GetComponent<ThisBehavior>().index = combatList.Count;
                    combatList.Add(Enemy);
                }
                break;
        }
        

        player.newTurn();
        player.drainEnergy();

        for (int i = 0; i < board.Count; i++)
        {
            board[i].SetActive(true);
            board[i].gameObject.GetComponent<SpriteRenderer>().enabled = true;
        }

        combatPanel.SetActive(true);
    }

    public void special(int specialType)
    {
        switch (specialType)
        {
            case 1:
                merchantPanel.SetActive(true);
                break;

            case 2:
                restSpotPanel.SetActive(true);
                break;
        }
    }

    public void enemyTurn()
    {
        foreach (var enemy in combatList)
        {
            enemy.GetComponent<TurnManager>().turn = true;
        }
    }

    public void enemyDeath(int index)
    {
        Enemy = combatList[index];
        combatList.RemoveAt(index);
        Destroy(Enemy);
        player.gainGold(random.Next(5, 10));
        for (int i = 0; i < combatList.Count; i++)
        {
            combatList[i].gameObject.GetComponent<ThisBehavior>().index = i;
        }
        if (combatList.Count == 0)
        {
            combatEnd();
        }
    }

    public void cellAttack()
    {
        if(combatList.Count > 0)
        {
            combatList[0].GetComponent<ThisBehavior>().damageEnemy(player.damage);
        }

    }

    public void skillAttack()
    {
        if (player.energy >= 5)
        {
            if (combatList.Count > 0)
            {
                combatList[0].GetComponent<ThisBehavior>().damageEnemy(10);
            }
            player.energy -= 5;
        }
    }

    public void cleaveAttack()
    {
        if (player.energy >= 10)
        {
            for (int i = combatList.Count - 1; i >= 0; i--)
            {
                combatList[i].GetComponent<ThisBehavior>().damageEnemy(5);
            }
            player.energy -= 5;
        }
    }

    public void healSkill()
    {
        if (player.energy >= 5)
        {

            player.healPlayer(10);
            player.energy -= 5;
        }
    }

    public void detonate()
    {
        for (int i = combatList.Count - 1; i >= 0; i--)
        {
            combatList[i].GetComponent<ThisBehavior>().damageEnemy(player.mineDamage);
        }
        player.damagePlayer(player.mineDamage);

        if (player.health == 0)
        {
            for (int i = 0; i < board.Count; i++)
            {
                board[i].SetActive(false);
            }

            combatPanel.SetActive(false);
            defeat.SetActive(true);
        }
    }

    private void combatEnd()
    {
        for (int i = 0; i < board.Count; i++)
        {
            board[i].SetActive(false);
        }

        combatPanel.SetActive(false);

        if (combatType != 2)
        {
            map.SetActive(true);
        }
        else
        {
            victory.SetActive(true);
        }
    }
}
