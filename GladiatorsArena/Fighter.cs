using System;
using System.Collections.Generic;
using System.Text;

namespace GladiatorsArena
{
    public class Fighter : IDisposable
    {
        // declare the events to be handled
        public event fighterHealthHandler HealthUpdate;
        public event fighterHealthHandler DeadFighter;
        
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
            Code = fightPosition.ToString();
            Name = name;
        }

        // Fields

        private static readonly Stack<int> damageLog = new Stack<int>();

        private static int fightPosition = 0;

        // Properties

        private string code;
        public string Code
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

        private int health = 15;

        public int Health
        {
            get { return health; }
            set
            {
                damageLog.Push(value);
                health -= damageLog.Peek();
                HealthUpdate?.Invoke();
                if (health <= 0)
                    DeadFighter?.Invoke();
            }
        }

        // Members

        public void AssigneHealthListeners()
        {
            this.HealthUpdate += HealthUpdateListener;
            this.DeadFighter += DeadFighterListener;
        }

        // Event Listeners

        /// <summary>
        /// Displays the fighter's health update
        /// </summary>
        /// <param name="fighter"></param>
        public void HealthUpdateListener()
        {
            int health = this.Health < 0 ? 0 : this.Health;
            Console.WriteLine($"{this.Name} was damaged and lost {damageLog.Pop()} health. Now has a health of {health}");
        }

        /// <summary>
		/// Display that the given fighter has lost his life and switch Battle.IsStillOn to false
		/// </summary>
		/// <param name="fighter"></param>
		public void DeadFighterListener()
        {
            Console.WriteLine($"{this.Name} has lost his life! The game is over.");
            Battle.IsStillOn = false;
        }

        // IDisposable Members

        /// <summary>
        /// By the end of the method, if using the 'using' keyword, 
        /// the object will be disposed
        /// and the position manager will be reseted
        /// </summary>
        public void Dispose()
        {
            damageLog.Clear();
            fightPosition = 0;
        }
    }
}
