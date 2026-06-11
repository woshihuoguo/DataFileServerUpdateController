using System;
using LT.Common.IO.Storage.Model;

namespace LT.SuperTranCockpit.Entity
{
    [Entity(Name = "inspectionresult")]
    public class InspectionResult : IEntity, IAutoClear
    {
        public const string SysIDName = "SysID";
        [Id(Name = SysIDName, Type = PropertyType.长整数, IsNative = true)]
        public virtual long SysID { get; set; }
        /// <summary>
        /// ICW发来的uniqueID
        /// </summary>
        [Property(Name = "UniqueID", Type = PropertyType.字符, MaxLength = 38)]
        public virtual string UniqueID { get; set; }
        /// <summary>
        /// 站点名成
        /// </summary>

        [Property(Name = "TestMode", Type = PropertyType.字符, MaxLength = 100)]
        public virtual string TestMode { get; set; }

        /// <summary>
        /// 二维码
        /// </summary>
        [Property(Name = "CellID", Type = PropertyType.字符, MaxLength = 200)]
        public virtual string CellID { get; set; }

        [Property(Name = "PLCID", Type = PropertyType.整数, MaxLength = 11)]
        public virtual int PLCID { get; set; }

        [Property(Name = "StageIndex", Type = PropertyType.整数, MaxLength = 11)]
        public virtual int StageIndex { get; set; }

        /// <summary>
        /// 检测结果；OK、NG
        /// </summary>
        [Property(Name = "Result", Type = PropertyType.字符, MaxLength = 10)]
        public virtual string Result { get; set; }

        /// <summary>
        /// 产品结果状态，取值有Normal、Break、LostImage、Timeout、Other
        /// </summary>
        [Property(Name = "Status", Type = PropertyType.字符, MaxLength = 20)]
        public virtual string Status { get; set; }

        /// <summary>
        /// 产品主缺陷ID（为算法输出的枚举ID数值，需要通过一个csv配置映射为各种Code和中文名和描述，参见图8-1），根据优先级；如果有二次分类按照分类优先级，如果没有用第一个缺陷
        /// </summary>
        [Property(Name = "Resulttype", Type = PropertyType.单精度浮点数)]
        public virtual float Resulttype { get; set; }

        /// <summary>
        /// 品主缺陷名，是用resulttype通过csv配置映射得到的，参见图8-1
        /// </summary>
        [Property(Name = "Resultname", Type = PropertyType.字符, MaxLength = 150)]
        public virtual string Resultname { get; set; }

        /// <summary>
        /// 产品主缺陷对应的image json数组 Index
        /// </summary>
        [Property(Name = "Resultimageindex", Type = PropertyType.单精度浮点数)]
        public virtual float Resultimageindex { get; set; }

        /// <summary>
        /// 产品主缺陷对应的defect json数组Index
        /// </summary>
        [Property(Name = "Resuldefectindex", Type = PropertyType.单精度浮点数)]
        public virtual float Resuldefectindex { get; set; }

        [Property(Name = "Process_time", Type = PropertyType.单精度浮点数)]
        public virtual float Process_time { get; set; }

        /// <summary>
        /// 检测耗时，毫秒
        /// </summary>
        [Property(Name = "Checktime", Type = PropertyType.单精度浮点数)]
        public virtual float Checktime { get; set; }
        /// <summary>
        /// 模板名,对应主站得索引
        /// </summary>
        [Property(Name = "Modelname", Type = PropertyType.字符, MaxLength = 50)]
        public virtual string Modelname { get; set; }


        [Property(Name = "Starttime", Type = PropertyType.日期)]
        public virtual DateTime? Starttime { get; set; }

        [Property(Name = "Endtime", Type = PropertyType.日期)]
        public virtual DateTime? Endtime { get; set; }

        /// <summary>
        /// Code 二次分类后主缺陷代码
        /// </summary>
        [Property(Name = "Code", Type = PropertyType.字符, MaxLength = 50)]
        public virtual string Code { get; set; }

        /// <summary>
        /// Grade	二次分类后主缺陷等级
        /// </summary>
        [Property(Name = "Grade", Type = PropertyType.字符, MaxLength = 10)]
        public virtual string Grade { get; set; }

        [Property(Name = "Img_mark", Type = PropertyType.字符, MaxLength = 10)]
        public virtual string Img_mark { get; set; }

        /// <summary>
        /// 预留：缺陷标记图路径
        /// </summary>
        [Property(Name = "Path", Type = PropertyType.字符, MaxLength = 300)]
        public virtual string Path { get; set; }

        [Property(Name = "Ext", Type = PropertyType.字符, MaxLength = 1000)]
        public virtual string Ext { get; set; }
        /// <summary>
        /// 物理长度
        /// </summary>
        [Property(Name = "ShapeX_mm", Type = PropertyType.单精度浮点数)]
        public virtual float ShapeX_mm { get; set; }
        /// <summary>
        /// 物理宽度
        /// </summary>
        [Property(Name = "ShapeY_mm", Type = PropertyType.单精度浮点数)]
        public virtual float ShapeY_mm { get; set; }
        /// <summary>
        /// 物理高度
        /// </summary>
        [Property(Name = "ShapeZ_mm", Type = PropertyType.单精度浮点数)]
        public virtual float ShapeZ_mm { get; set; }
        public virtual string GetTimePropertyName
        {
            get
            {
                return "Starttime";
            }
            set { }
        }

        public virtual string GetTableName
        {
            get
            {
                return "inspectionresult";
            }
            set { }
        }
    }
}
