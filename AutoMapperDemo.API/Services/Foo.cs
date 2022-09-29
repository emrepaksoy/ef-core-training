namespace AutoMapperDemo.API.Services
{
    public interface IFoo
    {
        Guid GetValue();
    }
    public class Foo : IFoo
    {
        Guid _value { get; set; }
        public Foo()
        {
            _value = Guid.NewGuid();
        }
        public Guid GetValue()
        {
            return _value;
        }
    }

    public interface IBar
    {
        Guid Getvalue2();

    }

    public class Bar : IBar
    {
        private readonly IFoo _foo;
        public Bar(IFoo foo)
        {
            _foo = foo;
        }

        public Guid Getvalue2()
        {
            return _foo.GetValue();
        }
    }
}
