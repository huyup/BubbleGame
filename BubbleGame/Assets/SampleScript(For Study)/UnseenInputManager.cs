using UnityEngine;

public class UnseenInputManager : Singleton<UnseenInputManager>
{
    public void OnUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("ここでスペース");
        }   
    }
}
