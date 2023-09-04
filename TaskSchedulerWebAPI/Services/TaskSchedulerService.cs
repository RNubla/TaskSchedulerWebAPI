using System.Collections.Generic;
using System.Threading.Tasks;
using TaskScheduler;

namespace TaskSchedulerWebAPI.Services
{
    public class TaskSchedulerService
    {
        public enum TASK_STATE
        {
            UNKNOWN,
            DISABLED,
            QUEUED,
            READY,
            RUNNING
        }
        Dictionary<int, string> LastTaskResultDict = new Dictionary<int, string>
        {
            {0, "The operation completed successfully" },
            {1, "(0x1)" },
            {267011, "The task has not yet run" },
            //{267009,"The task is currently running" },
            //{268435456, "268435456" },
            //{-2147020576,"-2147020576" }
        };
        ITaskService taskService = new TaskSchedulerClass();
        ITaskFolder rootFolder { get; set; }
        private List<IRegisteredTask> listOfTask = new List<IRegisteredTask>();
        public TaskSchedulerService() {
            //string remoteServerName = ""; // Replace with your server's name or IP address.
            //string userName = "";  // Adjust as needed.
            //string userPassword = "";  // Adjust as needed.
            //taskService.Connect(remoteServerName, userName, userPassword);
            taskService.Connect();
            rootFolder = taskService.GetFolder("\\");     
            ListAllTasksInFolder(rootFolder);

        }
        private void ListAllTasksInFolder(ITaskFolder folder)
        {
            IRegisteredTaskCollection tasks = folder.GetTasks(0);
            foreach(IRegisteredTask task in tasks)
            {
                listOfTask.Add(task);
            }

            // Recursively list tasks in subfolders
            ITaskFolderCollection subFolders = folder.GetFolders(0);
            foreach (ITaskFolder subFolder in subFolders)
            {
                ListAllTasksInFolder(subFolder);
            }
        }
        public List<Task> GetListOfTask()
        {
            List<Task> tasks = new List<Task>();
            //Console.WriteLine(listOfTask);
      
            foreach(IRegisteredTask task in listOfTask)
            {
                Task tempTask = new Task();
                tempTask.Name = task.Name;
                tempTask.Path = task.Path;
                tempTask.State = Enum.GetName(enumType: typeof(TASK_STATE), task.State);
                tempTask.LastRunTime = task.LastRunTime;
                if (!LastTaskResultDict.ContainsKey(task.LastTaskResult))
                {
                    LastTaskResultDict.Add(task.LastTaskResult, task.LastTaskResult.ToString());
                }
                tempTask.LastTaskResult = LastTaskResultDict[task.LastTaskResult];
                tempTask.NextRunTime = task.NextRunTime;
                tempTask.NumberOfMissedRuns = task.NumberOfMissedRuns;
                tempTask.Enabled = task.Enabled;
                tasks.Add(tempTask);

            }
            return tasks;
        }
    }
}
