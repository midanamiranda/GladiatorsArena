using System;
using System.Collections.Generic;
using System.Text;

namespace GladiatorsArena
{
	public class Battle : IDisposable
    {
		// Contructors

		public Battle(List<Fighter> fightersList)
		{
			FightersList = fightersList;
		}

		// Properties

		public static bool IsStillOn { get; set; } = true;

		private List<Fighter> fightersList;

		public List<Fighter> FightersList
		{
			get { return fightersList; }
			set { fightersList = value; }
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
}
