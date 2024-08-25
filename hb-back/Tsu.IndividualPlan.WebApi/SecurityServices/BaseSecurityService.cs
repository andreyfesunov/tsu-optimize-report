namespace Tsu.IndividualPlan.WebApi.SecurityServices;

public abstract class BaseSecurityService<T>
{
    public abstract Task validateCanUse(T item);
}