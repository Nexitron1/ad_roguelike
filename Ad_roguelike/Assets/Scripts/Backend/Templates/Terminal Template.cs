using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TerminalTemplate : MonoBehaviour
{
    Character ch;
    ItemsHolder ih;
    public TMP_Text mainText, inputText;
    public string content, inputLine;
    public List<string> PreviousCommands = new List<string>();
    public int commandIndex;
    void Start()
    {
        ch = Camera.main.GetComponent<Character>();
        ih = Camera.main.GetComponent<ItemsHolder>();
        string InitMessage = "AD_XP version " + Application.version;
        
        content = InitMessage;
        ch.SetTT(this);
    }

    void Update()
    {
        mainText.text = content;
        CheckInputs();
        inputText.text = inputLine;

        if (Input.GetKey(KeyCode.LeftControl))
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                commandIndex -= 1;
                if(commandIndex < 0) commandIndex = 0;
                if(commandIndex >= PreviousCommands.Count) commandIndex = PreviousCommands.Count - 1;
                if(PreviousCommands.Count > 0)
                    inputLine = PreviousCommands[commandIndex];
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                commandIndex += 1;
                if (commandIndex < 0) commandIndex = 0;
                if (commandIndex >= PreviousCommands.Count) commandIndex = PreviousCommands.Count - 1;
                if (PreviousCommands.Count > 0)
                    inputLine = PreviousCommands[commandIndex];

            }
        }
    }

    void CheckInputs()
    {
        if (Input.GetKeyDown(KeyCode.Q)) inputLine += "q";
        if (Input.GetKeyDown(KeyCode.W)) inputLine += "w";
        if (Input.GetKeyDown(KeyCode.E)) inputLine += "e";
        if (Input.GetKeyDown(KeyCode.R)) inputLine += "r";
        if (Input.GetKeyDown(KeyCode.T)) inputLine += "t";
        if (Input.GetKeyDown(KeyCode.Y)) inputLine += "y";
        if (Input.GetKeyDown(KeyCode.U)) inputLine += "u";
        if (Input.GetKeyDown(KeyCode.I)) inputLine += "i";
        if (Input.GetKeyDown(KeyCode.O)) inputLine += "o";
        if (Input.GetKeyDown(KeyCode.P)) inputLine += "p";

        if (Input.GetKeyDown(KeyCode.A)) inputLine += "a";
        if (Input.GetKeyDown(KeyCode.S)) inputLine += "s";
        if (Input.GetKeyDown(KeyCode.D)) inputLine += "d";
        if (Input.GetKeyDown(KeyCode.F)) inputLine += "f";
        if (Input.GetKeyDown(KeyCode.G)) inputLine += "g";
        if (Input.GetKeyDown(KeyCode.H)) inputLine += "h";
        if (Input.GetKeyDown(KeyCode.J)) inputLine += "j";
        if (Input.GetKeyDown(KeyCode.K)) inputLine += "k";
        if (Input.GetKeyDown(KeyCode.L)) inputLine += "l";

        if (Input.GetKeyDown(KeyCode.Z)) inputLine += "z";
        if (Input.GetKeyDown(KeyCode.X)) inputLine += "x";
        if (Input.GetKeyDown(KeyCode.C)) inputLine += "c";
        if (Input.GetKeyDown(KeyCode.V)) inputLine += "v";
        if (Input.GetKeyDown(KeyCode.B)) inputLine += "b";
        if (Input.GetKeyDown(KeyCode.N)) inputLine += "n";
        if (Input.GetKeyDown(KeyCode.M)) inputLine += "m";

        if (Input.GetKeyDown(KeyCode.Backspace)) Backspace();
        if (Input.GetKeyDown(KeyCode.Return)) Enter();
        if (Input.GetKeyDown(KeyCode.Slash)) inputLine += "/";
        if (Input.GetKeyDown(KeyCode.Space)) inputLine += " ";

        if (Input.GetKeyDown(KeyCode.Alpha0)) inputLine += "0";
        if (Input.GetKeyDown(KeyCode.Alpha1)) inputLine += "1";
        if (Input.GetKeyDown(KeyCode.Alpha2)) inputLine += "2";
        if (Input.GetKeyDown(KeyCode.Alpha3)) inputLine += "3";
        if (Input.GetKeyDown(KeyCode.Alpha4)) inputLine += "4";
        if (Input.GetKeyDown(KeyCode.Alpha5)) inputLine += "5";
        if (Input.GetKeyDown(KeyCode.Alpha6)) inputLine += "6";
        if (Input.GetKeyDown(KeyCode.Alpha7)) inputLine += "7";
        if (Input.GetKeyDown(KeyCode.Alpha8)) inputLine += "8";
        if (Input.GetKeyDown(KeyCode.Alpha9)) inputLine += "9";

    }

    public bool AnimateCursor = true;
    IEnumerator Cursor()
    {
        content += "\n";
        while (AnimateCursor)
        {
            content += "_";
            yield return new WaitForSecondsRealtime(0.5f);
            string content2 = "";
            for (int i = 0; i < content.Length - 1; i++)
            {
                content2 += content[i];
            }
            content = content2;
            yield return new WaitForSecondsRealtime(0.5f);
        }
    }

    private void Backspace()
    {
        string il2 = "";
        for (int i = 0; i < inputLine.Length - 1; i++)
        {
            il2 += inputLine[i];
        }
        inputLine = il2;
    }
    void Enter()
    {
        string str = ParseCommand(inputLine);
        content += str;
        PreviousCommands.Add(inputLine);
        inputLine = "";
        commandIndex++;
    }

    bool CheatCheck()
    {
        return ch.cheats;
    }
    string ParseCommand(string command)
    {
        if (command[0] != '/') return command + "\n";
        

        string[] args = command.Split(' ');

        switch (args[0])
        {
            case "/help":
                if (args.Length > 1)
                {
                    switch (args[1])
                    {
                        case "give":
                            return "/give [name] [rarity 0 - 3] [amount]" + "\n";
                    }

                    return "command not existed\n";
                }
                else
                {
                    return "/give     - give item\n" +
                           "/giveact  - give act item\n" +
                           "/money    - add money\n" +
                           "/heal     - add health\n" +
                           "/overheal - add overhp\n" +
                           "/skip     - skip ad\n" +
                           "/autoskip - autoskip ad\n" +
                           "/clear    - clear terminal\n";
                }
            case "/clear":
                int lines = 0;
                if (args.Length > 1)
                {
                    int.TryParse(args[1], out lines);
                    if (lines == 0)
                    {
                        content = "";
                        return "";
                    }
                    var c = content.Split('\n');
                    string c2 = "";
                    content = "";
                    for (int a = 0; a < c.Length - lines - 1; a++)
                    {
                        c2 += c[a];
                        c2 += "\n";
                    }
                    return c2;
                }
                else
                {
                    content = "";
                    return "";
                }
            case "/logo":
                return "  A  DD     X X PP   tm\n" +
                       " A A D D    X X P P\n" +
                       " AAA D D     X  PP \n" +
                       " A A DD ___ X X P  \n";
            case "/cheats":
                ch.cheats = !ch.cheats;
                if (ch.cheats) return "cheats enabled\n";
                return "cheats disabled\n";

            case "/money":
                if(!CheatCheck()) return "cheats are disabled \ntype /cheats to enable\n";
                int arg = 0;
                if (args.Length > 1)
                {
                    int.TryParse(args[1], out arg);
                    ch.money += arg;
                }
                return "added " + arg + "m\n";
            case "/heal":
                if (!CheatCheck()) return "cheats are disabled \ntype /cheats to enable\n";
                int arg2 = 0;
                if (args.Length > 1)
                    int.TryParse(args[1], out arg2);
                ch.Heal(arg2);
                return "healed " + arg2 + " hp\n";
            case "/overheal":
                if (!CheatCheck()) return "cheats are disabled \ntype /cheats to enable\n";
                int arg3 = 0;
                if (args.Length > 1)
                    int.TryParse(args[1], out arg3);
                ch.OverHealth += arg3;
                return "overhealed " + arg3 + " hp\n";
            case "/skip":
                if (!CheatCheck()) return "cheats are disabled \ntype /cheats to enable\n";
                ch.SkipAd();
                return "skipped ad\n";
            case "/autoskip":
                if (!CheatCheck()) return "cheats are disabled \ntype /cheats to enable\n";
                if(args.Length > 1)
                {
                    switch (args[1])
                    {
                        case "true":
                            ch.autoSkip = true;
                            break;
                        case "false":
                            ch.autoSkip = false;
                            break;
                        case "check":
                            return "autoskip is " + ch.autoSkip + "\n";
                    }
                }
                else
                {
                    ch.autoSkip = !ch.autoSkip;             
                }
                return "autoskip setted " + ch.autoSkip + "\n";
            case "/give":
                if (!CheatCheck()) return "cheats are disabled \ntype /cheats to enable\n";
                if (args.Length < 2) return "syntaxis error\n";
                int rarity1 = 0;
                if(args.Length > 2) int.TryParse(args[2], out rarity1);
                if (rarity1 < 0) rarity1 = 0;
                rarity1 %= 4;

                int count1 = 1;
                if (args.Length > 3) int.TryParse(args[3], out count1);
                if (count1 < 0) count1 = 0;

                string[] pass1 = Enum.GetNames(typeof(Item.Functional));                
                for (int i = 0; i < pass1.Length; i++)
                {
                    pass1[i] = pass1[i].ToLower();
                    if (pass1[i] == args[1])
                    {
                        for (int j = 0; j < count1; j++)
                        {
                            ch.AddItem(ih.CreateItem(Enum.Parse<Item.Functional>(args[1]), (Item.Rarity)rarity1));
                        }
                        return "added " + Enum.GetNames(typeof(Item.Rarity))[rarity1] + " " + args[1] + " " + count1 + "pc.\n";
                    }
                }
                

                return "unknown error\n";
            case "/giveact":
                if (!CheatCheck()) return "cheats are disabled \ntype /cheats to enable\n";
                if (args.Length < 2) return "syntaxis error\n";

                string[] Act = Enum.GetNames(typeof(ActiveItem.Functional));
                for (int i = 0; i < Act.Length; i++)
                {
                    Act[i] = Act[i].ToLower();
                    if (Act[i] == args[1])
                    {
                        ch.AddActiveItem(ih.CreateActiveItem(Enum.Parse<ActiveItem.Functional>(args[1])));
                        return "added " + args[1] + "\n";
                    }
                }


                return "unknown error\n";


            default:
                return "unknown command\n";
        }
        
    }

   
    
}
