using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.UI;

public class Clue
{
    private string Name;
    private string Description;

    public Clue(string Name, string Description)
    {
        this.Name = Name;
        this.Description = Description;
    }

    public string GetDescription()
    {
        return this.Description;
    }

    public string GetName()
    {
        return this.Name;
    }
}

