using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHateValueCtr : MonoBehaviour
{
    [SerializeField]
    private GameObject player1;

    [SerializeField]
    private GameObject player2;

    [SerializeField]
    private float lowerHateValueOnceTime = 120;

    private const float MaxHateValue = 99999;

    private const float MinHateValue = 100;

    private float player1HateValue;

    private float player2HateValue;

    // Use this for initialization
    void Start()
    {
        player1HateValue = MinHateValue;

        player2HateValue = MinHateValue;
    }
    public GameObject SendTargetToBehavior()
    {
        if (player1HateValue > player2HateValue)
        {
            LowerHateValue(PlayerSelection.Player1);
            //SendPlayer1
            return player1;

        }
        else if (player2HateValue > player1HateValue)
        {
            //SendPlayer2
            LowerHateValue(PlayerSelection.Player2);
            return player2;
        }
        else if (Mathf.FloorToInt(player1HateValue) == Mathf.FloorToInt(player2HateValue))
        {
            //RandomSelect
            var randomValue = Random.Range(1, 3);
            Debug.Log("randomValue" + randomValue);
            if (randomValue == 1)
            {
                //SendPlayer1
                LowerHateValue(PlayerSelection.Player1);
                return player1;
            }
            else if (randomValue == 2)
            {
                //SendPlayer2
                LowerHateValue(PlayerSelection.Player2);
                return player2;
            }
        }

        return null;
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
