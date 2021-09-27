namespace Juce.Core.DI.Data
{
    public class Class2Test
    {
        private readonly Class1Test class1Test;

        public Class2Test(Class1Test class1Test)
        {
            this.class1Test = class1Test;
        }
    }
}
