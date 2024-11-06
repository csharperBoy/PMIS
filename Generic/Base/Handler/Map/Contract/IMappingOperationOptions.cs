namespace Generic.Base.Handler.Map.Contract
{
    public interface IGenericMappingOperationOptions
    {
        Task BeforeMap(Action<object, object> beforeFunction);
        Task AfterMap(Action<object, object> afterFunction);
        //Task BeforeMap(Action<TSource, TDestination> beforeFunction);
        //Task AfterMap(Action<TSource, TDestination> afterFunction);
    }
    public sealed class MappingOperationOptions : IGenericMappingOperationOptions
    {
        public Action<object, object> BeforeMapAction { get; private set; }
        public Action<object, object> AfterMapAction { get; private set; }

        public Task BeforeMap(Action<object, object> beforeFunction)
            => Task.FromResult(BeforeMapAction = beforeFunction);

        public Task AfterMap(Action<object, object> afterFunction)
            => Task.FromResult(AfterMapAction = afterFunction);

        Task IGenericMappingOperationOptions.BeforeMap(Action<object, object> beforeFunction)
            => Task.FromResult(BeforeMapAction = (s, d) => beforeFunction(s, d));

        Task IGenericMappingOperationOptions.AfterMap(Action<object, object> afterFunction)
            => Task.FromResult(AfterMapAction = (s, d) => afterFunction(s, d));
    }
}