using Rocket.API;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Steamworks;
using static UnityEngine.GraphicsBuffer;
using SDG.Unturned;

namespace UnadvancedRobPlugin.Commands
{
    internal class OverCommand : IRocketCommand
    {
        public AllowedCaller AllowedCaller => AllowedCaller.Player;
        public string Name => "stoprob";

        public string Help => "Stopping an active robbery";

        public string Syntax => "Syntax: /stoprob <player>";

        public List<string> Aliases => new List<string>();

        public List<string> Permissions => new List<string>() { "UnadvancedRobPlugin.Rob" };

        public void Execute(IRocketPlayer caller, string[] command)

        {
            UnturnedPlayer player = (UnturnedPlayer)caller;

            if (UnadvancedRobPlugin.Instance.robberies.ContainsKey(player.CSteamID.m_SteamID))
            {
                var target = UnturnedPlayer.FromCSteamID((CSteamID)UnadvancedRobPlugin.Instance.robberies[player.CSteamID.m_SteamID]);
                if(target?.Player == null) 
                {
                    UnturnedChat.Say(UnadvancedRobPlugin.Instance.Translate("Robbing"));
                    return;
                }
                UnadvancedRobPlugin.Instance.robberies.Remove(player.CSteamID.m_SteamID);
                UnturnedChat.Say(UnadvancedRobPlugin.Instance.Translate("StopRob", player.DisplayName, target.DisplayName));
                return;
            }

            UnturnedChat.Say(UnadvancedRobPlugin.Instance.Translate("InactiveRob"));
        }
    }
}