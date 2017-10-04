using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class Room
{
    private string Name;
    private List<Clue> CluesInRoom;
    private List<Character> CharactersInRoom;

    public Room(string name)
    {
        this.Name = name;
        CluesInRoom = new List<Clue>();
        CharactersInRoom = new List<Character>();
    }

    public string GetName()
    {
        return this.Name;
    }

    public void SetClues(List<Clue> clues)
    {
        this.CluesInRoom = clues;
    }

    public Clue GetClue(string name)
    {
        foreach (Clue item in this.CluesInRoom)
        {
            if (item.GetName().Equals(name))
            {
                return item;
            }
        }
        return null;
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

    public List<Character> GetCharacters()
    {
        return this.CharactersInRoom;
    }
}

