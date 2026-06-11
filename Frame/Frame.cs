using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frame
{
    public partial class Frame
    {
        private static readonly Frame instance = new Frame();
        private Frame()
        {

        }

        public static Frame Instance
        {
            get { return instance; }
        }

        private readonly DataBase db = new DataBase();

        public BaseConfig SystemConfig;

        public VerbOnStorage Storage { get; private set; }

        public bool Start(out string reason)
        {
            reason = string.Empty;

            try
            {
                //连接数据库
                db.Connect();
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
