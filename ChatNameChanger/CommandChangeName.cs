using Rocket.API;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ChatNameChanger
{
    class CommandChangeName : IRocketCommand
    {
        public AllowedCaller AllowedCaller
        {
            get
            {
                return AllowedCaller.Both;
            }
        }

        public string Name
        {
            get
            {
                return "changename";
            }
        }

        public string Help
        {
            get
            {
                return "Change your ingame name!";
            }
        }

        public List<string> Aliases
        {
            get
            {
                return new List<string>()
                {
                    "cn"
                };
            }
        }
        public string Syntax
        {
            get
            {
                return "<optional player> \"new name\"";
            }
        }
        public List<string> Permissions
        {
            get
            {
                return new List<string>() { };
            }
        }

        public void Execute(IRocketPlayer caller, string[] command)
        {
            if(caller.HasPermission("changename"))
            {
                UnturnedPlayer player = (UnturnedPlayer)caller;
                if (command.Length == 0)
                {
                    UnturnedChat.Say(caller, main.Instance.Translate("missing_params"), Color.red);
                }
                else if (command.Length == 1)
                {
                    if (caller.Id == "Console")
                    {
                        UnturnedChat.Say(caller, "This command can't be executed in console!", Color.red);
                    }
                    else
                    {
                        if (caller.HasPermission("changename"))
                        {
                            UnturnedChat.Say(caller, main.Instance.Translate("changed_name_priv", command[0]), Color.yellow);
                            if (main.Instance.Configuration.Instance.Announce_Name_Change)
                            {
                                UnturnedChat.Say(main.Instance.Translate("changed_name", player.CharacterName, command[0]), Color.yellow);
                            }
                            player.SteamPlayer().playerID.characterName = command[0];
                        }
                    }
                }
                else if (command.Length == 2)
                {
                    if (caller.HasPermission("changename.other"))
                    {
                        UnturnedPlayer p = UnturnedPlayer.FromName(command[0]);
                        UnturnedChat.Say(caller, main.Instance.Translate("changed_name_other_priv1", p.CharacterName, command[1]), Color.yellow);
                        UnturnedChat.Say(caller, main.Instance.Translate("changed_name_other_priv2", command[1], player.CharacterName), Color.yellow);
                        if (main.Instance.Configuration.Instance.Announce_Name_Change)
                        {
                            UnturnedChat.Say(main.Instance.Translate("changed_name_other", player.CharacterName, p.CharacterName, command[1]), Color.yellow);
                        }

                        p.SteamPlayer().playerID.characterName = command[1];
                    }
                    else
                    {
                        UnturnedChat.Say(caller, "You do not have permission to change other's name!", Color.red);
                    }
                }
            }
            else
            {
                UnturnedChat.Say(caller, "You do not have permission to execute that command!", Color.red);
            }
        }
    }
}
