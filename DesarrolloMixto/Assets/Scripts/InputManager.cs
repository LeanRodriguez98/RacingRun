using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

    #region Singleton
    public static InputManager instance;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    #endregion

    InputInterface input;
    
    private void Start()
    {
        input = new InputPC();       
    }

    public bool Fire()
    {
        if (input.Fire())
            return true;
        return false;
    } 
}
