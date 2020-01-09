namespace SimpleMiner.Execution
{
    /// <summary>
    /// Indica a forma que o datasource será instanciado
    /// </summary>
    public enum SourceInstanceMode
    {
        /// <summary>
        /// Indica que toda a execução acontecerá com a mesma instância do datasource.
        /// </summary>
        Execution = 0,

        /// <summary>
        /// Indica que uma nova instância será criada a cada processamento de lote.
        /// </summary>
        Lote = 1,

        /// <summary>
        /// Indica que uma nova instância será criada a cada thread.
        /// </summary>
        Thread = 2,
    }
}
