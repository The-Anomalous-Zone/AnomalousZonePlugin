using Exiled.API.Features;
using Exiled.Events.EventArgs.Map;
using Subtitles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exiled.API.Enums;
using MEC;
using Exiled.Events.Commands.Reload;
using Exiled.Loader;
using Mono.Cecil.Cil;
using static PlayerRoles.Spectating.SpectatableModuleBase;
using System.Security.Cryptography;
using CustomPlayerEffects;
using System.Security.Policy;
using Exiled.Events.EventArgs.Player;
using PlayerRoles;
using Exiled.API.Extensions;
using UnityEngine;

namespace AnomalousZonePlugin.EventHandlers
{
    // Fun Fact!
    // Modifying the normal C.A.S.S.I.E messages 
    // Violates NW's VSR
    // I didn't know I COULDN'T EVEN ADD COLOR TO IT
    // WHY Northwood
    // (probably because mulitple messages don't work correctly anymore)
    // Speaking of the VSR HOW COULD YOU EVEN VIOLATE 3.8
    // IT WOULDN'T EVEN LOAD IT
    // This code was written so unoptimized and horribly and I could have done better
    // But I didn't care Pleaes don't look at it

    internal sealed class ImprovedCassie
    {
        private Plugin plugin;
        public ImprovedCassie(Plugin plugin) => this.plugin = plugin;
        public void OnAnnoucingDecontaim(AnnouncingDecontaminationEventArgs ev)
        {
            if (ev.State == DecontaminationState.Start)
            {
                Cassie.Clear();
                Cassie.MessageTranslated("attention all personnel", "<b>Attention!</b> All personnel.", false, false, true);
                Cassie.MessageTranslated("the light containment zone decontamination process will occur in T minus 15 minutes",
                    "The Light Containment Zone <color=#4c5444>decontamination</color> process will occur in <color=red>T-15 minutes</color>.", false, false, true);
                Cassie.MessageTranslated("all biological substances must be removed in order to avoid destruction",
                    "All biological substances must be <b><color=red>removed</color></b> in order to avoid <color=red>destruction</color>.", false, false, true);
            }
            else if (ev.State == DecontaminationState.Remain10Minutes)
            {
                Cassie.Clear();
                Cassie.MessageTranslated("danger light containment zone overall decontamination in T minus 10 minutes",
                    "<b><color=red>Danger</color></b>, Light Containment Zone overall <color=#4c5444>decontamination</color> in <color=red>T-10 minutes</color>.", false, false, true);
            }
            else if (ev.State == DecontaminationState.Remain5Minutes)
            {
                Cassie.Clear();
                Cassie.MessageTranslated("danger light containment zone overall decontamination in T minus 5 minutes",
                    "<b><color=red>Danger</color></b>, Light Containment Zone overall <color=#4c5444>decontamination</color> in <color=red>T-5 minutes</color>.", false, false, true);
            }
            else if (ev.State == DecontaminationState.Remain1Minute)
            {
                Cassie.Clear();
                Cassie.MessageTranslated("danger light containment zone overall decontamination in T minus 1 minute",
                    "<b><color=red>Danger</color></b>, Light Containment Zone overall <color=#4c5444>decontamination</color> in <color=red>T-1 minute</color>.", false, false, true);
            }
            else if (ev.State == DecontaminationState.Countdown)
            {
                Cassie.Clear();
                Cassie.MessageTranslated("danger light containment zone overall decontamination in T minus 30 seconds",
                    "<b><color=red>Danger</color></b>, Light Containment Zone overall <color=#4c5444>decontamination</color> in <color=red>T-30 seconds</color>.", false, false, true);
                Cassie.MessageTranslated("all checkpoint doors have been permanently opened please evacuate immediately",
                    "All checkpoint doors have been permanently <b>opened</b>. Please <b><color=red>evacuate immediately</color></b>.");
            }
            else if (ev.State == DecontaminationState.Lockdown)
            {
                Cassie.Clear();
                Cassie.MessageTranslated("light containment zone is locked down and ready for decontamination",
                    "Light Containment Zone is <color=red>locked down</color> and ready for <color=#4c5444>decontamination</color>.", false, false, true);
                Cassie.MessageTranslated("the removal of organic substances has now begun",
                    "The <color=red>removal</color> of organic substances has now begun.", false, false, true);
            }
        }
        public void OnAnnouncingSCP(AnnouncingScpTerminationEventArgs ev) 
        {
            ev.IsAllowed = false;
            Log.Debug($"{ev.Player.Role.Type.GetFullName()} died to: {ev.TerminationCause}");
            string nameToShow = " ";
            string nameToSay = " ";
            if (ev.Role.Type == RoleTypeId.Scp049)
            {
                nameToShow = "<color=red>SCP-049</color>";
                nameToSay = "SCP 0 4 9";
            }
            else if (ev.Role.Type == RoleTypeId.Scp079)
            {
                nameToShow = "<color=red>SCP-079</color>";
                nameToSay = "SCP 0 7 9";
            }
            else if (ev.Role.Type == RoleTypeId.Scp096)
            {
                nameToShow = "<color=red>SCP-096</color>";
                nameToSay = "SCP 0 9 6";
            }
            else if (ev.Role.Type == RoleTypeId.Scp106)
            {
                nameToShow = "<color=red>SCP-106</color>";
                nameToSay = "SCP 1 0 6";
            }
            else if (ev.Role.Type == RoleTypeId.Scp173)
            {
                nameToShow = "<color=red>SCP-173</color>";
                nameToSay = "SCP 1 7 3";
            }
            else if (ev.Role.Type == RoleTypeId.Scp939)
            {
                nameToShow = "<color=red>SCP-939</color>";
                nameToSay = "SCP 9 3 9";
            }
            else if (ev.Role.Type == RoleTypeId.Scp3114)
            {
                nameToShow = "<color=red>SCP-3114</color>";
                nameToSay = "SCP 3 1 1 4";
            }
            else
            {
                nameToShow = "<color=orange>Go bother coollogan876 on Discord to fix this</color>";
                nameToSay = "some SCP";
            }
            if (ev.TerminationCause.Equals("SUCCESSFULLY TERMINATED . TERMINATION CAUSE UNSPECIFIED"))
            {
                Cassie.MessageTranslated($"{nameToSay} SUCCESSFULLY TERMINATED . TERMINATION CAUSE UNSPECIFIED",
                    $"{nameToShow} <color=green>successfully <b>terminated</b></color>. Termination cause <b>unspecified</b>.", false, false, true);
            }
            else if (ev.TerminationCause.Equals($"TERMINATED BY {nameToSay}"))
            {             
                Cassie.MessageTranslated($"{nameToSay} TERMINATED BY {nameToSay}",
                    $"{nameToShow} <b>terminated</b> by {nameToShow}.", false, false, true);            
            }
            else if (ev.TerminationCause.Equals("SUCCESSFULLY TERMINATED BY AUTOMATIC SECURITY SYSTEM"))
            {
                Cassie.MessageTranslated($"{nameToSay} SUCCESSFULLY TERMINATED BY AUTOMATIC SECURITY SYSTEM",
                    $"{nameToShow} <color=green>successfully <b>terminated</b></color> by Automatic Security System.", false, false, true);
            }
            else if (ev.TerminationCause.Equals("SUCCESSFULLY TERMINATED BY ALPHA WARHEAD"))
            {
                Cassie.MessageTranslated($"{nameToSay} SUCCESSFULLY TERMINATED BY THE ALPHA WARHEAD",
                    $"{nameToShow} <color=green>successfully <b>terminated</b></color> by the Alpha Warhead.", false, false, true);
            }
            else if (ev.TerminationCause.Equals("CONTAINEDSUCCESSFULLY BY SCIENCE PERSONNEL"))
            {
                Cassie.MessageTranslated($"{nameToSay} CONTAINEDSUCCESSFULLY BY SCIENCE PERSONNEL",
                    $"{nameToShow} <color=green><b>contained</b> successfully</color> by <color={ev.Attacker.Role.Type.GetColor().ToHex()}>Science Personnel</color>.", false, false, true);
            }
            else if (ev.TerminationCause.Equals("CONTAINEDSUCCESSFULLY BY CLASSD PERSONNEL"))
            {
                Cassie.MessageTranslated($"{nameToSay} CONTAINEDSUCCESSFULLY BY CLASSD PERSONNEL",
                    $"{nameToShow} <color=green><b>contained</b> successfully</color> by <color={ev.Attacker.Role.Type.GetColor().ToHex()}>Class-D Personnel</color>.", false, false, true);
            }
            else if (ev.TerminationCause.Equals("CONTAINEDSUCCESSFULLY BY CHAOSINSURGENCY"))
            {
                Cassie.MessageTranslated($"{nameToSay} CONTAINEDSUCCESSFULLY BY CHAOSINSURGENCY",
                    $"{nameToShow} <color=green><b>contained</b> successfully</color> by <color={ev.Attacker.Role.Type.GetColor().ToHex()}>Chaos Insurgency</color>.", false, false, true);
            }
            else if (ev.TerminationCause.Equals("CONTAINEDSUCCESSFULLY Containment unit unknown"))
            {
                Cassie.MessageTranslated($"{nameToSay} contained successfully Containment unit unknown",
                    $"{nameToShow} <color=green><b>contained</b> successfully</color>. Containment unit unknown.", false, false, true);
            }
            else if (ev.TerminationCause.Substring(0, 36).Trim().Equals("CONTAINEDSUCCESSFULLY CONTAINMENTUNIT"))
            {
                // Actually use a dictinary here
                string unit = ev.TerminationCause.Substring(37).Trim();
                string designation = "";
                if (unit.Substring(0, 5) == "NATO_A")
                {
                    designation = "ALPHA";
                }
                else if (unit.Substring(0, 5) == "NATO_B")
                {
                    designation = "BRAVO";
                }
                else if (unit.Substring(0, 5) == "NATO_C")
                {
                    designation = "CHARLIE";
                }
                else if (unit.Substring(0, 5) == "NATO_D")
                {
                    designation = "DELTA";
                }
                else if (unit.Substring(0, 5) == "NATO_E")
                {
                    designation = "ECHO";
                }
                else if (unit.Substring(0, 5) == "NATO_F")
                {
                    designation = "FOXTROT";
                }
                else if (unit.Substring(0, 5) == "NATO_G")
                {
                    designation = "GOLF";
                }
                else if (unit.Substring(0, 5) == "NATO_H")
                {
                    designation = "HOTEL";
                }
                else if (unit.Substring(0, 5) == "NATO_I")
                {
                    designation = "INDIA";
                }
                else if (unit.Substring(0, 5) == "NATO_J")
                {
                    designation = "JULIETT";
                }
                else if (unit.Substring(0, 5) == "NATO_K")
                {
                    designation = "KILO";
                }
                else if (unit.Substring(0, 5) == "NATO_L")
                {
                    designation = "LIMA";
                }
                else if (unit.Substring(0, 5) == "NATO_M")
                {
                    designation = "MIKE";
                }
                else if (unit.Substring(0, 5) == "NATO_N")
                {
                    designation = "NOVEMBER";
                }
                else if (unit.Substring(0, 5) == "NATO_O")
                {
                    designation = "OSCAR";
                }
                else if (unit.Substring(0, 5) == "NATO_P")
                {
                    designation = "PAPA";
                }
                else if (unit.Substring(0, 5) == "NATO_Q")
                {
                    designation = "QUEBEC";
                }
                else if (unit.Substring(0, 5) == "NATO_R")
                {
                    designation = "ROMEO";
                }
                else if (unit.Substring(0, 5) == "NATO_S")
                {
                    designation = "SIERRA";
                }
                else if (unit.Substring(0, 5) == "NATO_T")
                {
                    designation = "TANGO";
                }
                else if (unit.Substring(0, 5) == "NATO_U")
                {
                    designation = "UNIFORM";
                }
                else if (unit.Substring(0, 5) == "NATO_V")
                {
                    designation = "VICTOR";
                }
                else if (unit.Substring(0, 5) == "NATO_W")
                {
                    designation = "WHISKEY";
                }
                else if (unit.Substring(0, 5) == "NATO_X")
                {
                    designation = "XRAY";
                }
                else if (unit.Substring(0, 5) == "NATO_Y")
                {
                    designation = "YANKEE";
                }
                else if (unit.Substring(0, 5) == "NATO_Z")
                {
                    designation = "ZULU";
                }



                Cassie.MessageTranslated($"{nameToSay} {ev.TerminationCause}",                    
                    $"{nameToShow} <color=green><b>contained</b> successfully</color>. <color={ev.Attacker.Role.Type.GetColor().ToHex()}>Containment unit <b>{designation}-{unit.Substring(5)}</b></color>.", false, false, true);
            }
            else if (ev.TerminationCause.Equals("LOST IN DECONTAMINATION SEQUENCE"))
            {
                Cassie.MessageTranslated($"{nameToSay} LOST IN DECONTAMINATION SEQUENCE",
                    $"{nameToShow} lost in <color=#4c5444>Decontamination</color> Sequence.", false, false, true);
            }
        }
        public void OnAnnouncingMTF(AnnouncingNtfEntranceEventArgs ev)
        {
            ev.IsAllowed = false;
            Cassie.MessageTranslated($"mobile task force Epsilon 11 designated {ev.UnitName} {ev.UnitNumber} has entered the facility",
                $"<color=#003dca>Mobile Task Force <b>Epsilon-11</b></color> designated <color=#003dca><b>{ev.UnitName}-{ev.UnitNumber}</b></color> has entered the Facility.", false, false, true);
            Cassie.MessageTranslated($"All remaining personnel are advised to proceed with standard evacuation protocols until an M T F squad reaches your destination",
                $"<b>All remaining personnel</b> are advised to proceed with <color=red><b>standard evacuation protocols</b></color> until an <color=#003dca>MTF</color> squad reaches your destination.", false, false, true);
            if (ev.ScpsLeft > 1)
            {
                Cassie.MessageTranslated($"awaiting recontainment of {ev.ScpsLeft} SCP subjects",
                    $"Awaiting re-containment of <color=red>{ev.ScpsLeft} SCP subjects</color>.", false, false, true);
            }
            else
            {
                Cassie.MessageTranslated($"awaiting recontainment of 1 SCP subject",
                    $"Awaiting re-containment of <color=red>1 SCP subject</color>.", false, false, true);
            }
            if (UnityEngine.Random.value < .4)
            {
                Cassie.MessageTranslated($"substantial threat to safety remains within the facility exercise caution",
                    $"<color=red><b>Substantial threat</b></color> to <b>safety</b> remains within the facility -- <color=red><b>exercise caution</b></color>.", false, false, true);
            }

        }
    }
}
