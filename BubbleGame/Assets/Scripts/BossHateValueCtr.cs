using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHateValueCtr : MonoBehaviour
{
    [SerializeField]
    private float lowerHateValueOnceTime = 120;

    private const float MaxHateValue = 99999;

    private float player1HateValue = 100;

    private float player2HateValue = 100;

    // Use this for initialization
    void Start()
    {
        player1HateValue = 100;

        player2HateValue = 100;


    }

    private void Update()
    {
        Debug.Log("player1HateValue" + player1HateValue);
        Debug.Log("player2HateValue" + player2HateValue);

    }

    public void SendTargetToBehavior()
    {
        if (player1HateValue > player2HateValue)
        {
            //SendPlayer1
            LowerHateValue(PlayerSelection.Player1);
        }
        else if (player2HateValue > player1HateValue)
        {
            //SendPlayer2
            LowerHateValue(PlayerSelection.Player2);
        }
        else if (Mathf.FloorToInt(player1HateValue) == Mathf.FloorToInt(player2HateValue))
        {
            //RandomSelect
            var randomValue = Random.Range(0, 1);
            if (randomValue == 0)
            {
                //SendPlayer1
                LowerHateValue(PlayerSelection.Player1);
            }
            else if (randomValue == 1)
            {
                //SendPlayer2
                LowerHateValue(PlayerSelection.Player2);
            }
        }
    }

    public void LowerHateValue(PlayerSelection _playerSelection)
    {
        //AfterAttack
        if (_playerSelection == PlayerSelection.Player1)
        {
            player1HateValue -= lowerHateValueOnceTime;
        }
        else if (_playerSelection == PlayerSelection.Player2)
        {
            player2HateValue -= lowerHateValueOnceTime;
        }
    }

    public void IncreaseHateValueByCrash(float _power, PlayerSelection _playerSelection)
    {
        if (_playerSelection == PlayerSelection.Player1)
        {
            if (player1HateValue <= MaxHateValue)
                player1HateValue += _power;
            else
                player1HateValue = MaxHateValue;
        }
        else if (_playerSelection == PlayerSelection.Player2)
        {
            if (player2HateValue <= MaxHateValue)
                player2HateValue += _power;
            else
                player2HateValue = MaxHateValue;
        }
    }

    public void IncreaseHateValueByLittleBubble(float _power, PlayerSelection _playerSelection)
    {
        if (_playerSelection == PlayerSelection.Player1)
        {
            if (player1HateValue <= MaxHateValue)
                player1HateValue += _power;
            else
                player1HateValue = MaxHateValue;
        }
        else if (_playerSelection == PlayerSelection.Player2)
        {
            if (player2HateValue <= MaxHateValue)
                player2HateValue += _power;
            else
                player2HateValue = MaxHateValue;
        }
    }

    public void IncreaseHateValueByBigBubble(float _power, PlayerSelection _playerSelection)
    {
        if (_playerSelection == PlayerSelection.Player1)
        {
            if (player1HateValue <= MaxHateValue)
                player1HateValue += _power;
            else
                player1HateValue = MaxHateValue;
        }
        else if (_playerSelection == PlayerSelection.Player2)
        {
            if (player2HateValue <= MaxHateValue)
                player2HateValue += _power;
            else
                player2HateValue = MaxHateValue;
        }
    }
}
