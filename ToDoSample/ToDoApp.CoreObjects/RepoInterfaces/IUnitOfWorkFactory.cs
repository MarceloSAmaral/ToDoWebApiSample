namespace ToDoApp.CoreObjects.RepoInterfaces
{
    public interface IUnitOfWorkFactory
    {
        IUnitOfWork Create();
    }
}
