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

namespace BaiDuTop
{
    public partial class MainForm : Form
    {
        private string m_BaiDu = "http://www.baidu.com/s?wd={0}";
        private Encoding m_Encoding = Encoding.GetEncoding("GB2312");
        private string m_ShowFilter = "全部";
        private int m_WorkStatus = 0;
        private IList<BaiDuResultInfo> m_DataSource;
        int rowIndex = 0;
        public MainForm()
        {
            InitializeComponent();
            this.dgvResult.AutoGenerateColumns = false;
            this.DoubleBuffered = true;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string keyWord = System.Web.HttpUtility.UrlEncode(tbKeyWord.Text.Trim(), m_Encoding);
            string filter = tbFilter.Text;
           // btnSearch.Enabled = false;
            if (rbAll.Checked)
                this.m_ShowFilter = rbAll.Text;
            else if (rbTuiGuang.Checked)
            {
                this.m_ShowFilter = rbTuiGuang.Text;
            }
            else if (rbNormal.Checked)
            {
                this.m_ShowFilter = rbNormal.Text;
            }
            dgvResult.DataSource = null;
            groupBox1.Enabled = false;
            ts_StatusLabel.Text = "正在查询...";
            m_WorkStatus = 0;
            rowIndex = 0;
            //GetBaiDuTop(keyWord);
            Thread thread = new Thread(new ParameterizedThreadStart(GetBaiDuTop));
            thread.IsBackground = true;
            thread.Start(new string[] { keyWord, filter });

            Thread statusThread = new Thread(new ThreadStart(CheckStatus));
            statusThread.IsBackground = true;
            statusThread.Start();

        }

        #region 检查是否已完成
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

        #region 线程安全的控件更新
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

        delegate void UpdateCellValueDelegate(int rowIndex, string columnName, object value);
        void UpdateCellValue(int rowIndex, string columnName, object value)
        {
            if (dgvResult.InvokeRequired)
            {
                UpdateCellValueDelegate dlg = new UpdateCellValueDelegate(UpdateCellValue);
                this.Invoke(dlg, rowIndex, columnName, value);
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

        #region 抓取百度页面
        void GetBaiDuTop(object param) {
            string[] ps = (string[])param;
            string keyWord = ps[0];
            string filter = ps[1];
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(string.Format(m_BaiDu,keyWord));
            req.CookieContainer = new CookieContainer();         
            try
            {
                using (HttpWebResponse response = (HttpWebResponse)req.GetResponse())
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        StreamReader reader = new StreamReader(response.GetResponseStream(), m_Encoding);
                        string html = reader.ReadToEnd();
                        ResolveResult(html, filter);
                    }
                }
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.m_WorkStatus = 3;
            }


        }
        #endregion

        #region 解析页面内容
        void ResolveResult(string html, string filter)
        {

            string[] filters = filter.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            IList<string> filterList = new List<string>();
            foreach (string f in filters)
                filterList.Add(f);
            string strTuiGuang1 = "<table id=\"(\\d+)\"  width=\"65%\" cellspacing=\"0\" cellpadding=\"0\" border=\"0\" bgcolor=\"#f5f5f5\".*<font size=\"3\" style=\"text\\-decoration:underline;\">(.*)</font><font size=\"-1\" color=\"#008000\" style=\"margin-left:6px;\">(.*?)</font>";

            string strTuiGuang = "<table id=\"(\\d+)\"  class=\"ec_pp_f\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\">.*<font size=-1>(.*)<br><font size=-1 color=\"#008000\">(.*) \\d{4}-\\d{1,2}</font>.*</table>";
            string strResult = "<table cellpadding=\"0\" cellspacing=\"0\" class=\"result\" id=\"(\\d+)\">.*<font size=\"3\">.*</font></a><br><font size=-1>(.*)<br><font color=\"#008000\">(.*)/? \\d{4}-\\d{1,2}-\\d{1,2}  </font>";

            Regex reTuiGuang1 = new Regex(strTuiGuang1, RegexOptions.Singleline);
            Regex reTuiGuang = new Regex(strTuiGuang);
            Regex reResult = new Regex(strResult);
            IList<BaiDuResultInfo> resultList = new List<BaiDuResultInfo>();
            
            int index = 1;
            
            
           
                MatchCollection mcTuiGuang1 = reTuiGuang1.Matches(html);
                MatchCollection mcTuiGuang = reTuiGuang.Matches(html);
                //推广1
                foreach (Match m in mcTuiGuang1)
                {
                    BaiDuResultInfo bdri = new BaiDuResultInfo();
                    bdri.ID = index++;
                    bdri.Title = m.Groups[2].Value.Trim();
                    bdri.Url = m.Groups[3].Value.Trim();
                    // bdri.IP = GetIPAddress(bdri.Url);
                    bdri.Type = BaiDuResultInfo.ResultType.推广;
                    if (!filterList.Contains(bdri.Url) && (this.m_ShowFilter == "推广" || this.m_ShowFilter == "全部"))
                    {
                        resultList.Add(bdri);
                        bdri.RowIndex = rowIndex++;
                        ResolveIPAddress(resultList);
                    }
                }

                //推广
                foreach (Match m in mcTuiGuang)
                {
                    BaiDuResultInfo bdri = new BaiDuResultInfo();
                    bdri.ID = index++;
                    bdri.Title = m.Groups[2].Value.Trim();
                    bdri.Url = m.Groups[3].Value.Trim();
                    // bdri.IP = GetIPAddress(bdri.Url);
                    bdri.Type = BaiDuResultInfo.ResultType.推广;
                    if (!filterList.Contains(bdri.Url) && (this.m_ShowFilter == "推广" || this.m_ShowFilter == "全部"))
                    {
                        resultList.Add(bdri);
                        bdri.RowIndex = rowIndex++;
                        ResolveIPAddress(resultList);
                    }
                }
           
             
                MatchCollection mcResult = reResult.Matches(html);
                //普通结果
                foreach (Match m in mcResult)
                {
                    BaiDuResultInfo bdri = new BaiDuResultInfo();
                    bdri.ID = index++;
                    bdri.Title = m.Groups[2].Value.Trim();
                    bdri.Url = m.Groups[3].Value.Trim();
                    bdri.Type = BaiDuResultInfo.ResultType.普通;
                    if (!filterList.Contains(bdri.Url) && (this.m_ShowFilter == "普通" || this.m_ShowFilter == "全部"))
                    {
                        resultList.Add(bdri);
                        bdri.RowIndex = rowIndex++;
                        ResolveIPAddress(resultList);
                    }
                }
             
            m_DataSource = resultList;
            BindData(resultList);
            m_WorkStatus++;
            Thread pingThread = new Thread(new ParameterizedThreadStart(PingServer));
            pingThread.IsBackground = true;
            pingThread.Start(resultList);
            HttpGet(resultList);

        }
        #endregion

