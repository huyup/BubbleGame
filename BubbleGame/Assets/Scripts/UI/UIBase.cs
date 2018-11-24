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
    private PlayerStatus[] players;

    [SerializeField]
    private float baseWidth = 854f;

    [SerializeField]
    private float baseHeight = 480f;

    public bool IsVisible { get; private set; }


    public void SetUIVisible(bool _isVisible)
    {
        IsVisible = _isVisible;
    }

    private void OnGUI()
    {
        if (IsVisible)
        {
            // 解像度対応.
            GUI.matrix = Matrix4x4.TRS(
                Vector3.zero,
                Quaternion.identity,
                new Vector3(Screen.width / baseWidth, Screen.height / baseHeight, 1f));

            //GUI.Label(new Rect(10, 450, 100, 100), "Player1:", fontStyle);
            //DrawPlayer1Life(players[0].GetNowHp());

            //GUI.Label(new Rect(690, 450, 100, 100), "Player2:", fontStyle);
            //DrawPlayer2Life(players[1].GetNowHp());

            GUI.Label(new Rect(10, 10, 100, 100), "残りの狼:" + GameSetting.Instance.GetWargsNum() + "匹", fontStyle);
            GUI.Label(new Rect(10, 30, 100, 100), "残りのタコ:" + GameSetting.Instance.GetOctopusNum() + "匹", fontStyle);
        }
    }

    void DrawPlayer1Life(int _player1hp)
    {
        float tmp = 27;
        float tmp2 = 28;

        switch (_player1hp)
        {
            case 0:
                break;
            case 1:
                GUI.Label(new Rect(90, 450, tmp2, tmp2), heartImg);
                break;
            case 2:
                GUI.Label(new Rect(90, 450, tmp2, tmp2), heartImg);
                GUI.Label(new Rect(90 + tmp, 450, tmp2, tmp2), heartImg);
                break;
            case 3:
                GUI.Label(new Rect(90, 450, tmp2, tmp2), heartImg);
                GUI.Label(new Rect(90 + tmp, 450, tmp2, tmp2), heartImg);
                GUI.Label(new Rect(90 + tmp * 2, 450, tmp2, tmp2), heartImg);
                break;
        }
    }
    void DrawPlayer2Life(int _player2hp)
    {
        float tmp = 27;
        float tmp2 = 28;

        switch (_player2hp)
        {
            case 0:
                break;
            case 1:
                GUI.Label(new Rect(770, 450, tmp2, tmp2), heartImg);
                break;
            case 2:
                GUI.Label(new Rect(770, 450, tmp2, tmp2), heartImg);
                GUI.Label(new Rect(770 + tmp, 450, tmp2, tmp2), heartImg);
                break;
            case 3:
                GUI.Label(new Rect(770, 450, tmp2, tmp2), heartImg);
                GUI.Label(new Rect(770 + tmp, 450, tmp2, tmp2), heartImg);
                GUI.Label(new Rect(770 + tmp * 2, 450, tmp2, tmp2), heartImg);
                break;
        }
    }
}
