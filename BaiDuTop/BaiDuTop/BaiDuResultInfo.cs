using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Text.RegularExpressions;

namespace BaiDuTop
{
    class BaiDuResultInfo
    {
        private Regex reHtmlTag = new Regex("</?(.*?)>", RegexOptions.IgnoreCase);

        private int m_RowIndex;

        public int RowIndex
        {
            get { return m_RowIndex; }
            set { m_RowIndex = value; }
        }
        private int m_Id;

        public int ID
        {
            get { return m_Id; }
            set { m_Id = value; }
        }
        private string m_Title;

        public string Title
        {
            get { return m_Title; }
            set { 
                m_Title = value;
            this.m_Title = reHtmlTag.Replace(this.m_Title, "");
            }
        }
        private string m_Url;

        public string Url
        {
            get {
                string domain = reHtmlTag.Replace(this.m_Url, "");
                if (domain.IndexOf("/") > 0)
                    domain = domain.Substring(0, domain.IndexOf("/"));
                return domain;
            }
            set { 
                this.m_Url = value;                
            }
        }

        public string OriginUrl
        {
            get {
                return this.m_Url;
            }
        }

        private string m_IP;
        public string IP
        {
            get { return m_IP; }
            set { m_IP = value; }
        }
        private bool m_IsAlive;
        public bool IsAlive
        {
            get { return m_IsAlive; }
            set { m_IsAlive = value; }
        }
        
        public Image AliveImage
        {
            get {
                if (this.m_IsAlive)
                    return BaiDuTop.Properties.Resources.greendot;
                else
                    return BaiDuTop.Properties.Resources.reddot;
            }
        }
        private ResultType m_Type;

        public  ResultType Type
        {
            get { return m_Type; }
            set { m_Type = value; }
        }
        private string m_HttpStatus;

        public string HttpStatus
        {
            get { return m_HttpStatus; }
            set { m_HttpStatus = value; }
        }
        internal enum ResultType
        { 
            推广,
            普通
        }
    }
}
