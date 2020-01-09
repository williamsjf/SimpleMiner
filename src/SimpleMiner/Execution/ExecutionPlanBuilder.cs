using System;

namespace SimpleMiner.Execution
{
    public class ExecutionPlanBuilder
    {
        public ExecutionPlanConfig PlanConfig { get; private set; }

        public ExecutionPlanBuilder()
        {
            PlanConfig = new ExecutionPlanConfig
            {
                BatchSize = 10,
                LoteSize = 10,
                MaxParallel = 10,
                SourceInstanceMode = SourceInstanceMode.Lote,
                WaitForBatch = TimeSpan.FromHours(1)
            };
        }

        public ExecutionPlanBuilder SetLotSize(int size)
        {
            PlanConfig.LoteSize = size;
            return this;
        }

        public ExecutionPlanBuilder SetMaxParallel(int maxParallel)
        {
            PlanConfig.MaxParallel = maxParallel;
            return this;
        }

        public ExecutionPlanBuilder SetBatchSize(int size)
        {
            PlanConfig.BatchSize = size;
            return this;
        }

        public ExecutionPlanBuilder SetSourceInstanceMode(SourceInstanceMode instanceMode)
        {
            PlanConfig.SourceInstanceMode = instanceMode;
            return this;
        }

        public ExecutionPlanBuilder SetWaitForBatch(TimeSpan timeSpan)
        {
            PlanConfig.WaitForBatch = timeSpan;
            return this;
        }
    }
}
