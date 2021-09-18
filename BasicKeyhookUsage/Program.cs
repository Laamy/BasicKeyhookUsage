#region

using BasicKeyhookUsage.ClientBase.KeyBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

#endregion

namespace BasicKeyhookUsage
{
    class Program
    {
        static void Main(string[] args)
        {
            MCM.openGame(); // Open game up inside of MCM class
            MCM.openWindowHost();

            new Keymap(); // only define new keymap ONCE. this just starts up the keymap threads so it can catch minecraft key inputs

            Keymap.keyEvent += McKeyPress; // Assign Mc Keymap to function so that functions called when a keys been pressed/held/let go of

            while (true)// infinite loop
            {
            }
        }

        private static void McKeyPress(object sender, KeyEvent e)
        {
            // Game > ClientInstance > LocalPlayer
            ulong clientInstance = MCM.baseEvaluatePointer(0x041457D8, new ulong[] { 0x0, 0x20 });
            ulong localPlayer = MCM.evaluatePointer(clientInstance, new ulong[] { 0xC8, 0x0 });
            // all this means is Minecraft.Windows.exe+041457D8,0,20,C8,0(LocalPlayerPointer 1.17)
            // we put 0x before a hex number to tell the programing language that its hex
            // to fix this client all you have to do is replace the broken pointer information

            // Game > ClientInstance > LocalPlayer > fieldOfView
            ulong fieldOfViewAddr = localPlayer + 0x10F0;

            if (e.vkey == VKeyCodes.KeyDown) // Keydown
            {
                if (e.key == Keys.C) // If CKey has just been pressed this code here will trigger
                {
                    MCM.writeFloat(fieldOfViewAddr, 0.1f); // tell game to zoom in
                }
            }
            if (e.vkey == VKeyCodes.KeyUp) // Keyup
            {
                if (e.key == Keys.C) // If CKey has just been let go of this code here will trigger
                {
                    MCM.writeFloat(fieldOfViewAddr, 1f); // tell game to zoom back out
                }
            }
        }
    }
}
