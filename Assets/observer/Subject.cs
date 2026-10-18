using System;
using UnityEngine;

public class Subject : MonoBehaviour
{
    public bool ufoPresent = false;
    public Action onCall;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!ufoPresent)
            {
                ufoPresent = true;
                gameObject.GetComponent<Animator>().SetTrigger("call");
            }
        }
    }

    public void callUFO()
    {
        onCall?.Invoke();
    }
}
