using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Turn : MonoBehaviour
{
    private int CurrentTurn = 0;
    
    public int GetTurn()
    {
        return this.CurrentTurn;
    }

    public void NextTurn()
    {
        this.CurrentTurn++;
    }
}
