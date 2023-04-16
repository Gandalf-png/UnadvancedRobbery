using Rocket.API;
using Rocket.Core.Plugins;
using System;
using Logger = Rocket.Core.Logging.Logger;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rocket.Unturned;
using Rocket.Unturned.Events;
using Rocket.Unturned.Player;
using SDG.Unturned;
using Steamworks;
using UnityEngine;
using Rocket.Unturned.Chat;
using Rocket.API.Collections;

namespace UnadvancedRobPlugin
{
    public class UnadvancedRobPlugin : RocketPlugin<UnadvancedRobPluginConfig>
    {
        public static UnadvancedRobPlugin Instance;
        public Dictionary<ulong, ulong> robberies = new Dictionary<ulong, ulong>();

        protected override void Load()
        {
            Instance = this;
            Logger.Log("UnadvancedRobPlugin Loaded", ConsoleColor.Yellow);
            Logger.Log("Discord: https://discord.gg/NqJ2XDh", ConsoleColor.Yellow);

            UnturnedPlayerEvents.OnPlayerDeath += OnPlayerDeath;
        }
        public AllowedCaller AllowedCaller => AllowedCaller.Player;
        private void OnPlayerDeath(UnturnedPlayer victim, EDeathCause cause, ELimb limb, CSteamID murderer)
        {

            if (robberies.ContainsKey(victim.CSteamID.m_SteamID))

            {
                UnturnedPlayer player = UnturnedPlayer.FromCSteamID(murderer);
                string result = player?.Player == null ? "something" : player.DisplayName;
                UnturnedChat.Say(UnadvancedRobPlugin.Instance.Translate("TargetDeath", victim.DisplayName, result));
                UnadvancedRobPlugin.Instance.robberies.Remove(victim.CSteamID.m_SteamID);
            }

            if (murderer == null)
            {
                return;
            }
            var robber = UnturnedPlayer.FromCSteamID(murderer);

            if (robberies.ContainsKey(murderer.m_SteamID | victim.CSteamID.m_SteamID))
            {
                UnturnedChat.Say(UnadvancedRobPlugin.Instance.Translate("WhatDidIDo", victim.DisplayName, UnturnedPlayer.FromCSteamID(murderer).DisplayName));
                UnadvancedRobPlugin.Instance.robberies.Remove(murderer.m_SteamID);
            }
        }

        protected override void Unload()
        {
            UnturnedPlayerEvents.OnPlayerDeath -= OnPlayerDeath;
            Logger.Log("UnadvancedRobPlugin unloaded");
        }

        public override TranslationList DefaultTranslations => new TranslationList()
        {
            { "RobStart", "{0} is now robbing {1}!"},
            { "Robbing", "You are already Robbing someone!" },
            { "GettingRobbed", "You are already getting Robbed by someone!" },
            { "TargetDeath", "{0} has died. The Robbery is now over. They got murdered by {1}" },
            { "WhatDidIDo", "{0} has died while getting robbed by {1}!" },
            { "InactiveRob", "You are currently not in an active robbery!" },
            { "StopRob", "{0} has stopped robbing {1}" }
        };
    }
}
