using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public ButtonHandler Buttons;
    private string RoomName = "Entrance Hall";
    public Map GameMap;
    public Canvas Entrance, Lounge, Dining, Kitchen, Servants, Bedroom, Study, Library;
    Player MyPlayer;
    Room Location;

    // Use this for initialization
    void Start()
    {
        this.DisableCanvas();
        Entrance.enabled = true;
        GameMap.Createmap();
        Notebook PlayerNotebook = new Notebook();
        this.Location = GameMap.GetStartLocation();
        this.MyPlayer = new Player("TestDude", this.Location, PlayerNotebook);
        this.Buttons.CreateDefaultButtons(MyPlayer);
    }

    // Update is called once per frame
    void Update()
    {
        this.Buttons.Clicked();
        Location = this.GameMap.Mapclick(Location);
        if (Location != null && !RoomName.Equals(Location.GetName()))
        {
            if (Location.GetName().Equals("Entrance Hall"))
            {
                this.RoomName = "Entrance Hall";
                this.DisableCanvas();
                Entrance.enabled = true;
            }
            if (Location.GetName().Equals("Lounge"))
            {
                this.RoomName = "Lounge";
                this.DisableCanvas();
                Lounge.enabled = true;
            }
            if (Location.GetName().Equals("Servant"))
            {
                this.RoomName = "Servant";
                this.DisableCanvas();
                Servants.enabled = true;
            }
            if (Location.GetName().Equals("Library"))
            {
                this.RoomName = "Library";
                this.DisableCanvas();
                Library.enabled = true;
            }
            if (Location.GetName().Equals("Kitchen"))
            {
                this.RoomName = "Kitchen";
                this.DisableCanvas();
                Kitchen.enabled = true;
            }
            if (Location.GetName().Equals("Bed"))
            {
                this.RoomName = "Bed";
                this.DisableCanvas();
                Bedroom.enabled = true;
            }
            if (Location.GetName().Equals("Dining Room"))
            {
                this.RoomName = "Dining Room";
                this.DisableCanvas();
                Dining.enabled = true;
            }
            if (Location.GetName().Equals("Study"))
            {
                this.RoomName = "Study";
                this.DisableCanvas();
                Study.enabled = true;
            }
        }
    }

    private void DisableCanvas()
    {
        Study.enabled = false;
        Dining.enabled = false;
        Bedroom.enabled = false;
        Kitchen.enabled = false;
        Library.enabled = false;
        Servants.enabled = false;
        Lounge.enabled = false;
        Entrance.enabled = false;
    }
    public void AddClueToInventory(string name)
    {
        Clue temp = Location.GetClue(name);
        if (temp != null)
        {
            MyPlayer.GetNotebook().AddClue(temp);
        }

    }
}
