using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Notebook PlayerNotebook;

    public void SetData(Notebook playerNotebook)
    {
        this.PlayerNotebook = playerNotebook;
    }

    public Notebook GetNotebook()
    {
        return this.PlayerNotebook;
    }
}
