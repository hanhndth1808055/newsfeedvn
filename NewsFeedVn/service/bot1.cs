using HtmlAgilityPack;
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
        public Task Execute(IJobExecutionContext context)
        {
            var task = Task.Run(() =>
            {
                try
                {
                    //Debug.WriteLine(DateTime.Now);
                    //get the page
                    var web = new HtmlWeb();
                    var document = web.Load("https://vnexpress.net/tieu-chuan-ban-chap-hanh-trung-uong-khoa-moi-4099490.html");
                    var page = document.DocumentNode;

                    var title = page.QuerySelector("div.sidebar-1>h1.title-detail").InnerText;
                    var description = page.QuerySelector("div.sidebar-1>p.description").InnerText;
                    var paragraphs = page.QuerySelectorAll("article.fck_detail>p.Normal");
                    Debug.WriteLine(title);
                    Debug.WriteLine(description);

                    // selector => <p>
                    // selector => <img>
                    // html text

                    // <p class="sdsadds">
                    var abc = "";

                    foreach (var p in paragraphs)
                    {

                    }

                    //loop through all div tags with item css class
                    // foreach (var item in page.QuerySelectorAll("h3.title-news>a"))
                    // {
                    //     var url = item.GetAttributeValue("href", "");
                    //     Debug.WriteLine(url);
                    //     var date = DateTime.Parse(item.QuerySelector("span:eq(2)").InnerText);
                    //     var description = item.QuerySelector("span:has(b)").InnerHtml;
                    // }

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