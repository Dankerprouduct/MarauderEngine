namespace MarauderEngine.AI.FSM
{
    /// <summary>
    /// The Interface that all States should be inherited from
    /// </summary>
    public interface IState
    {
        /// <summary>
        /// the Enter State 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        IState EnterState(MarauderEngine.Entity.Entity entity);

        /// <summary>
        /// The Exit State
        /// this gets called before the state enters the next state
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        IState ExitState(MarauderEngine.Entity.Entity entity);

        /// <summary>
        /// The Update State
        /// This is what gets called during update and usually provides the conditions for the next state transition
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        IState Update(MarauderEngine.Entity.Entity entity);
        


    }
}
