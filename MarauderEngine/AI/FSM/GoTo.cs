using System;

namespace MarauderEngine.AI.FSM
{
    public class GoTo : IState
    {
        public IState EnterState(MarauderEngine.Entity.Entity entity)
        {
            //var action = entity.actionPlan.Peek().name;

            //switch (action)
            //{
            //    case "sleep":
            //    {
            //        // navigate to
            //        //entity.FindPathTo(new Vector2());
            //        break;
            //    }
            //}

            throw new NotImplementedException();

        }

        public IState ExitState(MarauderEngine.Entity.Entity entity)
        {
            throw new NotImplementedException();
        }

        public IState Update(MarauderEngine.Entity.Entity entity)
        {
            
            return null;
        }
    }
}
