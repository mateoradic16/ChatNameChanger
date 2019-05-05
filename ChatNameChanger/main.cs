using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rocket.Unturned.Player;
using Rocket.API;
using Rocket.Core.Plugins;
using Rocket.Core.Logging;
using Rocket.Core.Commands;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Events;
using Rocket.Unturned.Extensions;
using Rocket.Unturned.Items;
using SDG.Unturned;
using Rocket.API.Collections;
using Rocket.Core;
using Rocket.Unturned;
using UnityEngine;
using Steamworks;
using Logger = Rocket.Core.Logging.Logger;
using System.Collections;
using System.Threading;
using Rocket.API.Serialisation;

namespace ChatNameChanger
{
    class main : RocketPlugin<configuration>
    {
        public static main Instance;
        public override TranslationList DefaultTranslations
        {
            get
            {
                return new TranslationList()
                {
                    {"changed_name_pub", "{0} changed his chat name into {1}!"},
                    {"changed_name_priv", "You changed your name into {0}!"},
                    {"changed_name_other_priv1", "You have changed {0}'s name into {1}!" },
                    {"changed_name_other_priv2", "Your name has been changed into {0} by {1}!" },
                    {"changed_name_other", "{0} changed {1}'s name into {2}!" },
                    {"missing_params", "You must specify your new name! Usage: /cn <optional player> \"new name\"" }
                };
            }
        }
        protected override void Load()
        {
            Instance = this;
            base.Load();
            Logger.LogWarning("Chat Name Changer loaded!");

            UnturnedPlayerEvents.OnPlayerChatted += UnturnedPlayerEvents_OnPlayerChatted;
        }

        private void UnturnedPlayerEvents_OnPlayerChatted(UnturnedPlayer player, ref Color color, string message, EChatMode chatMode, ref bool cancel)
        {

            RocketPermissionsGroup test = R.Permissions.GetGroup("default");
            foreach (var t in R.Permissions.GetGroups(player, true))
            {
                if (test.Priority > t.Priority)
                {
                    test = t;
                }
            }

            if(Configuration.Instance.Enabled)
            {
                player.SteamPlayer().playerID.characterName = test.Prefix + player.SteamPlayer().playerID.nickName + test.Suffix;
            }
        }

        protected override void Unload()
        {
            UnturnedPlayerEvents.OnPlayerChatted -= UnturnedPlayerEvents_OnPlayerChatted;
            Logger.LogWarning("Chat Name Changer unloaded!");
            base.Unload();
        }
    }
}
