namespace web.Application.Mappers;

public interface IMapper<T, TO>
{
    public TO MapFrom(T param);
    public T MapTo(TO param);
}