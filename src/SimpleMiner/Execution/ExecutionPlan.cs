using SimpleMiner.Miner;
using SimpleMiner.Models;
using SimpleMiner.Persistence.Context;
using SimpleMiner.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleMiner.Execution
{
    public class ExecutionPlan<TInput, TOutput> where TInput : Input where TOutput : Output
    {
        protected DataBaseContext DataBaseContext { get; set; }
        protected ExecutionPlanConfig PlanConfig { get; set; }

        public ExecutionPlan(DataBaseContext dataBaseContext, Action<ExecutionPlanBuilder> planBuilder)
        {
            ExecutionPlanBuilder executionPlanBuilder = new ExecutionPlanBuilder();
            planBuilder(executionPlanBuilder);
            PlanConfig = executionPlanBuilder.PlanConfig;

            DataBaseContext = dataBaseContext;
            Repository = new Repository(dataBaseContext);
        }

        public ExecutionPlan(DataBaseContext dataBaseContext)
        {
            PlanConfig = new ExecutionPlanConfig();

            DataBaseContext = dataBaseContext;
            Repository = new Repository(dataBaseContext);
        }

        protected Repository Repository { get; set; }

        public virtual void Execute()
        {
            BaseSource<TInput, TOutput> source = default;

            if (PlanConfig.SourceInstanceMode == SourceInstanceMode.Execution)
                source = CreateSource();

            while (true)
            {
                if (PlanConfig.SourceInstanceMode == SourceInstanceMode.Lote)
                    source = CreateSource();

                var inputLote = GetInputLote(PlanConfig.LoteSize);
                if (inputLote == null || !inputLote.Any())
                {
                    Thread.Sleep(PlanConfig.WaitForBatch);
                }

                var stopWathLote = new Stopwatch();
                stopWathLote.Start();
                TOutput output = Activator.CreateInstance<TOutput>();
                Parallel.ForEach(inputLote, new ParallelOptions
                {
                    MaxDegreeOfParallelism = PlanConfig.MaxParallel
                }, input =>
                {
                    BaseSource<TInput, TOutput> threadSource;
                    if (PlanConfig.SourceInstanceMode == SourceInstanceMode.Thread)
                        threadSource = CreateSource();
                    else
                        threadSource = source;

                    var stopWatchInput = new Stopwatch();
                    stopWatchInput.Start();
                    try
                    {
                        output = threadSource.GetData(input);

                        stopWatchInput.Stop();

                        DateTime ultimaConsulta = DateTime.Now;
                        input.Status = output.Status;
                        input.UltimaConsulta = ultimaConsulta;
                        input.TempoConsulta = stopWatchInput.Elapsed;

                        output.DataConsulta = ultimaConsulta;
                        output.InputId = input.Id;
                        output.LoteId = input.LoteId;

                        Persist(input, output);
                    }
                    catch (Exception e)
                    {
                        output.Status = "Unhandled";
                        input.Mensagem = e.Message;
                    }
                    finally
                    {
                        // Definir local para persistir erros
                    }
                });

                stopWathLote.Stop();
                Repository.SaveChanges(PlanConfig.BatchSize);
            }
        }

        protected virtual IEnumerable<TInput> GetInputLote(int loteSize)
        {
            var repository = new Repository(DataBaseContext);
            try
            {
                var tableAttribute = typeof(TInput).GetCustomAttributes(typeof(TableAttribute), false).Single();

                return repository.Query<TInput>(
                    $"UPDATE TOP({loteSize}) {(tableAttribute as TableAttribute).Name} SET [Status] = " +
                    "{0} OUTPUT inserted.* WHERE[Status] IS NULL", "Consultando").ToList();
            }
            catch (Exception e)
            {
                return null;
            }
        }

        protected virtual BaseSource<TInput, TOutput> CreateSource()
        {
            var instance = AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes())
                             .Where(x => typeof(BaseSource<TInput, TOutput>).IsAssignableFrom(x))
                             .Select(x => x).FirstOrDefault();

            return (BaseSource<TInput, TOutput>)Activator.CreateInstance(instance);
        }

        protected virtual void Persist(TInput input, TOutput output)
        {
            lock (LockObj)
            {
                Repository.Update(input);

                if (output.Status == "OK")
                    Repository.Add(output);

                Repository.SaveChanges(PlanConfig.BatchSize);
            }
        }

        protected static object LockObj = new object();
    }
}
