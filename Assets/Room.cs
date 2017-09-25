using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class Room
{
    private RawImage Background;
    private string Name;
    private List<Clue> CluesInRoom;
    private List<Character> CharactersInRoom;

    public Room(RawImage background, string name)
    {
        Background = background;
        this.Name = name;
    }

    public string GetName()
    {
        return this.Name;
    }

    public RawImage GetBackground()
    {
        return this.Background;
    }

    public void SetClues(List<Clue> clues)
    {
        this.CluesInRoom = clues;
    }

    public void SetCharacters(List<Character> characters)
    {
        this.CharactersInRoom = characters;
    }

    public void RemoveCharacter(string name)
    {
        foreach (Character npc in this.CharactersInRoom)
        {
            if (npc.GetName().Equals(name))
            {
                this.CharactersInRoom.Remove(npc);
            }
        }
    }
}

