using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Juce.Core.Contexts
{
    public sealed class ContextsService : IContextsService
    {
        private readonly Dictionary<Type, IContext> contextsTypes = new Dictionary<Type, IContext>();

        private readonly List<IContext> states = new List<IContext>();
        private bool currentStateStarted;

        public void Add(IContext context)
        {
            Type type = context.GetType();

            contextsTypes.Add(type, context);
        }

        public Task UnloadCurrent(CancellationToken cancellationToken)
        {
            bool currentFound = TryGetCurrentContext(out IContext context);

            if(!currentFound)
            {
                return Task.CompletedTask;
            }

            states.Remove(context);

            return context.DisposeAsync();
        }

        public Task Load<TContext>(IContextData data, CancellationToken cancellationToken) where TContext : IContext
        {
            Type type = typeof(TContext);

            bool contextFound = contextsTypes.TryGetValue(type, out IContext context);

            if(!contextFound)
            {
                throw new Exception();
            }

            currentStateStarted = false;

            states.Add(context);

            return context.Load(data, cancellationToken);
        }

        public Task Load<TContext>(CancellationToken cancellationToken) where TContext : IContext
        {
            return Load<TContext>(NopContextData.Instance, cancellationToken);
        }

        public void StartCurrent()
        {
            bool currentFound = TryGetCurrentContext(out IContext context);

            if (!currentFound)
            {
                return;
            }

            if (currentStateStarted)
            {
                return;
            }

            context.Start();

            currentStateStarted = true;
        }

        public async Task UnloadCurrentAndLoad<TContext>(IContextData data, CancellationToken cancellationToken) where TContext : IContext
        {
            await UnloadCurrent(cancellationToken);

            await Load<TContext>(data, cancellationToken);
        }

        public Task UnloadCurrentAndLoad<TContext>(CancellationToken cancellationToken) where TContext : IContext
        {
            return UnloadCurrentAndLoad<TContext>(NopContextData.Instance, cancellationToken);
        }

        public async Task LoadAndStartCurrent<TContext>(IContextData data, CancellationToken cancellationToken) where TContext : IContext
        {
            await Load<TContext>(data, cancellationToken);

            StartCurrent();
        }

        public Task LoadAndStartCurrent<TContext>(CancellationToken cancellationToken) where TContext : IContext
        {
            return LoadAndStartCurrent<TContext>(NopContextData.Instance, cancellationToken);
        }


        private bool TryGetCurrentContext(out IContext context)
        {
            if(states.Count == 0)
            {
                context = default;
                return false;
            }

            context = states[states.Count - 1];
            return true;
        }
    }
}
