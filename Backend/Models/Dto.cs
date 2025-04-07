namespace Backend.Models;

public interface IntoDto<T>
{
  T ToDto();
}
