namespace MarauderEngine.AI.FSM
{
    /// <summary>
    /// A Finite State Machine
    /// Must provide an initial _state
    /// </summary>
    public class FSM
    {
        private IState _state; 

        /// <summary>
        /// Please provide the Entity that the state is being applied to
        /// </summary>
        /// <param name="entity">The Entity</param>
        /// <param name="initialState">The Initial State (Usually Idle)</param>
        public FSM(MarauderEngine.Entity.Entity entity, IState initialState)
        {
            _state = initialState.EnterState(entity);
        }

        /// <summary>
        /// Update with the Entity
        /// </summary>
        /// <param name="entity"></param>
        public void Update(MarauderEngine.Entity.Entity entity)
        {

            if (_state.Update(entity) != null)
            {
                _state = _state.Update(entity);
                _state.EnterState(entity); 
            }
        }
    }
}
