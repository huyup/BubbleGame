using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// TODO:UIの配置に関する修正
/// </summary>
public class UIBase : MonoBehaviour
{
    [SerializeField]
    private GUIStyle fontStyle;

    [SerializeField]
    private Texture2D heartImg;

    [SerializeField]
    private Transform player1;

    [SerializeField]
    private Transform player2;

    [SerializeField]
    private float baseWidth = 1920f;

    [SerializeField]
    private float baseHeight = 1080f;

    [SerializeField]
    private Rect heart1Rect;

    [SerializeField]
    private Rect heart2Rect;

    [SerializeField]
    private Rect heart3Rect;

    [SerializeField]
    private Rect heart1Rect2;

    [SerializeField]
    private Rect heart2Rect2;

    [SerializeField]
    private Rect heart3Rect2;

    [SerializeField]
    private Rect player1Rect;

    [SerializeField]
    private Rect player2Rect;

    public bool IsVisible { get; private set; }

    public void SetUIVisible(bool _isVisible)
    {
        IsVisible = _isVisible;
    }

    private void OnGUI()
    {
        GUI.Label(player1Rect, "Player1:", fontStyle);
        DrawPlayer1Life(player1.GetComponent<PlayerStatus>().nowHp);

        GUI.Label(player2Rect, "Player2:", fontStyle);
        DrawPlayer2Life(player2.GetComponent<PlayerStatus>().nowHp);
        if (IsVisible)
        {
            GUI.Label(new Rect(10, 10, 100, 100), "残りのうり坊:" + GameSetting.Instance.GetWargsNum() + "匹", fontStyle);
            GUI.Label(new Rect(10, 30, 100, 100), "残りのタコ:" + GameSetting.Instance.GetOctopusNum() + "匹", fontStyle);
        }
    }

    void DrawPlayer1Life(int _player1Hp)
    {
        switch (_player1Hp)
        {
            case 0:
                break;
            case 1:
                GUI.Label(heart1Rect, heartImg);
                break;
            case 2:
                GUI.Label(heart1Rect, heartImg);
                GUI.Label(heart2Rect, heartImg);
                break;
            case 3:
                GUI.Label(heart1Rect, heartImg);
                GUI.Label(heart2Rect, heartImg);
                GUI.Label(heart3Rect, heartImg);
                break;
        }
    }
    void DrawPlayer2Life(int _player2Hp)
    {
        switch (_player2Hp)
        {
            case 0:
                break;
            case 1:
                GUI.Label(heart1Rect2, heartImg);
                break;
            case 2:
                GUI.Label(heart1Rect2, heartImg);
                GUI.Label(heart2Rect2, heartImg);
                break;
            case 3:
                GUI.Label(heart1Rect2, heartImg);
                GUI.Label(heart2Rect2, heartImg);
                GUI.Label(heart3Rect2, heartImg);
                break;
        }
    }
}
