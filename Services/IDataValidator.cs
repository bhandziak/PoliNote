namespace PoliNote.Services
{
    public interface IDataValidator<T>
    {
        public void ValidateOrThrow(T value);
    }
}
