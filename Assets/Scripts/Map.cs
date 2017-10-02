using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Map : MonoBehaviour
{
    PointerController StudyBtn;
    PointerController LoungeBtn;
    PointerController LibraryBtn;
    PointerController LobbyBtn;
    PointerController DiningBtn;
    PointerController ServantBtn;
    PointerController BedBtn;
    PointerController KitchenBtn;
    public RawImage Lobbyimg;
    public RawImage Loungeimg;
    public RawImage Dinnerimg;
    public RawImage Kitchenimg;
    public RawImage Bedimg;
    public RawImage Libraryimg;
    public RawImage Servantimg;
    public RawImage Studyimg;
    Room Lobby;
    Room Dinner;
    Room Kitchen;
    Room Lounge;
    Room Bed;
    Room Study;
    Room Library;
    Room Servant;
    Room Current;
    public void Createmap()
    {
        //Lobbyimg.texture = (Texture)Resources.Load("Mansion_Lobby_2", typeof(Texture));
        //Loungeimg.texture = (Texture)Resources.Load("Mansion_Lounge_2", typeof(Texture));
        //Servantimg.texture = (Texture)Resources.Load("Mansion_Servant_Room", typeof(Texture));
        //Libraryimg.texture = (Texture)Resources.Load("Mansion_Library", typeof(Texture));
        //Kitchenimg.texture = (Texture)Resources.Load("Mansion_Kitchen", typeof(Texture));
        //Bedimg.texture = (Texture)Resources.Load("Mansion_Bedroom", typeof(Texture));
        //Dinnerimg.texture = (Texture)Resources.Load("Mansion_Dining room_2", typeof(Texture));
        //Studyimg.texture = (Texture)Resources.Load("Mansion_Study", typeof(Texture));
        Lobby = new Room("Mansion_Lobby_2", "Entrance Hall");
        List<Clue> temp = new List<Clue>
        {
            new Clue("test", "Testclue")
        };
        Lobby.SetClues(temp);
        Lounge = new Room("Mansion_Lounge_2", "Lounge");
        Servant = new Room("Mansion_Servant_Room", "Servant");
        Library = new Room("Mansion_Library", "Library");
        Kitchen = new Room("Mansion_Kitchen", "Kitchen");
        Bed = new Room("Mansion_Bedroom", "Bed");
        Dinner = new Room("Mansion_Dining room_2", "Dining Room");
        Study = new Room("Mansion_Study", "Study");
        Current = new Room(null, null);
        StudyBtn = (PointerController)GameObject.Find("BtnStudy").GetComponent<PointerController>();
        LibraryBtn = (PointerController)GameObject.Find("BtnLibrary").GetComponent<PointerController>();
        DiningBtn = (PointerController)GameObject.Find("BtnDiningroom").GetComponent<PointerController>();
        LoungeBtn = (PointerController)GameObject.Find("BtnLounge").GetComponent<PointerController>();
        LobbyBtn = (PointerController)GameObject.Find("BtnLobby").GetComponent<PointerController>();
        ServantBtn = (PointerController)GameObject.Find("BtnServantroom").GetComponent<PointerController>();
        BedBtn = (PointerController)GameObject.Find("BtnBedroom").GetComponent<PointerController>();
        KitchenBtn = (PointerController)GameObject.Find("BtnKitchen").GetComponent<PointerController>();

    }

    public Room Mapclick(Room Location)
    {
        this.Current = Location;
        if (StudyBtn.GetPointerDown())
        {
            return Study;
        }
        if (LobbyBtn.GetPointerDown())
        {
            return Lobby;
        }
        if (LibraryBtn.GetPointerDown())
        {
            return Library;
        }
        if (KitchenBtn.GetPointerDown())
        {
            return Kitchen;
        }
        if (BedBtn.GetPointerDown())
        {
            return Bed;
        }
        if (ServantBtn.GetPointerDown())
        {
            return Servant;
        }
        if (DiningBtn.GetPointerDown())
        {
            return Dinner;
        }
        if (LoungeBtn.GetPointerDown())
        {
            return Lounge;
        }
        else
        {
            return Current;
        }
    }

    public Room GetStartLocation()
    {
        return Lobby;
    }
}
