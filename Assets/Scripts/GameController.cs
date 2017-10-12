using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public ButtonHandler Buttons;
    public Map GameMap;
    public Canvas Entrance, Lounge, Dining, Kitchen, Servants, Bedroom, Study, Library;
    public GameObject PanelTextBox, Panel;
    public Text LocationText, TurnText;
    public Turn Turn;
    public Player MyPlayer;
    public Character Butler, Count, Chef, Maid, Businessman, Reporter, Doctor, Writer, Narrator;
    private string RoomName = "Lobby";
    private Canvas CurrentCanvas;
    private Room Location;
    private int LastTurn = 0;
    private int MaxTurns = 20;

    /// <summary>
    /// Initializes all objects and sets the starting location to the Entrance.
    /// Dont't Even try to change the start location. It will NOT work.
    /// </summary>
    void Start()
    {
        this.DisableCanvas();
        this.Entrance.enabled = true;
        this.CurrentCanvas = Entrance;
        GameMap.Createmap();
        this.Location = GameMap.GetStartLocation();
        this.Panel.SetActive(false);
        Notebook PlayerNotebook = new Notebook();
        this.MyPlayer.SetData(PlayerNotebook);
        this.Buttons.CreateDefaultButtons(MyPlayer);
        this.LocationText.text = this.RoomName;
        this.TurnText.text = (this.MaxTurns - Turn.GetTurn()).ToString();
        this.Buttons.UpdateRoom(this.Location, this.CurrentCanvas);
        this.Buttons.GameStart();
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
        if (!RoomName.Equals(Location.GetName()))
        {
            if (Location.GetName().Equals("Lobby") && this.CanMoveToRoom(this.GameMap.GetRoomObject("Lobby")))
            {
                this.RoomName = "Lobby";
                this.DisableCanvas();
                Entrance.enabled = true;
                CurrentCanvas = Entrance;
                Turn.NextTurn();
            }
            else if (Location.GetName().Equals("Lounge") && this.CanMoveToRoom(this.GameMap.GetRoomObject("Lounge")))
            {
                this.RoomName = "Lounge";
                this.DisableCanvas();
                Lounge.enabled = true;
                CurrentCanvas = Lounge;
                Turn.NextTurn();
            }
            else if (Location.GetName().Equals("Servant") && this.CanMoveToRoom(this.GameMap.GetRoomObject("Servant")))
            {
                this.RoomName = "Servant";
                this.DisableCanvas();
                Servants.enabled = true;
                CurrentCanvas = Servants;
                Turn.NextTurn();
            }
            else if (Location.GetName().Equals("Library") && this.CanMoveToRoom(this.GameMap.GetRoomObject("Library")))
            {
                this.RoomName = "Library";
                this.DisableCanvas();
                Library.enabled = true;
                CurrentCanvas = Library;
                Turn.NextTurn();
            }
            else if (Location.GetName().Equals("Kitchen") && this.CanMoveToRoom(this.GameMap.GetRoomObject("Kitchen")))
            {
                this.RoomName = "Kitchen";
                this.DisableCanvas();
                Kitchen.enabled = true;
                CurrentCanvas = Kitchen;
                Turn.NextTurn();
            }
            else if (Location.GetName().Equals("Bed") && this.CanMoveToRoom(this.GameMap.GetRoomObject("Bed")))
            {
                this.RoomName = "Bed";
                this.DisableCanvas();
                Bedroom.enabled = true;
                CurrentCanvas = Bedroom;
                Turn.NextTurn();
            }
            else if (Location.GetName().Equals("Dining Room") && this.CanMoveToRoom(this.GameMap.GetRoomObject("Dinner")))
            {
                this.RoomName = "Dining Room";
                this.DisableCanvas();
                Dining.enabled = true;
                CurrentCanvas = Dining;
                Turn.NextTurn();
            }
            else if (Location.GetName().Equals("Study") && this.CanMoveToRoom(this.GameMap.GetRoomObject("Study")))
            {
                this.RoomName = "Study";
                this.DisableCanvas();
                Study.enabled = true;
                CurrentCanvas = Study;
                Turn.NextTurn();
            }
            this.LocationText.text = this.RoomName;
            this.TurnText.text = (this.MaxTurns - Turn.GetTurn()).ToString();
        }
        if (this.LastTurn != this.Turn.GetTurn())
        {
            this.TurnAction();
        }

        this.Buttons.UpdateRoom(Location, CurrentCanvas);
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
    /// Handles character clicks.
    /// </summary>
    public void CharacterClicked(string name)
    {
        // Handle character clicks only in talk mode
        if (Buttons.GetTalkActive())
        {
            Debug.Log("Character clicked: " + name);
        }
    }
    /// <summary>
    /// Checks if the player can move to that room, based on turn 
    /// </summary>
    /// <param name="nextRoom"></param>
    /// <returns>bool that states if the player can move there</returns>
    private bool CanMoveToRoom(Room nextRoom)
    {
        if (Turn.GetTurn() == 0 && nextRoom != this.GameMap.GetRoomObject("Dinner"))
        {
            this.Panel.SetActive(true);
            this.PanelTextBox.GetComponent<Text>().text = "You’re here for a dinner party, not an investigation. " +
                "You should follow the butler and go to the dining room";
            StartCoroutine(ShowAndHide(Panel, 2.0f));
            return false;
        }
        if (!this.MyPlayer.GetNotebook().HasClue("Bedroom Key") && nextRoom == this.GameMap.GetRoomObject("Bed"))
        {
            this.Panel.SetActive(true);
            this.PanelTextBox.GetComponent<Text>().text = "The bedroom is locked, I need a key";
            StartCoroutine(ShowAndHide(Panel, 3.0f));
            return false;
        }
        if (!this.MyPlayer.GetNotebook().HasClue("Study Room Key") && nextRoom == this.GameMap.GetRoomObject("Study"))
        {
            this.Panel.SetActive(true);
            this.PanelTextBox.GetComponent<Text>().text = "The study is locked, I need a key";
            StartCoroutine(ShowAndHide(Panel, 3.0f));
            return false;
        }
        return true;
    }

    /// <summary>
    /// Makes changes in the game world when turn has changed.
    /// Changes Dialogue trees & Moves Characters
    /// </summary>
    public void TurnAction()
    {
        if (Turn.GetTurn() == 1 && !this.Butler.GetTreeName().Equals("ButlerDefault"))
        {
            this.Buttons.DiningRoomAction();
            this.Butler.SetTree("ButlerDefault");
        }
        if (Turn.GetTurn() == 2)
        {
            this.GameMap.RemoveCharacter(Narrator);
        }
        if (Turn.GetTurn() >= this.MaxTurns)
        {
            SceneManager.LoadScene(3);
        }

        if (this.MyPlayer.GetNotebook().HasClue("Fake Letter"))
        {
            this.Maid.SetTree("MaidWithFakeLetter");
        }
        else
        {
            this.Maid.SetTree("MaidWithoutFakeLetter");
        }

        if (this.MyPlayer.GetNotebook().HasClue("Diary") && this.MyPlayer.MadeAgreementWithReporter())
        {
            this.Reporter.SetTree("ReporterAgreement");
        }
        if (this.MyPlayer.GetNotebook().HasClue("Study Room Key"))
        {
            this.Butler.SetTree("ButlerWithStudyKey");
        }

        if (this.MyPlayer.GetNotebook().HasClue("Victim's Will"))
        {
            this.Doctor.SetTree("DoctorWithWill");
        }

        if (this.MyPlayer.GetNotebook().HasClue("Study Room Key"))
        {
            this.Chef.SetTree("ChefWithStudyKey");
        }
        if (this.MyPlayer.GetNotebook().HasClue("Playing Cards"))
        {
            this.Count.SetTree("CountWithPlayingCards");
        }
        if (this.MyPlayer.GetNotebook().HasClue("Detective Story"))
        {
            this.Writer.SetTree("WriterWithStory");
        }
        if (this.MyPlayer.MadeAgreementWithReporter())
        {
            this.Businessman.SetTree("BusinessmanReporterAgreement");
        }

        if (this.TimeHasPassed())
        {
            this.Maid.SetTree("MaidCanAccuse");
            this.Reporter.SetTree("ReporterCanAccuse");
            this.Butler.SetTree("ButlerCanAccuse");
            this.Doctor.SetTree("DoctorCanAccuse");
            this.Chef.SetTree("ChefCanAccuse");
            this.Count.SetTree("CountCanAccuse");
            this.Writer.SetTree("WriterCanAccuse");
            if (this.MyPlayer.GetNotebook().GetClues().Count >= 28)
            {
                this.Businessman.SetTree("BusinessmanCanAccuse");
            }
        }

    }
    /// <summary>
    /// Checks if Enough turns have passed so you can accuse the murderer.
    /// </summary>
    /// <returns></returns>
    private bool TimeHasPassed()
    {
        return (Turn.GetTurn() >= 15 && this.MyPlayer.GetNotebook().GetClues().Count > 26);
    }
}