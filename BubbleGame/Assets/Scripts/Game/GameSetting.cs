using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameSetting : Singleton<GameSetting>
{
    public static int MaxPlayerNum = 2;

    private GameObject[] wargs;
    private GameObject[] octopus;
    
    private int wargsNum;
    private int octopusNum;

    private void Start()
    {
        wargs = GameObject.FindGameObjectsWithTag("Warg");
        wargsNum = wargs.Length;

        octopus = GameObject.FindGameObjectsWithTag("Octopus");
        octopusNum = octopus.Length;
    }

    public void Update()
    {
        Debug.Log("Update");
        wargs = GameObject.FindGameObjectsWithTag("Warg");
        wargsNum = wargs.Length;

        octopus = GameObject.FindGameObjectsWithTag("Octopus");
        octopusNum = octopus.Length;
    }

    public int GetWargsNum()
    {
        return wargsNum;
    }

    public int GetOctopusNum()
    {
        return octopusNum;
    }

    protected override void OnCreated()
    {
        base.OnCreated();
    }

    protected override void OnDisposed()
    {
        base.OnDisposed();
    }
}
