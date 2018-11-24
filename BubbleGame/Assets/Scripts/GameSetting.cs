using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameSetting : MonoBehaviour
{
    static public int MaxPlayerNum = 2;

    private GameObject[] wargs;
    private GameObject[] octopus;

    static public int WargsNum;
    static public int OctopusNum;

    private void Start()
    {
        wargs = GameObject.FindGameObjectsWithTag("Warg");
        WargsNum = wargs.Length;

        octopus = GameObject.FindGameObjectsWithTag("Octopus");
        OctopusNum = octopus.Length;
    }

    private void Update()
    {
        wargs = GameObject.FindGameObjectsWithTag("Warg");
        WargsNum = wargs.Length;

        octopus = GameObject.FindGameObjectsWithTag("Octopus");
        OctopusNum = octopus.Length;
    }
}
