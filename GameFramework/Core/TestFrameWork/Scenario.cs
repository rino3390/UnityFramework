using System;
using System.Collections.Generic;

namespace GameFramework.Core
{
	public class Scenario
	{
		private List<Action> actions;
		
		public Scenario And(string annotation , Action action)
		{
			action.Invoke();
			return this;
		}
		public Scenario Given(string annotation , Action action)
		{
			action.Invoke();
			return this;
		}
		
		public Scenario Then(string annotation , Action action)
		{
			action.Invoke();
			return this;
		}
		
		public Scenario When(string annotation , Action action)
		{
			action.Invoke();
			return this;
		}
	}
}