namespace BusCatalog.Test.Fakes;

public interface IFakeFactory<T>
{
    static abstract T[] Generate();
}
