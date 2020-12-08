using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    public bool isGamePaused;
    public int playerScore
    {
        get
        {
            return _playerScore;
        }

        private set
        {
            _playerScore = value;
            if(onScoreChanged != null)
            {
                onScoreChanged.Invoke(_playerScore);
            }
        }
    }
    private int _playerScore;

    [SerializeField] private PlayerController player;
    [SerializeField] private SpeciaController specialsController;
    [HideInInspector]
    public PlayerController Player
    {
        get { return player; }
    }

    public delegate void EnemyDied(GameObject corpse);
    public event EnemyDied onEnemyDied;


    public delegate void ScoreChanged(int UpdatedScore);
    public event ScoreChanged onScoreChanged;

    private void Awake()
    {
        Instance = this;
    }


    public void OnDie(GameObject deadObject, int score=0)
    {
        playerScore += score;
       

        Debug.Log(score);
        Debug.Log(playerScore);
       
        player.AddPowerLevel(1);

        if(onEnemyDied !=null)
        {
            onEnemyDied.Invoke(deadObject);
        }
    }

    public void onPlayerDie()
    {
        Debug.Log("***** PLAYER DIED ******");
    }

    public void onPickupPickedUp(PickupController pickup)
    {
        playerScore += pickup.config.score;
        Debug.Log("Picked");
        player.UnlockSpecial(pickup.config);
    }
}
