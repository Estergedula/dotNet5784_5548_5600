namespace BlApi;

/// <summary>
/// A class that initializes the implementing class and has a method named Get of the factory that will
/// return an object initialized from a type that implements the IBl interface.
/// </summary>
public static class Factory
{
    public static IBl Get() => new BlImplementation.Bl();
}
