using CitizenFX.Core;
using CitizenFX.Core.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Breathalyzer
{
    public class Breathalyzer : BaseScript
    {
        private bool beingBreathalyzed = false;
        private int currentnetidofficer;
        public Breathalyzer()
        {
            EventHandlers["BTZ:ReceiveTest"] += new Action<int, string, string, string>(HandleBreathalyzerReceived);
            EventHandlers["BTZ:ProvideSample"] += new Action<string, string, string>(provideSample);
            EventHandlers["BTZ:FailProvide"] += new Action<string>(failProvide);
        }

        private async void HandleBreathalyzerReceived(int netidofficer, string officername, string limitnum, string limitunit)
        {
            if (beingBreathalyzed) { return; }
            beingBreathalyzed = true;
            int count = -1;
            currentnetidofficer = netidofficer;
            while (beingBreathalyzed)
            {
                count++;
                if (count % 20 == 0)
                {
                    TriggerEvent("chatMessage", "BREATHALYZER", new int[] { 255, 255, 0 }, "You're being breathalyzed by " + officername + ". Type /breath X (X is a number) or /failprovide. Limit: " + limitnum + " " + limitunit);
                }
                Ped officer = Players[netidofficer].Character;
                if (count > 120 || Vector3.Distance(officer.Position, Game.PlayerPed.Position) > 20)
                {
                    TriggerEvent("chatMessage", "BREATHALYZER", new int[] { 255, 255, 0 }, "You failed to provide a valid breath sample.");
                    TriggerServerEvent("BTZ:SendMessage", currentnetidofficer, "The suspect failed to provide a valid breath sample.");
                    beingBreathalyzed = false;
                }
                await Delay(500);
            }
        }

        private void failProvide(string SuspectName)
        {
            if (beingBreathalyzed)
            {
                TriggerEvent("chatMessage", "BREATHALYZER", new int[] { 255, 255, 0 }, "You failed to provide a valid breath sample.");
                TriggerServerEvent("BTZ:SendMessage", currentnetidofficer, SuspectName + " failed to provide a valid breath sample.");
                beingBreathalyzed = false;
            }
        }

        private void provideSample(string SuspectName, string reading, string limitunit)
        {
            if (beingBreathalyzed)
            {
                TriggerEvent("chatMessage", "BREATHALYZER", new int[] { 255, 255, 0 }, "You provided a breath sample of " + reading + " " + limitunit + ".");
                TriggerServerEvent("BTZ:SendMessage", currentnetidofficer, SuspectName + " provided a breath sample of " + reading + " " + limitunit + ".");
                beingBreathalyzed = false;
            }
        }
    }
}
