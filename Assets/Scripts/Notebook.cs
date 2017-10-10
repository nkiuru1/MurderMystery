using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Notebook
{
    private List<Clue> Clues;

    public Notebook()
    {
        this.Clues = new List<Clue>();
    }

    public void AddClue(Clue clue)
    {
        this.Clues.Add(clue);
    }

    public Clue GetClue(string name)
    {
        foreach (Clue clue in this.Clues)
        {
            if (clue.GetName().Equals(name))
            {
                return clue;
            }
        }
        return null;
    }

    public void DeleteClue(string name)
    {
        foreach (Clue clue in this.Clues)
        {
            if (clue.GetName().Equals(name))
            {
                this.Clues.Remove(clue);
            }
        }
    }

    public List<Clue> GetClues()
    {
        return this.Clues;
    }

    public bool HasClue(string clueName)
    {
        foreach (Clue clue in this.Clues)
        {
            if (clue.GetName().Equals(clueName))
            {
                return true;
            }
        }
        return false;
    }
}

