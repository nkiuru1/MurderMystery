using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
    public ButtonHandler Buttons;
    public RawImage BackgroundPicture;
    public Map GameMap;
    public Canvas TestCanvas, TestCanvas2;
    Player MyPlayer;
    Room TestRoom;
    Clue TestClue;
    Room Location;
    private string BackgroundPath;

    // Use this for initialization
    void Start () {
        BackgroundPicture.texture = (Texture)Resources.Load("Lobby", typeof(Texture));
        BackgroundPath = "Lobby";
        this.TestRoom = new Room("Lobby","test room");
        Notebook testNotebook = new Notebook();
        GameMap.Createmap();
        this.MyPlayer = new Player("TestDude", this.TestRoom, testNotebook);
        this.Buttons.CreateDefaultButtons(testNotebook);
        this.InitializeClue("BrokenGlass", "Broken Glass", "Wonder how this was broken?");
        this.InitializeClue("BrokenGlass", "Broken Glass1", "Wonder how this was broken?");
        this.InitializeClue("BrokenGlass", "Broken Glass2", "Wonder how this was broken?");
        this.InitializeClue("BrokenGlass", "Broken Glass3", "Wonder how this was broken?");
        this.Location = new Room(null,null);
    }
	
    private void InitializeClue(string resourceName,string name,string description)
    {
        RawImage ClueBackground = GameObject.Find("Clue").GetComponent<RawImage>();
        ClueBackground.texture = (Texture)Resources.Load(resourceName, typeof(Texture));
        this.TestClue = new Clue(name, description, ClueBackground);
        this.MyPlayer.GetNotebook().AddClue(this.TestClue);
    }

	// Update is called once per frame
	void Update ()
    {
        this.Buttons.Clicked();
        Location = this.GameMap.Mapclick(Location);
        if (BackgroundPath != Location.GetBackground() && Location.GetBackground() != null)
        {
            BackgroundPicture.texture = (Texture)Resources.Load(Location.GetBackground(), typeof(Texture));
        }   
    }
}
