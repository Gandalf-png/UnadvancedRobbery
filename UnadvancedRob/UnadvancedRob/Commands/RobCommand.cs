using Rocket.API;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Steamworks;

namespace UnadvancedRobPlugin.Commands
{
    internal class RobCommand : IRocketCommand
    {
        public AllowedCaller AllowedCaller => AllowedCaller.Player;
        public string Name => "Rob";

        public string Help => "Rob";

        public string Syntax => "Syntax: Rob <Player>";

        public List<string> Aliases => new List<string>();

        public List<string> Permissions => new List<string>() { "UnadvancedRobPlugin.Rob" };

        public void Execute(IRocketPlayer caller, string[] command)
        {
            UnturnedPlayer player = (UnturnedPlayer)caller;
            if (command.Length != 1)
            {
                UnturnedChat.Say(caller, Syntax);
                return;
            }
            var target = UnturnedPlayer.FromName(command[0]);

            if (target?.Player == null)
            {
                UnturnedChat.Say(caller, "Couldn't Find Player: " + command[0]);
                return;
            }

            ulong targetID = target.CSteamID.m_SteamID;
            if(UnadvancedRobPlugin.Instance.robberies.ContainsKey(player.CSteamID.m_SteamID))
            {
                UnturnedChat.Say(UnadvancedRobPlugin.Instance.Translate("Robbing"));
                return;
            }
            if (UnadvancedRobPlugin.Instance.robberies.ContainsKey(target.CSteamID.m_SteamID))
            {
                UnturnedChat.Say(UnadvancedRobPlugin.Instance.Translate("GettingRobbed"));
                return;
            }
            UnadvancedRobPlugin.Instance.robberies.Add(player.CSteamID.m_SteamID, targetID);

            UnturnedChat.Say(UnadvancedRobPlugin.Instance.Translate("RobStart", caller.DisplayName, target.DisplayName));
        }
    }
}