        #region 解析IP地址
        private void ResolveIPAddress(IList<BaiDuResultInfo> resultList)
        {
            try
            {
                BaiDuResultInfo origin = resultList[resultList.Count-1];
                IPAddress[] ips = Dns.GetHostAddresses(origin.Url);
                if (ips.Length == 0)
                    resultList[resultList.Count - 1].IP = "未知";
                else if (ips.Length == 1)
                {//单ＩＰ
                    resultList[resultList.Count - 1].IP = ips[0].ToString();

                }
                else
                {
                    resultList[resultList.Count - 1].IP = ips[0].ToString();
                    for (int i = 1; i < ips.Length; i++)
                    {
                        BaiDuResultInfo bdri = new BaiDuResultInfo();
                        bdri.ID = origin.ID;
                        bdri.Type = origin.Type;
                        bdri.Title = "";
                        bdri.Url = "";
                        bdri.IP = ips[i].ToString();
                        bdri.RowIndex = rowIndex++;
                        resultList.Add(bdri);

                    }
                }
                
            }
            catch
            {
                resultList[resultList.Count - 1].IP = "未知";
            }
        }
        #endregion

        #region Connect服务器
        void PingServer(object p)
        {
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

        #region HttpGet测试
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
                    req = (HttpWebRequest)HttpWebRequest.Create("http://" + lastDomain);
                    req.UserAgent = "Mozilla/5.0 (BaiduTop Crawler v1.0)";
                    req.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
                    req.Method = "GET";
                    req.Proxy = new WebProxy(bd.IP, 80);
                    req.Timeout = 10 * 1000;
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

        #region 类别筛选
        private void rbAll_Click(object sender, EventArgs e)
        {
            if (this.m_DataSource == null)
                return;
            RadioButton rb = (RadioButton)sender;
            
            if (rb.Text == "全部")
            {

                dgvResult.DataSource = m_DataSource;
            }
            else if (rb.Text == "普通")
            {
                IList<BaiDuResultInfo> temp = new List<BaiDuResultInfo>();
                foreach (BaiDuResultInfo bd in m_DataSource)
                {
                    if (bd.Type == BaiDuResultInfo.ResultType.普通)
                    {
                        temp.Add(bd);
                    }
                }
                dgvResult.DataSource = temp;
            }
            else if (rb.Text == "推广")
            {
                IList<BaiDuResultInfo> temp = new List<BaiDuResultInfo>();
                foreach (BaiDuResultInfo bd in m_DataSource)
                {
                    if (bd.Type == BaiDuResultInfo.ResultType.推广)
                    {
                        temp.Add(bd);
                    }
                }
                dgvResult.DataSource = temp;
            }

        }
        #endregion

        #region Http状态筛选
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
    }
}
