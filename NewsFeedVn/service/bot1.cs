using Quartz;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;


namespace NewsFeedVn.service
{
    public class bot1 : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            var task = Task.Run(() =>
            {
                try
                {
                 
                    Debug.WriteLine(DateTime.Now);
                    //Do whatever stuff you want
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }

            });
            return task;
        }
    }
}   