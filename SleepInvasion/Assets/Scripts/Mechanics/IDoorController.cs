namespace Mechanics
{
    public interface IDoorController
    {
        public void Use();
        public void Close();
        public void Open();
        public bool IsOpen();
    }
}