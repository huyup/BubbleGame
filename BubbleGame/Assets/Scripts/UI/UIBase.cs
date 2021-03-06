﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
/// <summary>
/// TODO:UIの配置に関する修正
/// </summary>
public class UIBase : MonoBehaviour
{
    [SerializeField]
    private Transform player1;

    [SerializeField]
    private Transform player2;

    [SerializeField]
    private Transform boss;

    [SerializeField] private Text bossNameText;
    [SerializeField] private RawImage bossHpImage;
    [SerializeField] private RawImage bossStaminaImage;

    [SerializeField] private Text player1NameText;
    [SerializeField] private Text player2NameText;

    [SerializeField] private Text player1WeaponText;
    [SerializeField] private Text player2WeaponText;

    [SerializeField] private RawImage player1AmmoImage;
    [SerializeField] private RawImage player2AmmoImage;

    [SerializeField] private Image player1Heart1;
    [SerializeField] private Image player1Heart2;
    [SerializeField] private Image player1Heart3;

    [SerializeField] private Image player2Heart1;
    [SerializeField] private Image player2Heart2;
    [SerializeField] private Image player2Heart3;

    [SerializeField] private Text clearText;
    public bool IsVisible { get; private set; }

    [SerializeField]
    private Rect scaleRect;

    public void SetUIVisible(bool _isVisible)
    {
        IsVisible = _isVisible;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            PlayerGetDamage(3);

        if (boss)
        {
            DrawBossHp(boss.GetComponent<ObjController>(), bossHpImage);
            DrawBossStamina(boss.GetComponent<BossStaminaCtr>(), bossStaminaImage);
        }

        DrawPlayerAmmo(player1.GetComponent<PlayerAmmoCtr>(), player1AmmoImage);
        DrawPlayerAmmo(player2.GetComponent<PlayerAmmoCtr>(), player2AmmoImage);

        DrawPlayer1Life(player1.GetComponent<PlayerStatus>().nowHp);
        DrawPlayer2Life(player2.GetComponent<PlayerStatus>().nowHp);

        DrawPlayerWeapon(player1.GetComponent<PlayerController>().GetNowWeaponType(), player1WeaponText);
        DrawPlayerWeapon(player2.GetComponent<PlayerController>().GetNowWeaponType(), player2WeaponText);
    }

    public void DrawClearText()
    {
        clearText.enabled = true;
    }
    private void DrawBossStamina(BossStaminaCtr _staminaController, RawImage _bossStamina)
    {
        //200->1 100->0.5 0->0
        //200-100=100 
        int maxStamina = 100;
        int nowStamina = _staminaController.GetNowStamina();
        float newRate = 1 * (nowStamina * 100 / maxStamina) * 0.01f;

        _bossStamina.uvRect = new Rect(0, 1 - newRate, _bossStamina.uvRect.width, _bossStamina.uvRect.height);

    }

    private void DrawBossHp(ObjController _objController, RawImage _bossHp)
    {
        //200->1 100->0.5 0->0
        //200-100=100 
        int maxHp = 100;
        int nowHp = _objController.GetBossNowHp();
        float newRate = 1 * (nowHp * 100 / maxHp) * 0.01f;
        _bossHp.uvRect = new Rect(0, 1 - newRate, 1, 1);

    }
    private void DrawPlayerAmmo(PlayerAmmoCtr _ammoCtr, RawImage _playerAmmoImage)
    {
        if (!_ammoCtr)
        {
            return;
        }
        else
        {
            //y=0 ammo=Max, y=1 ammo=0
            int ammoCount = _ammoCtr.GetNowAmmo();
            _playerAmmoImage.uvRect = new Rect(0, (100 - ammoCount) * 0.01f, 1, 1);
        }
    }
    private void DrawPlayerWeapon(WeaponType _weaponType, Text _weaponText)
    {

        switch (_weaponType)
        {
            case WeaponType.WeaponA:
                _weaponText.text = "装備中:大きいストロー";
                break;
            case WeaponType.WeaponB:
                _weaponText.text = "装備中:小さいストロー";
                break;
            case WeaponType.WeaponC:
                _weaponText.text = "装備中:空気砲";
                break;
            case WeaponType.WeaponD:
                _weaponText.text = "装備中:空気逆流装置";
                break;
            default:
                _weaponText.text = "None";
                break;
        }
    }

    public void PlayerGetDamage(int _nowHp)
    {
        switch (_nowHp)
        {
            case 3:
                DisableHpImg(player1Heart1);
                break;
            case 2:
                player1Heart1.enabled = true;
                player1Heart2.enabled = true;
                player1Heart3.enabled = false;
                break;
            case 1:
                player1Heart1.enabled = true;
                player1Heart2.enabled = false;
                player1Heart3.enabled = false;
                break;
            case 0:
                player1Heart1.enabled = false;
                player1Heart2.enabled = false;
                player1Heart3.enabled = false;
                break;
        }
    }

    private void DisableHpImg(Image _hpImg)
    {
        Debug.Log("DisableHpImg");
        var sequence = DOTween.Sequence();
        sequence.Append(_hpImg.transform.DOScale(Vector3.one * 2f, 1.5f));
        sequence.Join(_hpImg.DOFade(0, 1.5f));
        //sequence.Append(_hpImg.DOFade(Vector3.zero, 1));
    }



    void DrawPlayer1Life(int _player1Hp)
    {
        switch (_player1Hp)
        {
            case 0:
                player1Heart1.enabled = false;
                player1Heart2.enabled = false;
                player1Heart3.enabled = false;
                break;
            case 1:
                player1Heart1.enabled = true;
                player1Heart2.enabled = false;
                player1Heart3.enabled = false;
                break;
            case 2:
                player1Heart1.enabled = true;
                player1Heart2.enabled = true;
                player1Heart3.enabled = false;
                break;
            case 3:
                player1Heart1.enabled = true;
                player1Heart2.enabled = true;
                player1Heart3.enabled = true;
                break;
        }
    }
    void DrawPlayer2Life(int _player2Hp)
    {
        switch (_player2Hp)
        {
            case 0:
                player2Heart1.enabled = false;
                player2Heart2.enabled = false;
                player2Heart3.enabled = false;
                break;
            case 1:
                player2Heart1.enabled = true;
                player2Heart2.enabled = false;
                player2Heart3.enabled = false;
                break;
            case 2:
                player2Heart1.enabled = true;
                player2Heart2.enabled = true;
                player2Heart3.enabled = false;
                break;
            case 3:
                player2Heart1.enabled = true;
                player2Heart2.enabled = true;
                player2Heart3.enabled = true;
                break;
        }
    }
}
