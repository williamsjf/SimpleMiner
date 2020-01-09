using System;

namespace SimpleMiner.Execution
{
    public class ExecutionPlanConfig
    {
        /// <summary>
        /// Define o tamanho do lote que será processado simultaneamente.
        /// </summary>
        public int LoteSize { get; set; }

        /// <summary>
        /// Define a quantidade máxima de threads que serão executadas simultaneamente.
        /// </summary>
        public int MaxParallel { get; set; }

        /// <summary>
        /// Indica a quantidade itens que serão comitados simultaneamente.
        /// </summary>
        public int BatchSize { get; set; }

        /// <summary>
        /// Indica a forma que o datasource será instanciado
        /// </summary>
        public SourceInstanceMode SourceInstanceMode { get; set; }

        /// <summary>
        /// Tempo a se esperar para buscar um novo lote quando não houver um lote disponível.
        /// </summary>
        public TimeSpan WaitForBatch { get; set; }
    }
}
