using System;
using System.Collections.Generic;
using System.Text;

namespace GladiatorsArena
{
	public class Battle : IDisposable
    {
		// declare the events to be handled
		public event EventHandler<FightersListEventArgs> FighterListChanged;

		// Contructors

		public Battle(List<Fighter> fightersList)
		{
			FightersList = fightersList;
		}

		// Fields

		private static readonly Stack<Fighter> deadFighterTrack = new Stack<Fighter>();

		// Properties

		public static int MaxFighterHealth { get; set; } = 8;
		public static int MaxHealthLoss { get; set; } = 5;
		public static bool IsStillOn { get; set; } = true;

		private List<Fighter> fightersList;

		public List<Fighter> FightersList
		{
			get { return fightersList; }
			set
			{
				fightersList = value;
				if(deadFighterTrack.Count != 0)
				{
					FighterListChanged(this, new FightersListEventArgs() { listChanged = deadFighterTrack.Peek().ToString() });
					deadFighterTrack.Pop();
				}					
				if (fightersList.Count == 1) IsStillOn = false;
			}
		}

		// Members

		/// <summary>
		/// Returns the fighter with the most health in the FighterList
		/// </summary>
		/// <returns></returns>
		public Fighter GetWinner()
		{
			List<Fighter> sortedFightersList = this.fightersList;
			sortedFightersList.Sort((x, y) => x.Health.CompareTo(y.Health));
			return sortedFightersList[sortedFightersList.Count - 1];
		}

		/// <summary>
		/// Finds the fighter with IsDead tag true and removes it from the battle list
		/// </summary>
		internal void CleanDeadFighter()
		{
			var deadFighter = this.FightersList.Find(fighter => fighter.IsDead == true);
			deadFighterTrack.Push(deadFighter);
			this.FightersList.Remove(deadFighterTrack.Peek());
		}

		//public void GenerateClash()
		//{
		//	HashSet<int> attackedFighters = new HashSet<int>(this.FightersList.Count / 2);
		//}

		// IDisposable Members

		/// <summary>
		/// By the end of the method, if using the 'using' keyword,
		/// the battle list will be cleared
		/// and static field will be turned on
		/// </summary>
		public void Dispose()
		{
			this.FightersList.Clear();
			IsStillOn = true;
		}		
	}

	public class FightersListEventArgs : EventArgs
	{
		public string listChanged;
	}
}
