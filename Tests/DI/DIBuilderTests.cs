using Juce.Core.Di.Builder;
using Juce.Core.Di.Container;
using Juce.Core.Di.Data;
using NUnit.Framework;
using System;

namespace Juce.Core.Di
{
    public class DIBuilderTests
    {
        [Test]
        public void DIBuilderTests_FromNew()
        {
            IDiContainerBuilder containerBuilder = new DiContainerBuilder();

            containerBuilder.Bind<Class1Test>().FromNew();

            IDiContainer container = containerBuilder.Build();

            Class1Test class1Test = container.Resolve<Class1Test>();

            Assert.IsNotNull(class1Test);
        }

        [Test]
        public void DIBuilderTests_FromInstance()
        {
            Class1Test classTest = new Class1Test();

            IDiContainerBuilder containerBuilder = new DiContainerBuilder();

            containerBuilder.Bind<Class1Test>().FromInstance(classTest);

            IDiContainer container = containerBuilder.Build();

            Class1Test class1Test = container.Resolve<Class1Test>();

            Assert.IsNotNull(class1Test);
        }

        [Test]
        public void DIBuilderTests_FromFunction()
        {
            IDiContainerBuilder containerBuilder = new DiContainerBuilder();

            containerBuilder.Bind<Class1Test>().FromFunction((c) => new Class1Test());

            IDiContainer container = containerBuilder.Build();

            Class1Test class1Test = container.Resolve<Class1Test>();

            Assert.IsNotNull(class1Test);
        }

        [Test]
        public void DIBuilderTests_FromFunctionLateResolve()
        {
            IDiContainerBuilder containerBuilder = new DiContainerBuilder();

            containerBuilder.Bind<Class1Test>().FromNew();

            containerBuilder.Bind<Class2Test>()
                .FromFunction((c) => new Class2Test(
                    c.Resolve<Class1Test>()
                    ));

            IDiContainer container = containerBuilder.Build();

            Class2Test class2Test = container.Resolve<Class2Test>();

            Assert.IsNotNull(class2Test);
        }

        [Test]
        public void DIBuilderTests_AssertCircularResolve()
        {
            IDiContainerBuilder containerBuilder = new DiContainerBuilder();

            containerBuilder.Bind<Class5Test>()
                .FromFunction((x) => new Class5Test(
                    x.Resolve<Class6Test>()
                    ));

            containerBuilder.Bind<Class6Test>()
                .FromFunction((x) => new Class6Test(
                    x.Resolve<Class5Test>()
                    ));

            IDiContainer container = containerBuilder.Build();

            Assert.Throws<Exception>(() => container.Resolve<Class5Test>());
        }

        [Test]
        public void DIBuilderTests_SimpleInit()
        {
            IDiContainerBuilder containerBuilder = new DiContainerBuilder();

            containerBuilder.Bind<Class3Test>()
                .FromNew()
                .WhenInit((o) => o.Init);

            IDiContainer container = containerBuilder.Build();

            Class3Test class3Test = container.Resolve<Class3Test>();

            Assert.IsNotNull(class3Test);
        }

        [Test]
        public void DIBuilderTests_LateResolveInit()
        {
            IDiContainerBuilder containerBuilder = new DiContainerBuilder();

            containerBuilder.Bind<Class1Test>().FromNew();

            containerBuilder.Bind<Class3Test>()
                .FromNew()
                .WhenInit((c, o) => o.Init(
                    c.Resolve<Class1Test>()
                    ));

            IDiContainer container = containerBuilder.Build();

            Class3Test class3Test = container.Resolve<Class3Test>();

            Assert.IsNotNull(class3Test);
            Assert.IsNotNull(class3Test.Class1Test);
        }

        [Test]
        public void DIBuilderTests_SimpleDispose()
        {
            IDiContainerBuilder containerBuilder = new DiContainerBuilder();

            containerBuilder.Bind<Class1Test>().FromNew();

            containerBuilder.Bind<Class3Test>()
                .FromNew()
                .WhenDispose((o) => o.Dispose());

            IDiContainer container = containerBuilder.Build();

            Class3Test class3Test = container.Resolve<Class3Test>();

            container.Dispose();

            Assert.IsNotNull(class3Test);
            Assert.IsTrue(class3Test.Disposed);
        }

        [Test]
        public void DIBuilderTests_AssertResolve()
        {
            IDiContainer container = new DiContainerBuilder().Build();

            Assert.Throws<Exception>(() => container.Resolve<Class1Test>());
        }

        [Test]
        public void DIBuilderTests_InterfaceBind()
        {
            IDiContainerBuilder containerBuilder = new DiContainerBuilder();

            containerBuilder.Bind<IClass1Test, Class1Test>().FromNew();

            IDiContainer container = containerBuilder.Build();

            IClass1Test class1Test = container.Resolve<IClass1Test>();

            Assert.NotNull(class1Test);
        }

        [Test]
        public void DIBuilderTests_InterfaceBindNonAssignableAssert()
        {
            IDiContainerBuilder containerBuilder = new DiContainerBuilder();

            Assert.Throws<Exception>(() => containerBuilder.Bind<IClass1Test, Class2Test>().FromNew());
        }

        [Test]
        public void DIBuilderTests_AssertDuplicateBind()
        {
            IDiContainerBuilder containerBuilder = new DiContainerBuilder();

            containerBuilder.Bind<Class1Test>().FromNew();

            Assert.Throws<Exception>(() => containerBuilder.Bind<Class1Test>().FromNew());
        }

        [Test]
        public void DIBuilderTests_MultipleContainersBind()
        {
            IDiContainerBuilder containerBuilder1 = new DiContainerBuilder();
            containerBuilder1.Bind<Class1Test>().FromNew();
            IDiContainer container1 = containerBuilder1.Build();

            IDiContainerBuilder containerBuilder2 = new DiContainerBuilder();
            containerBuilder2.Bind<Class2Test>()
                .FromFunction((c) => new Class2Test(
                c.Resolve<Class1Test>()
                ));
            containerBuilder2.Bind(container1);
            IDiContainer container2 = containerBuilder2.Build();

            container2.Resolve<Class2Test>();
        }

        [Test]
        public void DIBuilderTests_AssertMultipleContainersWithDuplicatesBind()
        {
            IDiContainerBuilder containerBuilder1 = new DiContainerBuilder();
            containerBuilder1.Bind<Class1Test>().FromNew();
            IDiContainer container1 = containerBuilder1.Build();

            IDiContainerBuilder containerBuilder2 = new DiContainerBuilder();
            containerBuilder2.Bind<Class1Test>().FromNew();
            Assert.Throws<Exception>(() => containerBuilder2.Bind(container1));
        }
    }
}
