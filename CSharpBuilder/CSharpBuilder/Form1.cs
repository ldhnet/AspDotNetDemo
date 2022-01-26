using CSharpBuilder;
using System.Data.SqlClient;

namespace CSharpBuilder
{
    public partial class Form1 : Form
    { 
        ICreator Creator =new sqlDbCreater();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.databaseurl.Text = ".";
            this.username.Text = "sa";
            this.userpwd.Text = "";
        }
         
        private void button2_Click(object sender, EventArgs e)
        {
            string _dburl= databaseurl.Text;
            string _username = username.Text;
            string _userpwd = userpwd.Text;
            if (string.IsNullOrWhiteSpace(_dburl) || string.IsNullOrWhiteSpace(_username) || string.IsNullOrWhiteSpace(_userpwd))
                ErrorMsg("登录信息输入有误!");

            string mConnString = $"Server={_dburl};User Id={_username};Password={_userpwd};";
             
            if (Creator.IsConnDB(mConnString))
            {
                Creator.InitConn(mConnString);
                Creator.ConnDB();
                var list = Creator.GetDatabases().ToArray();
                Databaselist.Items.Clear();
                Databaselist.Items.AddRange(list);
                SetComboBoxSelected(Databaselist, list[0]);

                Msg("登录成功");
            }
            else
            {
                Msg("登录失败");
            }       
        }
   

        private void Databaselist_SelectedIndexChanged(object sender, EventArgs e)
        {
            var dbName= Databaselist.SelectedItem.ToString();
            if (string.IsNullOrWhiteSpace(dbName))
                ErrorMsg("请选择数据库!");
            string _dburl = databaseurl.Text;
            string _username = username.Text;
            string _userpwd = userpwd.Text;
            string mConnString = $"Server={_dburl};Database={dbName};User Id={_username};Password={_userpwd};";

            Creator.InitConn(mConnString);
            Creator.ConnDB();

            var list = Creator.GetTables().ToArray();
            tablelist.Items.Clear();
            tablelist.Items.AddRange(list); 
        }


        private void createEntity_Click(object sender, EventArgs e)
        {
            var tableName = tablelist.Text;
            if (string.IsNullOrWhiteSpace(tableName))
                ErrorMsg("请选择表!");

            Creator.InitTableName(tableName);
            var namespase = textBox1.Text;
            if (string.IsNullOrWhiteSpace(namespase))                
            {
                //Msg("请输入命名空间!");
            }
            var strEntity =  Creator.CreateEntity(namespase, tableName, "C:/demo/");             
            richTextBox1.Clear();
            richTextBox1.Text = strEntity; 
        }
        private void button3_Click(object sender, EventArgs e)
        {
            var tableName = tablelist.Text;
            if(string.IsNullOrWhiteSpace(tableName))
                ErrorMsg("请选择表!");
            Creator.InitTableName(tableName);

            var namespase = textBox1.Text;
            if (string.IsNullOrWhiteSpace(namespase))
                ErrorMsg("请输入命名空间!");
            var strEntity = Creator.CreateEntity(namespase, tableName, "C:/demo/"); 
            EntityCreater.SaveStrToFile(strEntity, $"C:/demo/{tableName}.cs");

            Msg("操作成功!");
        }

        #region 私有方法
        /// <summary>
        /// 错误提示
        /// </summary>
        /// <param name="message"></param>
        private void ErrorMsg(string message)
        {
            MessageBox.Show(message, "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information); 
        }
        /// <summary>
        /// 提示
        /// </summary>
        /// <param name="message"></param>
        private void Msg(string message)
        {
            MessageBox.Show(message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void SetComboBoxSelected(ComboBox comboBox, string key)
        { 
            for (int i = 0; i < comboBox.Items.Count; i++)
            { 
                 dynamic obj = comboBox.Items[i];
                if (obj.ToString() == key)
                { 
                    string text = obj.ToString();
                    comboBox.SelectedIndex = comboBox.FindString(text);
                }
            }
        }

        #endregion
         
    }
}