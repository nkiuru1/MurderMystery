using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Notebook PlayerNotebook;
    private bool AgreementWithReporter = false;

    public void SetData(Notebook playerNotebook)
    {
        this.PlayerNotebook = playerNotebook;
        playerNotebook.AddClue(new Clue("Nobleman", "The now deceased owner of the mansion. An old friend of yours, whom you haven't met in ages. " +
    "He won't be able to rest in peace until the culprit has been found."));
        playerNotebook.AddClue(new Clue("Constable", "The police officer who is on his way to the mansion."));
        playerNotebook.AddClue(new Clue("Invitation", "Invitation to a dinner party. It was sent to you by your friend, the victim."));
    }

    public Notebook GetNotebook()
    {
        return this.PlayerNotebook;
    }
    public void MakeAgreement()
    {
        this.AgreementWithReporter = true;
    }

    public bool MadeAgreementWithReporter()
    {
        return AgreementWithReporter;
    }
}
