using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.UI;

public class Clue
{
    private string Name;
    private string Description;
    private RawImage Image;

    public Clue(string Name, string Description, RawImage image)
    {
        this.Name = Name;
        this.Description = Description;
        this.Image = image;
    }

    public string GetDescription()
    {
        return this.Description;
    }

    public string GetName()
    {
        return this.Name;
    }

    public RawImage GetImage()
    {
        return this.Image;
    }
}

