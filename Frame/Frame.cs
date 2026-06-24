using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frame
{
    public partial class Frame
    {
        string ip = string.Empty;
        public Frame(string ip)
        {
            this.ip = ip;
        }

        private readonly DataBase db = new DataBase();

        public BaseConfig SystemConfig;

        public VerbOnStorage Storage { get; private set; }

        /// <summary>
        /// 连接数据库
        /// </summary>
        /// <param name="reason"></param>
        /// <returns></returns>
        public bool Start(out string reason)
        {
            reason = string.Empty;

            try
            {
                //连接数据库
                db.Connect(ip);
                Storage = db;

                db.Start();
                return true;
            }
            catch(Exception ex)
            {
                reason = ex.Message;
                return false;
            }
        }
    }
}
