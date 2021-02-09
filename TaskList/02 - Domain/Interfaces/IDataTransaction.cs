namespace TaskList._01___Domain.Interfaces
{
    public interface IDataTransaction
    {
        /// <summary>
        /// Efetivar transações de banco
        /// </summary>
        void Commit();


        /// <summary>
        /// Desfazer transações de banco
        /// </summary>
        void RollBack();

    }
}

