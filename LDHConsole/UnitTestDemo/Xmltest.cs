using System.Xml;
using System.Xml.Linq;

namespace UnitTestDemo
{
    public enum Rainbow
    {
        Red,
        Orange,
        Yellow,
        Green,
        Blue,
        Indigo,
        Violet
    }
    public class Xmltest
    {
        public void XmlSave1()
        {
            var doc = new XDocument(
              new XElement("Contacts",
                    new XElement("Contact",
                            new XAttribute("id", "01"),
                    new XElement("Name", "Daisy Abbey"),
                            new XElement("Gender", "female")
                                )
                            )
              );
            doc.Save("test2.xml");
        }
       
        public void XmlUpdate()
        { 

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(@"D:\1MK.xml");

            //XmlNodeList nodeList = xmlDoc.SelectSingleNode("User").ChildNodes;//获取Employees节点的所有子节点

            XmlElement xmlRoot = xmlDoc.DocumentElement; //DocumentElement获取文档的跟

            XmlElement Age = xmlDoc.CreateElement("Age");
            Age.InnerText = "20";
            xmlRoot.AppendChild(Age); 


            foreach (XmlNode xn in xmlRoot.ChildNodes)//遍历所有子节点
            {
                XmlElement xe = (XmlElement)xn;//将子节点类型转换为XmlElement类型

                System.Diagnostics.Debug.WriteLine(xe.Name + xe.InnerText);

                XmlElement xe2 = (XmlElement)xe;//转换类型
                if (xn.Name == "Explain")//如果找到
                {
                    xn.InnerText = "888888"; 
                }
            }
            xmlDoc.Save(@"D:\1MK.xml");//保存。
        } 
        public void GetXmlDocument()
        {
            //创建XML文档类
            XmlDocument xmlDoc = new XmlDocument();
            //加载xml文件
            xmlDoc.Load(@"D:\1MK.xml"); //从指定的位置加载xml文档
            //获取根节点
            XmlElement xmlRoot = xmlDoc.DocumentElement; //DocumentElement获取文档的跟
            //遍历节点
            foreach (XmlNode node in xmlRoot.ChildNodes)
            {

                //根据节点名称查找节点对象 
                System.Diagnostics.Debug.WriteLine(node.Name + node.InnerText);

                System.Diagnostics.Debug.WriteLine(node.InnerXml);

            

            }
        }
       
        public void GetXmlTextReader()
        {
            var aa = System.Environment.CurrentDirectory;
            //System.Environment.CurrentDirectory系统路径，bin-Debug下
            //XmlTextReader 文档流的读取方式，只读
            XmlTextReader reader = new XmlTextReader(@"D:\1MK.xml");

            while (reader.Read())
            {
                if (reader.Name == "User") //判断节点名称
                {
                    System.Diagnostics.Debug.WriteLine(reader.ReadString());//将节点内容读取为一个字符串
                }
                else if (reader.Name == "ID")
                {
                    System.Diagnostics.Debug.WriteLine(reader.ReadString());
                }
                else if (reader.Name == "programName")
                {
                    System.Diagnostics.Debug.WriteLine(reader.ReadString());
                }

            }
            reader.Close(); //关闭文档流 
        }

        public void XmlSave() //生成
        {
            //创建XmlDocument对象
            XmlDocument document = new XmlDocument();
            //xml文档的声明部分
            XmlDeclaration declaration = document.CreateXmlDeclaration("1.0", "UTF-8", "");//xml文档的声明部分
            document.AppendChild(declaration);//添加至XmlDocument对象中
            //创建用户对象
            User xrz = new User() { ID = "1", Name = "MK", Age = "20", Explain = "666666" };
            //创建根节点User
            XmlElement User = document.CreateElement("User");//CreateElement（节点名称）
            //设置根节点的属性
            User.SetAttribute("Type", "员工");
            //创建子节点ID
            XmlElement ID = document.CreateElement("ID");
            ID.InnerText = xrz.ID; //设置其值
            XmlElement Name = document.CreateElement("Name");
            Name.InnerText = xrz.Name;
            XmlElement Age = document.CreateElement("Age");
            Age.InnerText = xrz.Age;
            XmlElement Explain = document.CreateElement("Explain");
            Explain.InnerText = xrz.Explain;
            //添加至父节点User中
            User.AppendChild(ID);
            User.AppendChild(Name);
            User.AppendChild(Age);
            User.AppendChild(Explain);
            //将根节点添加至XML文档中
            document.AppendChild(User);
            //保存输出路径
            document.Save(@"D:\" + xrz.ID + xrz.Name + ".xml");

            ///>
            ///注意要把根节点添加至XML文档中
            ///多层级只需要在其子节点使用AppendChild再次添加即可
            ///使用XmlElement对象的SetAttribute方法设置其属性
            ///在保存路径的时候最好使用异常处理
        }
    
    }
    class User
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Age { get; set; }
        public string Explain { get; set; }
    }
}