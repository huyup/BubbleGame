using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WargFunctionRef : EnemyFunctionRef
{
    [SerializeField]
    private WargsCommonParameter wargsCommonParameter;
  
    public WargsCommonParameter GetWargsParameter()
    {
        return wargsCommonParameter;
    }
}
