using Juce.Core.Di.Container;

namespace Juce.Core.Di.Builder
{
    public static class DiContainerBuilderUtils
    {
        public static IDiContainer Build<TInterface>(TInterface value)
        {
            IDiContainerBuilder builder = new DiContainerBuilder();

            builder.Bind<TInterface>().FromInstance(value);

            return builder.Build();
        }

        public static IDiContainer Build<TInterface1, TInterface2>(TInterface1 value1, TInterface2 value2)
        {
            IDiContainerBuilder builder = new DiContainerBuilder();

            builder.Bind<TInterface1>().FromInstance(value1);
            builder.Bind<TInterface2>().FromInstance(value2);

            return builder.Build();
        }

        public static IDiContainer Build<TInterface1, TInterface2, TInterface3>(TInterface1 value1, TInterface2 value2, TInterface3 value3)
        {
            IDiContainerBuilder builder = new DiContainerBuilder();

            builder.Bind<TInterface1>().FromInstance(value1);
            builder.Bind<TInterface2>().FromInstance(value2);
            builder.Bind<TInterface3>().FromInstance(value3);

            return builder.Build();
        }

        public static IDiContainer Build<
            TInterface1, 
            TInterface2, 
            TInterface3, 
            TInterface4
            >(
            TInterface1 value1, 
            TInterface2 value2, 
            TInterface3 value3,
            TInterface4 value4
            )
        {
            IDiContainerBuilder builder = new DiContainerBuilder();

            builder.Bind<TInterface1>().FromInstance(value1);
            builder.Bind<TInterface2>().FromInstance(value2);
            builder.Bind<TInterface3>().FromInstance(value3);
            builder.Bind<TInterface4>().FromInstance(value4);

            return builder.Build();
        }

        public static IDiContainer Build<
            TInterface1,
            TInterface2,
            TInterface3,
            TInterface4,
            TInterface5
            >(
            TInterface1 value1,
            TInterface2 value2,
            TInterface3 value3,
            TInterface4 value4,
            TInterface5 value5
            )
        {
            IDiContainerBuilder builder = new DiContainerBuilder();

            builder.Bind<TInterface1>().FromInstance(value1);
            builder.Bind<TInterface2>().FromInstance(value2);
            builder.Bind<TInterface3>().FromInstance(value3);
            builder.Bind<TInterface4>().FromInstance(value4);
            builder.Bind<TInterface5>().FromInstance(value5);

            return builder.Build();
        }
    }
}
