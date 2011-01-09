using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using System.Net.Sockets;
using System.Net.NetworkInformation;
using System.Threading;

namespace WebServerScan
{
    public partial class MainForm : Form
    {               
        private IList<BaiDuResultInfo> m_DataSource;
        private int m_WorkStatus = 0;
        int rowIndex = 0;
        public MainForm()
        {
            InitializeComponent();
            this.dgvResult.AutoGenerateColumns = false;
            this.DoubleBuffered = true;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tbUrls.Text))
            {
                dgvResult.DataSource = null;
                groupBox1.Enabled = false;
                ts_StatusLabel.Text = "正在查询...";
                m_WorkStatus = 0;
                rowIndex = 0;
                string scanClass = "C";
                if (rb_ClassB.Checked)
                    scanClass = "B";
                //GetBaiDuTop(keyWord);
                Thread thread = new Thread(new ParameterizedThreadStart(ProcessUrls));
                thread.IsBackground = true;
                thread.Start(new string[]{tbUrls.Text,scanClass});

                Thread statusThread = new Thread(new ThreadStart(CheckStatus));
                statusThread.IsBackground = true;
                statusThread.Start();
            }
        }
        #region 处理URL
        void ProcessUrls(object p) {
            IList<BaiDuResultInfo> resultList = new List<BaiDuResultInfo>();
            int index = 1;
            string[] pas = p as string[];
            string urls = pas[0];
            string scanClass = pas[1];

            //string[] urlList = urls.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            //foreach (string url in urlList)
            //{
                BaiDuResultInfo bdri = new BaiDuResultInfo();
                bdri.ID = index++;
              //  bdri.Title = m.Groups[2].Value.Trim();
                bdri.Url = urls;
                // bdri.IP = GetIPAddress(bdri.Url);
               // bdri.Type = BaiDuResultInfo.ResultType.推广;
               
                 
                 bdri.RowIndex = rowIndex++;
                 resultList.Add(bdri);
                 ResolveIPAddress(resultList, scanClass);
                
           // }
            this.m_WorkStatus++;
            m_DataSource = resultList;
            BindData(resultList);

            Thread pingThread = new Thread(new ParameterizedThreadStart(PingServer));
            pingThread.IsBackground = true;

            pingThread.Start(resultList);
            HttpGet(resultList);
        }

        void CheckStatus() {
            while (m_WorkStatus < 3)
            {
                Thread.Sleep(100);
            }
            EnableButton();
            RefreshGrid();
            ts_StatusLabel.Text = "操作完成;)";
        }
        #endregion

        #region 线程安全的控件操作方法
        delegate void EnableButtonDelegate();
        void EnableButton() {
            if (groupBox1.InvokeRequired)
            {
                EnableButtonDelegate dlg = new EnableButtonDelegate(EnableButton);
                this.Invoke(dlg);
            }
            else
                groupBox1.Enabled = true;
            if (plStatus.InvokeRequired)
            {
                EnableButtonDelegate dlg = new EnableButtonDelegate(EnableButton);
                this.Invoke(dlg);
            }
            else
                plStatus.Enabled = true;
        }
        delegate void RefreshGridDelegate();
        void RefreshGrid() {
            if (dgvResult.InvokeRequired)
            {
                RefreshGridDelegate dlg = new RefreshGridDelegate(RefreshGrid);
                this.Invoke(dlg);
            }
            else
                dgvResult.Refresh();
        }

        delegate void BindDataDelegate(IList<BaiDuResultInfo> datasource);
        void BindData(IList<BaiDuResultInfo> datasource)
        {
            if (dgvResult.InvokeRequired)
            {
                BindDataDelegate dlg = new BindDataDelegate(BindData);

                this.Invoke(dlg, datasource);
            }
            else
            {
                dgvResult.DataSource = datasource;
            }
        }
        #endregion

        #region 解析IP地址
        private void ResolveIPAddress(IList<BaiDuResultInfo> resultList, string scanClass)
        {
            try
            {
                BaiDuResultInfo origin = resultList[resultList.Count-1];
               


                IPAddress[] ips = Dns.GetHostAddresses(origin.Domain);
                if (ips.Length == 0)
                    resultList[resultList.Count - 1].IP = "未知";
                else if (ips.Length == 1)
                {//单ＩＰ
                    resultList[resultList.Count - 1].IP = ips[0].ToString();
                    AppendClass(resultList, scanClass);
                }
                else
                {
                    resultList[resultList.Count - 1].IP = ips[0].ToString();

                    AppendClass(resultList, scanClass);
                    string lastIp = origin.IP;
                    
                    for (int i = 1; i < ips.Length; i++)
                    {
                        //同段的IP忽略
                        if (!IsSameClass(lastIp, ips[i].ToString(), scanClass))
                        {
                            BaiDuResultInfo bdri = new BaiDuResultInfo();
                            bdri.ID = origin.ID;

                            bdri.Title = "";
                            bdri.Url = "";
                            bdri.IP = ips[i].ToString();
                            bdri.RowIndex = rowIndex++;
                            resultList.Add(bdri);
                            AppendClass(resultList, scanClass);
                            lastIp  = bdri.IP;
                        }
                    }
                }
                
            }
            catch
            {
                resultList[resultList.Count - 1].IP = "未知";
            }
        }

        bool IsSameClass(string ip1, string ip2, string cls)
        {
            if (cls == "C")
            {
                return ip1.Substring(0, ip1.LastIndexOf(".")) == ip2.Substring(0, ip2.LastIndexOf("."));
            }
            else if (cls == "B")
            {
                string[] ip1s = ip1.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
                string[] ip2s = ip1.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
                return ip1s[0] == ip2s[0] && ip1s[1] == ip2s[1] && ip1s[3] == ip2s[3];
            }
            else
                return true;
        }

        void AppendClass(IList<BaiDuResultInfo> resultList, string scanClass)
        {
            string ip = resultList[resultList.Count - 1].IP;
            if (ip.Contains("."))
            {
                string[] ipSeg = ip.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
                if (scanClass == "C")
                {
                    int current = Convert.ToInt32(ipSeg[3]);
                    for (int i = 1; i < 256 ; i++)
                    {
                        if (i == current)
                            continue;
                        BaiDuResultInfo bdri = new BaiDuResultInfo();
                        

                        bdri.Title = "";
                        bdri.Url = "";
                        bdri.IP = string.Format("{0}.{1}.{2}.{3}", ipSeg[0], ipSeg[1], ipSeg[2], i);
                        bdri.RowIndex = rowIndex++;
                        resultList.Add(bdri);
                       
                    }
                }
                else if (scanClass == "B")
                {
                    int current = Convert.ToInt32(ipSeg[2]);
                    for (int i = 1; i < 256; i++)
                    {
                        if (i == current)
                            continue;
                        BaiDuResultInfo bdri = new BaiDuResultInfo();


                        bdri.Title = "";
                        bdri.Url = "";
                        bdri.IP = string.Format("{0}.{1}.{2}.{3}", ipSeg[0], ipSeg[1],i, ipSeg[3]);
                        bdri.RowIndex = rowIndex++;
                        resultList.Add(bdri);

                    }
                }

            }

        }
        #endregion       

        #region Connect服务器测试
        void PingServer(object p)
        {
           // ts_StatusLabel.Text = "正在Connect服务器...";
            IList<BaiDuResultInfo> resultList = p as IList<BaiDuResultInfo>;
            foreach (BaiDuResultInfo bd in resultList)
            {
                Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                try
                {
                    if (bd.IP!=null && bd.IP != "未知")
                        socket.Connect(IPAddress.Parse(bd.IP), 80);
                    else if(!string.IsNullOrEmpty(bd.Url))
                        socket.Connect(bd.Url, 80);                        
                    bd.IsAlive = socket.Connected;
                }
                catch
                {
                    bd.IsAlive = false;
                }
                finally {
                    socket.Close();
                }
               
                UpdateCellValue(bd.RowIndex, "IsAlive", bd.AliveImage);
            }
            m_WorkStatus++;
        }
        #endregion

        #region HttpGet
        void HttpGet(object p)
        {
            ts_StatusLabel.Text = "正在查询WEB状态,此操作时间较长,请耐心等待...";
            IList<BaiDuResultInfo> resultList = p as IList<BaiDuResultInfo>;
            string lastDomain = "";
            HttpWebRequest req;
            foreach (BaiDuResultInfo bd in resultList)
            {
                 
                if (!string.IsNullOrEmpty(bd.Url))
                    lastDomain = bd.Url;
                try
                {
                    req = (HttpWebRequest)HttpWebRequest.Create(lastDomain);
                    req.UserAgent = "Mozilla/5.0 (Windows; U; Windows NT 5.1; zh-CN; rv:1.9.2.13) Gecko/20101203 Firefox/3.6.13";
                    req.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
                    req.Headers[HttpRequestHeader.AcceptLanguage] = "zh-cn,zh;q=0.5";
                    req.Headers[HttpRequestHeader.AcceptCharset] = "GB2312,utf-8;q=0.7,*;q=0.7";
                    req.Method = "GET";                 
                    req.Proxy = new WebProxy(bd.IP, 80);
                    req.Timeout = 5 * 1000;
                    req.CookieContainer = new CookieContainer();

                    using (HttpWebResponse response = (HttpWebResponse)req.GetResponse())
                    {
                        this.m_DataSource[bd.RowIndex].HttpStatus = string.Format("{0}({1})", response.StatusCode, (int)response.StatusCode);
                        UpdateCellValue(bd.RowIndex, "HttpStatus", string.Format("{0}({1})", response.StatusCode, (int)response.StatusCode));
                    }
                }
                catch (Exception ex)
                {
                    UpdateCellValue(bd.RowIndex, "HttpStatus", ex.Message);
                }
                 
            }
            m_WorkStatus++;
        }
        #endregion

        #region 修改单元格内容
        delegate void UpdateCellValueDelegate(int rowIndex, string columnName, object value);
        void UpdateCellValue(int rowIndex,string columnName, object value)
        {
            if (dgvResult.InvokeRequired)
            {
                UpdateCellValueDelegate dlg = new UpdateCellValueDelegate(UpdateCellValue);
               this.Invoke(dlg,rowIndex, columnName, value);
            }
            else
            {
                if (rowIndex < dgvResult.Rows.Count)
                {
                    
                    DataGridViewCell cell = dgvResult.Rows[rowIndex].Cells[columnName];                  
                    cell.Value = value;
                    cell.Style = dgvResult.Rows[rowIndex].Cells[columnName].Style;
                    if (columnName == "HttpStatus" && value.ToString() == "操作超时")
                        cell.OwningRow.DefaultCellStyle.BackColor = Color.Red;
                    //  dgvResult.Refresh();
                }
            }
        }
        #endregion

        #region 筛选
        private void rb_Status_All_Click(object sender, EventArgs e)
        {
            if (this.m_DataSource == null)
                return;
            RadioButton rb = (RadioButton)sender;

            if (rb.Text == "全部")
            {
                dgvResult.DataSource = m_DataSource;
            }
            else if (rb.Text == "超时")
            {
                IList<BaiDuResultInfo> temp = new List<BaiDuResultInfo>();
                foreach (BaiDuResultInfo bd in m_DataSource)
                {
                    if (bd.HttpStatus == "操作超时")
                    {
                        temp.Add(bd);
                    }
                }
                dgvResult.DataSource = temp;
            }
            else if (rb.Text == "其它")
            {
                IList<BaiDuResultInfo> temp = new List<BaiDuResultInfo>();
                foreach (BaiDuResultInfo bd in m_DataSource)
                {
                    if (bd.HttpStatus != "操作超时")
                    {
                        temp.Add(bd);
                    }
                }
                dgvResult.DataSource = temp;
            }

        }
        #endregion

        #region 导入导出文件
        private void btnImport_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK) {
                LoadFile(openFileDialog1.FileName);
            }
        }
        void LoadFile(string path) {
            StreamReader sr = new StreamReader(File.OpenRead(path), Encoding.Default);
            tbUrls.Text = sr.ReadToEnd();
            sr.Close();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK) {
                SaveFile(saveFileDialog1.FileName);
            }
        }

        private void SaveFile(string path)
        {
            StreamWriter sw = new StreamWriter(File.OpenWrite(path));
            sw.Write(tbUrls.Text);
            sw.Flush();
            sw.Close();
        }
        #endregion

        
    }
}
