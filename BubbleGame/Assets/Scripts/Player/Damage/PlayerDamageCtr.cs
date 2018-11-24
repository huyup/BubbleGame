using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamageCtr : PlayerController {
    //無敵かどうか
    private bool isVincible = false;
    
    private int nowHp;

    public override void OnInitialize()
    {
        nowHp = Status.MaxHp;
    }

    public override void OnUpdate()
    {
        CheckDied();
    }

    public void ReciveDamage()
    {
        if (isVincible || nowHp <= 0)
            return;

        if (nowHp > 0)
            nowHp--;

        StartCoroutine(Blink());
    }

    IEnumerator Blink()
    {
        Renderer renderers;
        renderers = transform.Find("PlayerMesh").GetComponent<Renderer>();

        for (int i = 0; i < Status.InvincibleTotalTime; i++)
        {
            renderers.enabled = !renderers.enabled;
            isVincible = true;
            yield return new WaitForSeconds(Status.InvincibleInterval);
        }

        isVincible = false;
    }
    private void CheckDied()
    {
        if (nowHp <= 0)
        {
            //TODO:ここにプレイヤーの死亡した後の操作を入れる
            this.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 15/*EnemyHit*/&& other.gameObject.tag != "SearchTrigger")
        {
            ReciveDamage();
        }
    }
}
