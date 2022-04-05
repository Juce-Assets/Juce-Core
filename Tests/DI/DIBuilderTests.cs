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
            IDiContainerBuilderA containerBuilder = new DiContainerBuilderA();

            containerBuilder.Bind<Class1Test>().FromNew();

            IDiContainerA container = containerBuilder.Build();

            Class1Test class1Test = container.Resolve<Class1Test>();

            Assert.IsNotNull(class1Test);
        }

        [Test]
        public void DIBuilderTests_FromInstance()
        {
            Class1Test classTest = new Class1Test();

            IDiContainerBuilderA containerBuilder = new DiContainerBuilderA();

            containerBuilder.Bind<Class1Test>().FromInstance(classTest);

            IDiContainerA container = containerBuilder.Build();

            Class1Test class1Test = container.Resolve<Class1Test>();

            Assert.IsNotNull(class1Test);
        }

        [Test]
        public void DIBuilderTests_FromFunction()
        {
            IDiContainerBuilderA containerBuilder = new DiContainerBuilderA();

            containerBuilder.Bind<Class1Test>().FromFunction((c) => new Class1Test());

            IDiContainerA container = containerBuilder.Build();

            Class1Test class1Test = container.Resolve<Class1Test>();

            Assert.IsNotNull(class1Test);
        }

        [Test]
        public void DIBuilderTests_FromFunctionLateResolve()
        {
            IDiContainerBuilderA containerBuilder = new DiContainerBuilderA();

            containerBuilder.Bind<Class1Test>().FromNew();

            containerBuilder.Bind<Class2Test>()
                .FromFunction((c) => new Class2Test(
                    c.Resolve<Class1Test>()
                    ));

            IDiContainerA container = containerBuilder.Build();

            Class2Test class2Test = container.Resolve<Class2Test>();

            Assert.IsNotNull(class2Test);
        }

        [Test]
        public void DIBuilderTests_AssertCircularResolve()
        {
            IDiContainerBuilderA containerBuilder = new DiContainerBuilderA();

            containerBuilder.Bind<Class5Test>()
                .FromFunction((x) => new Class5Test(
                    x.Resolve<Class6Test>()
                    ));

            containerBuilder.Bind<Class6Test>()
                .FromFunction((x) => new Class6Test(
                    x.Resolve<Class5Test>()
                    ));

            IDiContainerA container = containerBuilder.Build();

            Assert.Throws<Exception>(() => container.Resolve<Class5Test>());
        }

        [Test]
        public void DIBuilderTests_SimpleInit()
        {
            IDiContainerBuilderA containerBuilder = new DiContainerBuilderA();

            containerBuilder.Bind<Class3Test>()
                .FromNew()
                .WhenInit((o) => o.Init);

            IDiContainerA container = containerBuilder.Build();

            Class3Test class3Test = container.Resolve<Class3Test>();

            Assert.IsNotNull(class3Test);
        }

        [Test]
        public void DIBuilderTests_LateResolveInit()
        {
            IDiContainerBuilderA containerBuilder = new DiContainerBuilderA();

            containerBuilder.Bind<Class1Test>().FromNew();

            containerBuilder.Bind<Class3Test>()
                .FromNew()
                .WhenInit((c, o) => o.Init(
                    c.Resolve<Class1Test>()
                    ));

            IDiContainerA container = containerBuilder.Build();

            Class3Test class3Test = container.Resolve<Class3Test>();

            Assert.IsNotNull(class3Test);
            Assert.IsNotNull(class3Test.Class1Test);
        }

        [Test]
        public void DIBuilderTests_SimpleDispose()
        {
            IDiContainerBuilderA containerBuilder = new DiContainerBuilderA();

            containerBuilder.Bind<Class1Test>().FromNew();

            containerBuilder.Bind<Class3Test>()
                .FromNew()
                .WhenDispose((o) => o.Dispose());

            IDiContainerA container = containerBuilder.Build();

            Class3Test class3Test = container.Resolve<Class3Test>();

            container.Dispose();

            Assert.IsNotNull(class3Test);
            Assert.IsTrue(class3Test.Disposed);
        }

        [Test]
        public void DIBuilderTests_AssertResolve()
        {
            IDiContainerA container = new DiContainerBuilderA().Build();

            Assert.Throws<Exception>(() => container.Resolve<Class1Test>());
        }

        [Test]
        public void DIBuilderTests_InterfaceBind()
        {
            IDiContainerBuilderA containerBuilder = new DiContainerBuilderA();

            containerBuilder.Bind<IClass1Test, Class1Test>().FromNew();

            IDiContainerA container = containerBuilder.Build();

            IClass1Test class1Test = container.Resolve<IClass1Test>();

            Assert.NotNull(class1Test);
        }

        [Test]
        public void DIBuilderTests_InterfaceBindNonAssignableAssert()
        {
            IDiContainerBuilderA containerBuilder = new DiContainerBuilderA();

            Assert.Throws<Exception>(() => containerBuilder.Bind<IClass1Test, Class2Test>().FromNew());
        }

        [Test]
        public void DIBuilderTests_AssertDuplicateBind()
        {
            IDiContainerBuilderA containerBuilder = new DiContainerBuilderA();

            containerBuilder.Bind<Class1Test>().FromNew();

            Assert.Throws<Exception>(() => containerBuilder.Bind<Class1Test>().FromNew());
        }

        [Test]
        public void DIBuilderTests_MultipleContainersBind()
        {
            IDiContainerBuilderA containerBuilder1 = new DiContainerBuilderA();
            containerBuilder1.Bind<Class1Test>().FromNew();
            IDiContainerA container1 = containerBuilder1.Build();

            IDiContainerBuilderA containerBuilder2 = new DiContainerBuilderA();
            containerBuilder2.Bind<Class2Test>()
                .FromFunction((c) => new Class2Test(
                c.Resolve<Class1Test>()
                ));
            containerBuilder2.Bind(container1);
            IDiContainerA container2 = containerBuilder2.Build();

            container2.Resolve<Class2Test>();
        }

        [Test]
        public void DIBuilderTests_AssertMultipleContainersWithDuplicatesBind()
        {
            IDiContainerBuilderA containerBuilder1 = new DiContainerBuilderA();
            containerBuilder1.Bind<Class1Test>().FromNew();
            IDiContainerA container1 = containerBuilder1.Build();

            IDiContainerBuilderA containerBuilder2 = new DiContainerBuilderA();
            containerBuilder2.Bind<Class1Test>().FromNew();
            Assert.Throws<Exception>(() => containerBuilder2.Bind(container1));
        }
    }
}
