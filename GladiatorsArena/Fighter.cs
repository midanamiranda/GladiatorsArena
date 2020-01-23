using System;
using System.Collections.Generic;
using System.Text;

namespace GladiatorsArena
{
    public class Fighter : IDisposable
    {
        // Events

        public event fighterHealthHandler HealthUpdate;
        
        // Constructors

        /// <summary>
        /// Base Fighter Constructor with fightPosition managed to start at 1
        /// </summary>
        public Fighter()
        {
            fightPosition++;
        }

        /// <summary>
        /// By giving the Name, the fighter will be created with the right pre-set health and the respective position
        /// </summary>
        /// <param name="name"></param>
        public Fighter(string name)
            : this()
        {
            Code = fightPosition;
            Name = name;
        }

        // Fields

        private static readonly Stack<int> damageTrack = new Stack<int>();

        private static int fightPosition = 0;

        // Properties

        public bool IsDead { get; set; } = false;

        private int code;
        public int Code
        {
            get { return code; }
            set { code = value; }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private int health = Battle.MaxFighterHealth;

        public int Health
        {
            get { return health; }
            set
            {
                damageTrack.Push(value);
                health -= damageTrack.Peek();
                HealthUpdate?.Invoke();
                if (health <= 0)
                    IsDead = true;                   
            }
        }

        // Members

        // Event Listeners

        /// <summary>
        /// Displays the fighter's health update
        /// </summary>
        /// <param name="fighter"></param>
        public void HealthUpdateListener()
        {
            int health = this.Health < 0 ? 0 : this.Health;
            Console.WriteLine($"{this.Name} was damaged and lost {damageTrack.Pop()} health but still has a health of {health}.");
        }

        // IDisposable Members

        /// <summary>
        /// By the end of the method, if using the 'using' keyword, 
        /// the object will be disposed
        /// and the position manager will be reseted
        /// </summary>
        public void Dispose()
        {
            damageTrack.Clear();
            fightPosition = 0;
        }
    }
}
