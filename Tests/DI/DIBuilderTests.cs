using Juce.Core.DI.Builder;
using Juce.Core.DI.Container;
using Juce.Core.DI.Data;
using NUnit.Framework;
using System;

namespace Juce.Core.DI
{
    public class DIBuilderTests
    {
        [Test]
        public void DIBuilderTests_FromNew()
        {
            IDIContainerBuilder containerBuilder = new DIContainerBuilder();

            containerBuilder.Bind<Class1Test>().FromNew();

            IDIContainer container = containerBuilder.Build();

            Class1Test class1Test = container.Resolve<Class1Test>();

            Assert.IsNotNull(class1Test);
        }

        [Test]
        public void DIBuilderTests_FromInstance()
        {
            Class1Test classTest = new Class1Test();

            IDIContainerBuilder containerBuilder = new DIContainerBuilder();

            containerBuilder.Bind<Class1Test>().FromInstance(classTest);

            IDIContainer container = containerBuilder.Build();

            Class1Test class1Test = container.Resolve<Class1Test>();

            Assert.IsNotNull(class1Test);
        }

        [Test]
        public void DIBuilderTests_FromFunction()
        {
            IDIContainerBuilder containerBuilder = new DIContainerBuilder();

            containerBuilder.Bind<Class1Test>().FromFunction((c) => new Class1Test());

            IDIContainer container = containerBuilder.Build();

            Class1Test class1Test = container.Resolve<Class1Test>();

            Assert.IsNotNull(class1Test);
        }

        [Test]
        public void DIBuilderTests_FromFunctionLateResolve()
        {
            IDIContainerBuilder containerBuilder = new DIContainerBuilder();

            containerBuilder.Bind<Class1Test>().FromNew();

            containerBuilder.Bind<Class2Test>()
                .FromFunction((c) => new Class2Test(
                    c.Resolve<Class1Test>()
                    ));

            IDIContainer container = containerBuilder.Build();

            Class2Test class2Test = container.Resolve<Class2Test>();

            Assert.IsNotNull(class2Test);
        }

        [Test]
        public void DIBuilderTests_AssertCircularResolve()
        {
            IDIContainerBuilder containerBuilder = new DIContainerBuilder();

            containerBuilder.Bind<Class5Test>()
                .FromFunction((x) => new Class5Test(
                    x.Resolve<Class6Test>()
                    ));

            containerBuilder.Bind<Class6Test>()
                .FromFunction((x) => new Class6Test(
                    x.Resolve<Class5Test>()
                    ));

            IDIContainer container = containerBuilder.Build();

            Assert.Throws<Exception>(() => container.Resolve<Class5Test>());
        }

        [Test]
        public void DIBuilderTests_SimpleInit()
        {
            IDIContainerBuilder containerBuilder = new DIContainerBuilder();

            containerBuilder.Bind<Class3Test>()
                .FromNew()
                .WhenInit((o) => o.Init);

            IDIContainer container = containerBuilder.Build();

            Class3Test class3Test = container.Resolve<Class3Test>();

            Assert.IsNotNull(class3Test);
        }

        [Test]
        public void DIBuilderTests_LateResolveInit()
        {
            IDIContainerBuilder containerBuilder = new DIContainerBuilder();

            containerBuilder.Bind<Class1Test>().FromNew();

            containerBuilder.Bind<Class3Test>()
                .FromNew()
                .WhenInit((c, o) => o.Init(
                    c.Resolve<Class1Test>()
                    ));

            IDIContainer container = containerBuilder.Build();

            Class3Test class3Test = container.Resolve<Class3Test>();

            Assert.IsNotNull(class3Test);
            Assert.IsNotNull(class3Test.Class1Test);
        }

        [Test]
        public void DIBuilderTests_SimpleDispose()
        {
            IDIContainerBuilder containerBuilder = new DIContainerBuilder();

            containerBuilder.Bind<Class1Test>().FromNew();

            containerBuilder.Bind<Class3Test>()
                .FromNew()
                .WhenDispose((o) => o.Dispose());

            IDIContainer container = containerBuilder.Build();

            Class3Test class3Test = container.Resolve<Class3Test>();

            container.Dispose();

            Assert.IsNotNull(class3Test);
            Assert.IsTrue(class3Test.Disposed);
        }

        [Test]
        public void DIBuilderTests_AssertResolve()
        {
            IDIContainer container = new DIContainerBuilder().Build();

            Assert.Throws<Exception>(() => container.Resolve<Class1Test>());
        }

        [Test]
        public void DIBuilderTests_InterfaceBind()
        {
            IDIContainerBuilder containerBuilder = new DIContainerBuilder();

            containerBuilder.Bind<IClass1Test, Class1Test>().FromNew();

            IDIContainer container = containerBuilder.Build();

            IClass1Test class1Test = container.Resolve<IClass1Test>();

            Assert.NotNull(class1Test);
        }

        [Test]
        public void DIBuilderTests_InterfaceBindNonAssignableAssert()
        {
            IDIContainerBuilder containerBuilder = new DIContainerBuilder();

            Assert.Throws<Exception>(() => containerBuilder.Bind<IClass1Test, Class2Test>().FromNew());
        }

        [Test]
        public void DIBuilderTests_AssertDuplicateBind()
        {
            IDIContainerBuilder containerBuilder = new DIContainerBuilder();

            containerBuilder.Bind<Class1Test>().FromNew();

            Assert.Throws<Exception>(() => containerBuilder.Bind<Class1Test>().FromNew());
        }

        [Test]
        public void DIBuilderTests_MultipleContainersBind()
        {
            IDIContainerBuilder containerBuilder1 = new DIContainerBuilder();
            containerBuilder1.Bind<Class1Test>().FromNew();
            IDIContainer container1 = containerBuilder1.Build();

            IDIContainerBuilder containerBuilder2 = new DIContainerBuilder();
            containerBuilder2.Bind<Class2Test>()
                .FromFunction((c) => new Class2Test(
                c.Resolve<Class1Test>()
                ));
            containerBuilder2.Bind(container1);
            IDIContainer container2 = containerBuilder2.Build();

            container2.Resolve<Class2Test>();
        }

        [Test]
        public void DIBuilderTests_AssertMultipleContainersWithDuplicatesBind()
        {
            IDIContainerBuilder containerBuilder1 = new DIContainerBuilder();
            containerBuilder1.Bind<Class1Test>().FromNew();
            IDIContainer container1 = containerBuilder1.Build();

            IDIContainerBuilder containerBuilder2 = new DIContainerBuilder();
            containerBuilder2.Bind<Class1Test>().FromNew();
            Assert.Throws<Exception>(() => containerBuilder2.Bind(container1));
        }
    }
}
