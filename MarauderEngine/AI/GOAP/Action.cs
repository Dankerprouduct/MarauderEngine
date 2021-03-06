﻿using System;
using System.Collections.Generic;

namespace MarauderEngine.AI.GOAP
{
    public class Action
    {

        /// <summary>
        /// optional name for the Action
        /// </summary>
        public string name;

        /// <summary>
        /// the cost of performing the action
        /// </summary>
        public int cost = 1;

        internal HashSet<Tuple<string, bool>> preConditions = new HashSet<Tuple<string, bool>>();
        internal HashSet<Tuple<string, bool>> postConditions = new HashSet<Tuple<string, bool>>();

        public Action() { }

        public Action(string name)
        {
            this.name = name;
        }

        public Action(string name, int cost): this(name)
        {
            this.cost = cost;
        }

        public void setPrecondition(string conditionName, bool value)
        {
            preConditions.Add(new Tuple<string, bool>(conditionName, value));
        }


        public void setPostcondition(string conditionName, bool value)
        {
            postConditions.Add(new Tuple<string, bool>(conditionName, value));
        }

        /// <summary>
		/// called before the Planner does its planning. Gives the Action an opportunity to set its score or to opt out if it isnt of use.
		/// For example, if the Action is to pick up a gun but there are no guns in the world returning false would keep the Action from being
		/// considered by the ActionPlanner.
		/// </summary>
		public virtual bool validate()
        {
            return true;
        }


        public override string ToString()
        {
            return string.Format("[Action] {0} - cost: {1}", name, cost);
        }

    }
}
