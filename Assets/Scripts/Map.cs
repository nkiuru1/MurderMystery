﻿using System.Collections;
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
    Room Lobby;
    Room Dinner;
    Room Kitchen;
    Room Lounge;
    Room Bed;
    Room Study;
    Room Library;
    Room Servant;
    Room Current;
    public Character Butler;
    public Character Noble;

    public void Createmap()
    {
        Lobby = new Room("Entrance Hall");
        Butler.SetTree("Prologue");
        Butler.SetData("Butler", Lobby);
        Noble.SetTree("TestCharacter");
        Noble.SetData("Noble", Lobby);
        List<Character> roomChars = new List<Character>
        {
            Butler, Noble
        };
        Lobby.SetCharacters(roomChars);
        List<Clue> temp = new List<Clue>
        {
            new Clue("test", "Testclue")
        };
        Lobby.SetClues(temp);
        Lounge = new Room("Lounge");
        List<Clue> temp2 = new List<Clue>
        {
            new Clue("knife", "testKnife")
        };
        Lounge.SetClues(temp2);
        Servant = new Room("Servant");
        Library = new Room("Library");
        Kitchen = new Room("Kitchen");
        Bed = new Room("Bed");
        Dinner = new Room("Dining Room");
        List<Clue> temp3 = new List<Clue>
        {
            new Clue("Wine Glass", "This is a Wine glass")
        };
        Dinner.SetClues(temp3);
        Study = new Room("Study");
        Current = new Room(null);

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
        return this.Lobby;
    }

    public Room GetRoomObject(string name)
    {
        if (name.Equals("Lobby")) return Lobby;
        if (name.Equals("Dinner")) return Dinner;
        if (name.Equals("Kitchen")) return Lobby;
        if (name.Equals("Lounge")) return Lounge;
        if (name.Equals("Bed")) return Bed;
        if (name.Equals("Study")) return Study;
        if (name.Equals("Library")) return Library;
        if (name.Equals("Servant")) return Servant;
        return null;
    }

    public void TransportCharacter(Character Person, Room TargetRoom)
    {
        Person.GetLocation().RemoveCharacter(Person.GetName());
        TargetRoom.BringCharacterIntoThisRoom(Person);
        Person.ChangeLocation(TargetRoom);
    }
}