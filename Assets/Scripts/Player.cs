using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Player : Character
{
    public Notebook PlayerNotebook;

    public Player(string name, Room location, Notebook playerNotebook) : base(name,location)
    {
        this.PlayerNotebook = playerNotebook;
    }

    public Notebook GetNotebook()
    {
        return this.PlayerNotebook;
    }
}
