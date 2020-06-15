using System;
using System.Collections.Generic;
using System.Text;

namespace GladiatorsArena
{
    // define the delegate for the event handler
    public delegate void fighterHealthHandler();

    public class FightersListEventArgs : EventArgs
    {
        public string removedFighter;

        public FightersListEventArgs(string removedFighterName)
        {
            removedFighter = removedFighterName;
        }
    }

}
