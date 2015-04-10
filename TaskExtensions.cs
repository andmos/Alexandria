using System.Threading.Tasks;

public static class TaskExtensions
{
    public static void RunAndWait(this Task task)
    {
        task.Wait();
        if (!task.IsFaulted)
        {
            return;
        }

        if (task.Exception != null)
        {
            throw task.Exception;
        }
    }

    public static T RunAndWait<T>(this Task<T> task)
    {
        task.Wait();
        if (!task.IsFaulted)
        {
            return task.Result;
        }

        if (task.Exception != null)
        {
            throw task.Exception;
        }

        return task.Result;
    }
}