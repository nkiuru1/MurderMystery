using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public ButtonHandler Buttons;
    public Map GameMap;
    public Canvas Entrance, Lounge, Dining, Kitchen, Servants, Bedroom, Study, Library;
    public GameObject PanelTextBox, Panel;
    public Text LocationText, TurnText;
    public Turn Turn;
    private string RoomName = "Entrance Hall";
    public Player MyPlayer;
    public Character Butler, Noble;
    private Room Location;

    /// <summary>
    /// Initializes all objects and sets the starting location to the Entrance.
    /// Dont't Even try to change the start location. It will NOT work.
    /// </summary>
    void Start()
    {
        this.DisableCanvas();
        this.Entrance.enabled = true;
        GameMap.Createmap();
        this.Location = GameMap.GetStartLocation();
        this.Panel.SetActive(false);
        Notebook PlayerNotebook = new Notebook();
        this.MyPlayer.SetData("TestDude", this.Location, PlayerNotebook);
        this.Buttons.CreateDefaultButtons(MyPlayer);
        this.LocationText.text = this.RoomName;
        this.TurnText.text = (30 - Turn.GetTurn()).ToString();
    }

    /// <summary>
    /// Calls ButtonHandler's clicked method to check if any UI buttons are clicked.
    /// Calls Map's Mapclick method with the current location and it returns a new location if a map button is clicked.
    /// Enables the canvas that has been selected from the map.
    /// Sets the LocationText to the name of the current room.
    /// </summary>
    void Update()
    {
        this.Buttons.Clicked();
        Location = this.GameMap.Mapclick(Location);
        this.Buttons.UpdateRoom(Location);
        if (!RoomName.Equals(Location.GetName()))
        {
            if (Location.GetName().Equals("Entrance Hall"))
            {
                this.RoomName = "Entrance Hall";
                this.DisableCanvas();
                Entrance.enabled = true;
                Turn.NextTurn();
            }
            if (Location.GetName().Equals("Lounge"))
            {
                this.RoomName = "Lounge";
                this.DisableCanvas();
                Lounge.enabled = true;
                Turn.NextTurn();
            }
            if (Location.GetName().Equals("Servant"))
            {
                this.RoomName = "Servant";
                this.DisableCanvas();
                Servants.enabled = true;
                Turn.NextTurn();
            }
            if (Location.GetName().Equals("Library"))
            {
                this.RoomName = "Library";
                this.DisableCanvas();
                Library.enabled = true;
                Turn.NextTurn();
            }
            if (Location.GetName().Equals("Kitchen"))
            {
                this.RoomName = "Kitchen";
                this.DisableCanvas();
                Kitchen.enabled = true;
                Turn.NextTurn();
            }
            if (Location.GetName().Equals("Bed"))
            {
                this.RoomName = "Bed";
                this.DisableCanvas();
                Bedroom.enabled = true;
                Turn.NextTurn();
            }
            if (Location.GetName().Equals("Dining Room"))
            {
                this.RoomName = "Dining Room";
                this.DisableCanvas();
                Dining.enabled = true;
                Turn.NextTurn();
            }
            if (Location.GetName().Equals("Study"))
            {
                this.RoomName = "Study";
                this.DisableCanvas();
                Study.enabled = true;
                Turn.NextTurn();
            }
            this.TurnAction();
            this.LocationText.text = this.RoomName;
            this.TurnText.text = (20 - Turn.GetTurn()).ToString();
        }
    }
    /// <summary>
    /// Disables other canvases
    /// </summary>
    private void DisableCanvas()
    {
        Entrance.enabled = false;
        Study.enabled = false;
        Bedroom.enabled = false;
        Kitchen.enabled = false;
        Library.enabled = false;
        Servants.enabled = false;
        Lounge.enabled = false;
        Dining.enabled = false;
    }
    /// <summary>
    /// Adds a clue to player inv if the room contains the clue that was clicked.
    /// Makes a popup to show what was picked up
    /// </summary>
    /// <param name="name"> Name of the clue</param>
    public void AddClueToInventory(string name)
    {
        Clue temp = Location.GetClue(name);
        if (temp != null)
        {
            MyPlayer.GetNotebook().AddClue(temp);
            this.Panel.SetActive(true);
            this.PanelTextBox.GetComponent<Text>().text = name + " added to notebook";
            StartCoroutine(ShowAndHide(Panel, 2.0f));
        }

        Location.DestroyClue(name);
    }
    IEnumerator ShowAndHide(GameObject go, float delay)
    {
        go.SetActive(true);
        yield return new WaitForSeconds(delay);
        go.SetActive(false);
    }

    /// <summary>
    /// Makes changes in the game world when turn has changed.
    /// </summary>
    public void TurnAction()
    {
        // test code
        // move character to Lounge on turn 3
        if (Turn.GetTurn().Equals(3))
        {
            GameMap.TransportCharacter(Noble, GameMap.GetRoomObject("Lounge"));
        }
    }
}