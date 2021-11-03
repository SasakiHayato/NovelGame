using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
    // Singleton
    static private GameManager m_instance = new GameManager();
    static public GameManager Instnace() => m_instance;
    private GameManager() { }

    public bool IsBreak { get; set; } = false;
}
