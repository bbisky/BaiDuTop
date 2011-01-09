using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Text.RegularExpressions;

namespace WebServerScan
{
    class BaiDuResultInfo
    {
      
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
            }
        }
        private string m_Url;

        public string Url
        {
            get {
                return this.m_Url;
            }
            set { 
                this.m_Url = value;
                if (!string.IsNullOrEmpty(this.m_Url))
                {
                    if(!this.m_Url.ToLower().StartsWith("http"))
                        this.m_Url = "http://" + this.m_Url;
                    m_Domain = this.m_Url.ToLower().Replace("http://", "");
                    m_Domain = m_Domain.Replace("https://", "");

                    if (m_Domain.IndexOf("/") > 0)
                        m_Domain = m_Domain.Substring(0, m_Domain.IndexOf("/"));
                }
                
            }
        }
        private string m_Domain;
        public string Domain
        {
            get {
                return this.m_Domain;
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
                    return WebServerScan.Properties.Resources.greendot;
                else
                    return WebServerScan.Properties.Resources.reddot;
            }
        }
       
        private string m_HttpStatus;

        public string HttpStatus
        {
            get { return m_HttpStatus; }
            set { m_HttpStatus = value; }
        }

        private long m_ContentLength;

        public long ContentLength
        {
            get { return m_ContentLength; }
            set { m_ContentLength = value; }
        }
       
    }
}
