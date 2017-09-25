using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character
{
    private string Name;
    private Room CurrentLocation;

    public Character(string name, Room loc)
    {
        this.Name = name;
        this.CurrentLocation = loc;
    }

    public Room GetLocation()
    {
        return this.CurrentLocation;
    }

    public string GetName()
    {
        return this.Name;
    }

    public void ChangeLocation(Room room)
    {
        this.CurrentLocation = room;
    }
}


