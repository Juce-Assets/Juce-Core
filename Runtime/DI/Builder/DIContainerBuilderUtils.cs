using Juce.Core.DI.Container;

namespace Juce.Core.DI.Builder
{
    public static class DIContainerBuilderUtils
    {
        public static IDIContainer Build<TInterface>(TInterface value)
        {
            IDIContainerBuilder builder = new DIContainerBuilder();

            builder.Bind<TInterface>().FromInstance(value);

            return builder.Build();
        }

        public static IDIContainer Build<TInterface1, TInterface2>(TInterface1 value1, TInterface2 value2)
        {
            IDIContainerBuilder builder = new DIContainerBuilder();

            builder.Bind<TInterface1>().FromInstance(value1);
            builder.Bind<TInterface2>().FromInstance(value2);

            return builder.Build();
        }

        public static IDIContainer Build<TInterface1, TInterface2, TInterface3>(TInterface1 value1, TInterface2 value2, TInterface3 value3)
        {
            IDIContainerBuilder builder = new DIContainerBuilder();

            builder.Bind<TInterface1>().FromInstance(value1);
            builder.Bind<TInterface2>().FromInstance(value2);
            builder.Bind<TInterface3>().FromInstance(value3);

            return builder.Build();
        }

        public static IDIContainer Build<
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
            IDIContainerBuilder builder = new DIContainerBuilder();

            builder.Bind<TInterface1>().FromInstance(value1);
            builder.Bind<TInterface2>().FromInstance(value2);
            builder.Bind<TInterface3>().FromInstance(value3);
            builder.Bind<TInterface4>().FromInstance(value4);

            return builder.Build();
        }

        public static IDIContainer Build<
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
            IDIContainerBuilder builder = new DIContainerBuilder();

            builder.Bind<TInterface1>().FromInstance(value1);
            builder.Bind<TInterface2>().FromInstance(value2);
            builder.Bind<TInterface3>().FromInstance(value3);
            builder.Bind<TInterface4>().FromInstance(value4);
            builder.Bind<TInterface5>().FromInstance(value5);

            return builder.Build();
        }
    }
}
