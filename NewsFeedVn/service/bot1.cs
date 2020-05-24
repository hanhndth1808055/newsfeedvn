using NewsFeedVn.Models;
using Quartz;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Fizzler.Systems.HtmlAgilityPack;



namespace NewsFeedVn.service
{
    public class bot1 : IJob
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public Task Execute(IJobExecutionContext context)
        {
            var task = Task.Run(() =>
            {
                getData();
            });
            return task;
        }
        public void getData()
        {
            var task = Task.Run(() =>
            {
                Debug.WriteLine("start get api");
                try
                {
                    List<Source> sources = db.Sources.ToList();
                    for (int i = 0; i < sources.Count; i++)
                    {
                        Debug.WriteLine(sources[i].Status);
                        if (sources[i].Status.ToString().Equals("ACTIVE"))
                        {
                            Debug.WriteLine("url active");
                            var web = new HtmlAgilityPack.HtmlWeb();
                            var document = web.Load(sources[i].Domain + sources[i].Path);
                            var page = document.DocumentNode;

                            foreach (var item in page.QuerySelectorAll(sources[i].Link_selector))
                            {
                                var url = item.GetAttributeValue("href", "");
                                Debug.WriteLine(url);
                                //Id,CategoryID,SourceId,Title,Content,Status,Url,CreatedAt,EditedAt,DeletedAt
                                Article article = new Article()
                                {
                                    CreatedAt = DateTime.Now,
                                    SourceId = sources[i].Id,
                                    CategoryID = 1,
                                    Url = url,
                                    Status = 0
                                };
                                db.Articles.Add(article);
                                db.SaveChanges();
                            }
                        }
                    };
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            });
        }
    }
}   