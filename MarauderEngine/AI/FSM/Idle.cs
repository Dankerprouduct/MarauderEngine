namespace MarauderEngine.AI.FSM
{
    //TODO: Update with new IDLE state from Crops 

    /// <summary>
    /// The defaule idle state for the FSM
    /// </summary>
    public class Idle : IState
    {
         /// <summary>
         /// The Enter State 
         /// </summary>
         /// <param name="entity"></param>
         /// <returns></returns>
        public IState EnterState(MarauderEngine.Entity.Entity entity)
        {
            //// find plan
            //entity.actionPlan = entity.planner.plan(entity.GetWorldState(), entity.GetGoalState());

            //if (entity.actionPlan != null && entity.actionPlan.Count > 0)
            //{
            //    return ExitState(Entity.Entity);
            //}
            //else
            //{
            //    return null;
            //}

            return null; 
            
        }


         /// <summary>
         /// The Exit State
         /// </summary>
         /// <param name="entity"></param>
         /// <returns></returns>
        public IState ExitState(MarauderEngine.Entity.Entity entity)
        {
            // found something to do move to 
            return new GoTo();
        }

         /// <summary>
         /// The Goto State
         /// </summary>
         /// <param name="entity"></param>
         /// <returns></returns>
        public IState Update(MarauderEngine.Entity.Entity entity)
        {
            return null; 
        }
    }
}
