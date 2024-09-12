namespace Tsu.IndividualPlan.Domain.Security;

public abstract class BaseSecurityService<T>
{
    public abstract Task validateCanUse(T item);
}