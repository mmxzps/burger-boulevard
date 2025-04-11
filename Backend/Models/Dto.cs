namespace Backend.Models;

public interface IIntoDto<T>
{
  T ToDto();
}
