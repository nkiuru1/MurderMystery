using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Notebook PlayerNotebook;
    private string Name;
    private Room Location;

    public void SetData(string name, Room location, Notebook playerNotebook)
    {
        this.PlayerNotebook = playerNotebook;
        this.Name = name;
        this.Location = location;
    }

    public Notebook GetNotebook()
    {
        return this.PlayerNotebook;
    }
}
