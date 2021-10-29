
// See https://aka.ms/new-console-template for more information
using PressureTest;


string url = "http://localhost:6001/Home/PostImfo";

HttpResult httpResult = new HttpHelper().GetHtml(new HttpItem
{
    URL = "http://localhost:6001/Home/Privacy",
    ContentType = "text/html; charset=gb2312"
});
 
try
{ 
    var dto = new Employee
    {
        Name = "tom"
    };
    for (int i = 0; i < 1200; i++)
    {
        var strResult = HttpHelper.HttpPost(url, JsonHelper.ToJson(dto));
        var result = JsonHelper.FromJson<Employee>(strResult);
   
    }

}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}
Console.WriteLine("1--Hello, World!");

Console.WriteLine("2--Hello, World!");

Console.WriteLine("Hello, World!");