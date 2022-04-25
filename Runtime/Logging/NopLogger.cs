namespace Juce.Core.Logging
{
    public class NopLogger : ILogger
    {
        public static readonly NopLogger Instance = new NopLogger();

        private NopLogger()
        {

        }

        public void Log(string log)
        {
        
        }

        public void Log<T1>(string log, T1 arg1)
        {
       
        }

        public void Log<T1, T2>(string log, T1 arg1, T2 arg2)
        {
           
        }

        public void Log<T1, T2, T3>(string log, T1 arg1, T2 arg2, T3 arg3)
        {
          
        }

        public void Log<T1, T2, T3, T4>(string log, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {

        }

        public void Log<T1, T2, T3, T4, T5>(string log, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
        {
  
        }

        public void Log<T1, T2, T3, T4, T5, T6>(string log, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6)
        {
         
        }

        public void Log<T1, T2, T3, T4, T5, T6, T7>(string log, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7)
        {
      
        }

        public void Log<T1, T2, T3, T4, T5, T6, T7, T8>(string log, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8)
        {
         
        }

        public void Log<T1, T2, T3, T4, T5, T6, T7, T8, T9>(string log, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9)
        {
        
        }
    }
}
