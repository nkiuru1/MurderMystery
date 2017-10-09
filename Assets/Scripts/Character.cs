using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    private string Name;
    private Room CurrentLocation;
    public Dialogues CharacterDialogues;
    private bool TalkedTo = false;
    private Clue CharacterClue;

    public void SetData(string name, Room loc)
    {
        this.Name = name;
        this.CurrentLocation = loc;
    }
    public void SetTree(string treeName)
    {
        this.CharacterDialogues.SetTree(treeName);
    }

    public void SetClue(Clue clue)
    {
        this.CharacterClue = clue;
    }

    public Clue GetClue()
    {
        return this.CharacterClue;
    }
    public string[] GetChoices()
    {
        return this.CharacterDialogues.GetChoices();
    }

    public bool NextChoice(string response)
    {
        return this.CharacterDialogues.NextChoice(response);
    }

    public int Next()
    {
        return this.CharacterDialogues.Next();
    }

    public string GetDialogue()
    {
        return this.CharacterDialogues.GetCurrentDialogue();
    }
    public Room GetLocation()
    {
        return this.CurrentLocation;
    }

    public void ResetConversation()
    {
        this.CharacterDialogues.Reset();
    }

    public bool IsEndOfConversation()
    {
        return this.CharacterDialogues.End();
    }

    public string GetName()
    {
        return this.Name;
    }

    public void ChangeLocation(Room room)
    {
        this.CurrentLocation = room;
    }

    public bool HasTalkedTo()
    {
        return TalkedTo;
    }

    public void Talked()
    {
        TalkedTo = true;
    }
}


