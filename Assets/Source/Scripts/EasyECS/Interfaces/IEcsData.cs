namespace Source.EasyECS.Interfaces
{
    public interface IEcsData<in T1, in T2, in T3, in T4, in T5, in T6, in T7> : IEcsComponent
    {
        public void InitializeValues(T1 argument1, T2 argument2, T3 argument3, T4 argument4, T5 argument5, T6 argument6, T7 argument7);
    }

    public interface IEcsData<in T1, in T2, in T3, in T4, in T5, in T6> : IEcsComponent
    {
        public void InitializeValues(T1 argument1, T2 argument2, T3 argument3, T4 argument4, T5 argument5, T6 argument6);
    }

    public interface IEcsData<in T1, in T2, in T3, in T4, in T5> : IEcsComponent
    {
        public void InitializeValues(T1 argument1, T2 argument2, T3 argument3, T4 argument4, T5 argument5);
    }

    public interface IEcsData<in T1, in T2, in T3, in T4> : IEcsComponent
    {
        public void InitializeValues(T1 argument1, T2 argument2, T3 argument3, T4 argument4);
    }

    public interface IEcsData<in T1, in T2, in T3> : IEcsComponent
    {
        public void InitializeValues(T1 argument1, T2 argument2, T3 argument3);
    }

    public interface IEcsData<in T1, in T2> : IEcsComponent
    {
        public void InitializeValues(T1 argument1, T2 argument2);
    }

    public interface IEcsData<in T> : IEcsComponent
    {
        public void InitializeValues(T argument);
    }

    public interface IEcsData : IEcsComponent
    {
    }

    public interface IEcsComponent {}
    
    public interface IEcsMark : IEcsComponent
    {
        
    }
    
    public interface IEcsRequestMark : IEcsComponent
    {
        
    }
    
    public interface IEcsRequestData<in T1, in T2, in T3, in T4, in T5, in T6, in T7> : IEcsComponent
    {
        public void InitializeValues(T1 argument1, T2 argument2, T3 argument3, T4 argument4, T5 argument5, T6 argument6, T7 argument7);
    }

    public interface IEcsRequestData<in T1, in T2, in T3, in T4, in T5, in T6> : IEcsComponent
    {
        public void InitializeValues(T1 argument1, T2 argument2, T3 argument3, T4 argument4, T5 argument5, T6 argument6);
    }

    public interface IEcsRequestData<in T1, in T2, in T3, in T4, in T5> : IEcsComponent
    {
        public void InitializeValues(T1 argument1, T2 argument2, T3 argument3, T4 argument4, T5 argument5);
    }

    public interface IEcsRequestData<in T1, in T2, in T3, in T4> : IEcsComponent
    {
        public void InitializeValues(T1 argument1, T2 argument2, T3 argument3, T4 argument4);
    }

    public interface IEcsRequestData<in T1, in T2, in T3> : IEcsComponent
    {
        public void InitializeValues(T1 argument1, T2 argument2, T3 argument3);
    }

    public interface IEcsRequestData<in T1, in T2> : IEcsComponent
    {
        public void InitializeValues(T1 argument1, T2 argument2);
    }

    public interface IEcsRequestData<in T> : IEcsComponent
    {
        public void InitializeValues(T argument);
    }
    
    

}