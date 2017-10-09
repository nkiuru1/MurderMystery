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
        Butler.SetClue(new Clue("Butler", "What an asshole"));
        Noble.SetTree("TestCharacter");
        Noble.SetData("Noble", Lobby);
        Noble.SetClue(new Clue("Noble", "I bet he's worth millions"));

        List<Character> roomChars = new List<Character>
        {
            Butler, Noble
        };
        Lobby.SetCharacters(roomChars);
        List<Clue> temp = new List<Clue>
        {
            new Clue("Lighter", "a Standard lighter. It's probably more useful to you then it's previous owner.")
        };
        Lobby.SetClues(temp);

        Lounge = new Room("Lounge");
        List<Clue> temp2 = new List<Clue>
        {
            new Clue("Diary","The most recent entry mentions something about a debt?" +
            " The rest is mostly complaints of a single man. You regret reading them."),
            new Clue("Sword Cane","A cane of a gentleman. Has  a small blade hidden inside." +
            " There is some rust on the blade. It must have not been used for some time."),
            new Clue("Playing Cards","This deck has seen quite a bit of use. " +
            "Perhaps the victim enjoyed gambling? The joker seems to be missing.")
        };
        Lounge.SetClues(temp2);

        Servant = new Room("Servant");
        List<Clue> temp3 = new List<Clue>
        {
            new Clue("Boot","Doesn't seem relevant in any way, but you never know, It might even be the culprit. " +
            "You seriously doubt it."),
            new Clue("Study Room Key","A key to the study.")
        };
        Servant.SetClues(temp3);

        Library = new Room("Library");
        List<Clue> temp4 = new List<Clue>
        {
            new Clue("Detective Story","A story about an amnesiac detective that tries to solve a murder, " +
            "only to find he himself was the culprit." +
            " It seems almost ironic that you decided to pick this book.")
        };
        Library.SetClues(temp4);

        Kitchen = new Room("Kitchen");
        List<Clue> temp5 = new List<Clue>
        {
            new Clue("Thin Blade", "Very thin blade. Has traces of poison in it. " +
            "It would be very hard to notice a wound created by this blade."),
            new Clue("Rat Poison", "Half empty bottle of rat poison, found in the kitchen." +
            " Either the chef had a rat problem, or another kind of problem. " +
            "Either way, the poison seems to have done it's job.")
        };
        Kitchen.SetClues(temp5);

        Bed = new Room("Bed");
        List<Clue> temp6 = new List<Clue>
        {
             new Clue("Syringe","a small syringe made for injecting drugs, or perhaps poison? " +
             "You might want to show this to someone with more information."),
             new Clue("Letter","According to this, the victim arranged a meeting with a business partner." +
             " The meeting was at four o'clock today, just before the dinner party. "),
             new Clue("Revolver(empty)", "It doesn't seem to have been used recently. Besides, the victim clearly wasn't shot." +
             " However, it might be useful, especially if you happen upon some bullets."),
             new Clue("IOU","Someone had a large debt to the victim. A clear motive, at the very least.")
        };
        Bed.SetClues(temp6);

        Dinner = new Room("Dining Room");
        List<Clue> temp7 = new List<Clue>
        {
            new Clue("Improvised Autopsy","The victim seems to have died of poisoning. " +
            "No clearly visible wounds, but there are few drops of blood on his shirt. "),
            new Clue("Wine Glass","The glass used by the victim. Could there have been poison in it? " +
            "Without proper tools, it would be impossible to tell.")
        };
        Dinner.SetClues(temp7);

        Study = new Room("Study");
        List<Clue> temp8 = new List<Clue>
        {
            new Clue("Victim's Will","Testament of the victim. Either a real one, or extremely well made forgery. " +
            "It might be hard to tell which even with proper tools, which you lack."),
            new Clue("Pocket Watch","It seems to have stopped working. The hands are pointing to quarter past four." +
            " Perhaps there is some meaning in the time?"),
            new Clue("Bedroom Key","A key to the bedroom.")
        };
        Study.SetClues(temp8);
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