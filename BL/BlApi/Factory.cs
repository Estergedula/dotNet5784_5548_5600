
namespace BlApi;

public static class Factory
{
    public static IBl get() => new BlImplementation.Bl();
}
