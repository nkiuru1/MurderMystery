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
    public GameObject PanelTextBox, Panel;
    public Text LocationText, TurnText;
    Player MyPlayer;
    Room Location;
    private int Turn = 0;

    /*
        Initializes all objects and sets the starting location to the Entrance. 
    */
    void Start()
    {
        this.DisableCanvas();
        this.Entrance.enabled = true;
        GameMap.Createmap();
        this.Location = GameMap.GetStartLocation();
        this.Panel.SetActive(false);
        Notebook PlayerNotebook = new Notebook();
        this.MyPlayer = new Player("TestDude", this.Location, PlayerNotebook);
        this.Buttons.CreateDefaultButtons(MyPlayer);
    }

    /*
        Calls ButtonHandler's clicked method to check if any UI buttons are clicked.
        Calls Map's Mapclick method with the current location and it returns a new location if a map button is clicked.
        Sets the LocationText to the name of the current room.
    */
    void Update()
    {
        this.Buttons.Clicked();
        Location = this.GameMap.Mapclick(Location);
        if (!RoomName.Equals(Location.GetName()))
        {
            if (Location.GetName().Equals("Entrance Hall"))
            {
                this.RoomName = "Entrance Hall";
                this.DisableCanvas();
                Entrance.enabled = true;
                Turn++;
            }
            if (Location.GetName().Equals("Lounge"))
            {
                this.RoomName = "Lounge";
                this.DisableCanvas();
                Lounge.enabled = true;
                Turn++;
            }
            if (Location.GetName().Equals("Servant"))
            {
                this.RoomName = "Servant";
                this.DisableCanvas();
                Servants.enabled = true;
                Turn++;
            }
            if (Location.GetName().Equals("Library"))
            {
                this.RoomName = "Library";
                this.DisableCanvas();
                Library.enabled = true;
                Turn++;
            }
            if (Location.GetName().Equals("Kitchen"))
            {
                this.RoomName = "Kitchen";
                this.DisableCanvas();
                Kitchen.enabled = true;
                Turn++;
            }
            if (Location.GetName().Equals("Bed"))
            {
                this.RoomName = "Bed";
                this.DisableCanvas();
                Bedroom.enabled = true;
                Turn++;
            }
            if (Location.GetName().Equals("Dining Room"))
            {
                this.RoomName = "Dining Room";
                this.DisableCanvas();
                Dining.enabled = true;
                Turn++;
            }
            if (Location.GetName().Equals("Study"))
            {
                this.RoomName = "Study";
                this.DisableCanvas();
                Study.enabled = true;
                Turn++;
            }
        }
        this.LocationText.text = this.RoomName;
    }

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
    public void AddClueToInventory(string name)
    {
        Clue temp = Location.GetClue(name);
        if (temp != null)
        {
            MyPlayer.GetNotebook().AddClue(temp);
            this.Panel.SetActive(true);
            this.PanelTextBox.GetComponent<Text>().text = name + " added to notebook";
            StartCoroutine(ShowAndHide(Panel,2.0f));
        }
    }
    IEnumerator ShowAndHide(GameObject go, float delay)
    {
        go.SetActive(true);
        yield return new WaitForSeconds(delay);
        go.SetActive(false);
    }
}
