using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Xml;

namespace Business
{
    #region XML序列化实体（客户标准 NG/G 格式）
    public class PANEL
    {
        public HEADER HEADER { get; set; }
        public BODY BODY { get; set; }
        public DEFECT_INFO DEFECT_INFO { get; set; }
    }

    public class HEADER
    {
        public string KEY_ID { get; set; }
        public string PANEL_ID { get; set; }
        public string SERIAL_NO { get; set; }
        public string TOTAL_INPUT { get; set; }
        public string LINE_ID { get; set; }
        public string MACHINE_ID { get; set; }
        public string UNIT_ID { get; set; }
        public string OPER_ID { get; set; }
        public string PROC_ID { get; set; }
        public string RECIPE_ID { get; set; }
        public string JUDGE { get; set; }
        public INSP_TIME INSP_TIME { get; set; }
    }

    public class INSP_TIME
    {
        [XmlAttribute] public string START { get; set; }
        [XmlAttribute] public string END { get; set; }
    }

    public class BODY
    {
        public AP_INFO AP_INFO { get; set; }
        public JUDGE_INFO JUDGE_INFO { get; set; }
        public DEFECT_NO DEFECT_NO { get; set; }
    }

    public class AP_INFO
    {
        public PROC PROC { get; set; }
        public MACHINE MACHINE { get; set; }
        public TIME TIME { get; set; }
        public OPERATOR OPERATOR { get; set; }
        public JUDGE JUDGE { get; set; }
        public REASON REASON { get; set; }
        public WORKTABLE WORKTABLE { get; set; }
        public TYPE TYPE { get; set; }
    }

    public class PROC { [XmlAttribute("FA")] public string FA { get; set; } }
    public class MACHINE { [XmlAttribute("FA")] public string FA { get; set; } }
    public class TIME { [XmlAttribute("FA")] public string FA { get; set; } }
    public class OPERATOR { [XmlAttribute("FA")] public string FA { get; set; } }
    public class JUDGE { [XmlAttribute("FA")] public string FA { get; set; } }
    public class WORKTABLE { [XmlAttribute("FA")] public string FA { get; set; } }
    public class TYPE { }

    public class REASON
    {
        [XmlAttribute("FA_FINAL")]
        public string FA_FINAL { get; set; } = null;

        // 动态扩展属性存储容器
        // 加这一行 → 支持无限动态属性
        [XmlAnyAttribute]
        public XmlAttribute[] DynamicAttributes { get; set; }
    }

    public class JUDGE_INFO
    {
        [XmlElement("LATEST")]
        public LATEST LATEST { get; set; } = new LATEST();
    }

    public class LATEST
    {
        [XmlAttribute("JUDGE")]
        public string JUDGE { get; set; }
        [XmlAttribute("REASON")]
        public string REASON { get; set; }
    }

    [XmlRoot("RECIPE")]
    public class RECIPE
    {
        [XmlElement("CLASSIFY")]
        public CLASSIFY CLASSIFY { get; set; }

        [XmlElement("RECIPE")]
        public RECIPE_NODE RECIPE_NODE { get; set; }
    }

    public class CLASSIFY
    {
        [XmlAttribute("FA")]
        public string FA { get; set; } = "45550";
    }

    public class RECIPE_NODE
    {
        [XmlAttribute("FA")]
        public string FA { get; set; } = "HNAMAL55ZK08";

        [XmlAttribute("FA_GIB")]
        public string FA_GIB { get; set; } = "";
    }

    public class DEFECT_NO
    {
        [XmlAttribute("TOTAL")] public int TOTAL { get; set; }
    }

    public class DEFECT_INFO
    {
        [XmlElement("DEFECT")] public List<DEFECT> DEFECT { get; set; }

        [XmlElement("PatXMLInfo")] public string PatXMLInfo { get; set; }
    }


    public class DEFECT
    {
        [XmlAttribute("SHOP")] public string SHOP { get; set; }
        [XmlAttribute("DEF_NO")] public int DEF_NO { get; set; }
        [XmlAttribute("PNL_START_TIME")] public string PNL_START_TIME { get; set; }
        [XmlAttribute("PNL_END_TIME")] public string PNL_END_TIME { get; set; }
        [XmlAttribute("PROC_ID")] public string PROC_ID { get; set; }
        [XmlAttribute("MACHINE_ID")] public string MACHINE_ID { get; set; }
        [XmlAttribute("JUDGE")] public string JUDGE { get; set; }
        [XmlAttribute("REASON")] public string REASON { get; set; }
        [XmlAttribute("CLASSIFY")] public string CLASSIFY { get; set; }
        [XmlAttribute("IMAGE_FILE_NO")] public int IMAGE_FILE_NO { get; set; }
        [XmlElement("IMG")] public List<IMG> IMG { get; set; }

        #region starting with G
        [XmlAttribute] public double GIB_judge { get; set; }
        [XmlAttribute] public double G001_RealX { get; set; }
        [XmlAttribute] public double G002_RealY { get; set; }
        [XmlAttribute] public double G003_GridPos { get; set; }
        [XmlAttribute] public double G004_PxlX { get; set; }
        [XmlAttribute] public double G005_PxlY { get; set; }
        [XmlAttribute] public double G006_DefectID { get; set; }
        [XmlAttribute] public double G007_LightID { get; set; }
        [XmlAttribute] public double G008_RoiID { get; set; }
        [XmlAttribute] public double G009_VpID { get; set; }
        [XmlAttribute] public double G010_ScanID { get; set; }
        [XmlAttribute] public int G011_TotalDetection { get; set; }
        [XmlAttribute] public int G012_RealWidth { get; set; }
        [XmlAttribute] public int G013_RealHeight { get; set; }
        [XmlAttribute] public int G014_RealLength { get; set; }
        [XmlAttribute] public int G015_RealArea { get; set; }
        [XmlAttribute] public int G016_PxlWidth { get; set; }
        [XmlAttribute] public int G017_PxlHeight { get; set; }
        [XmlAttribute] public int G018_PxlLength { get; set; }
        [XmlAttribute] public int G019_PxlArea { get; set; }
        [XmlAttribute] public int G020_FillRate { get; set; }
        [XmlAttribute] public int G021_Circularity { get; set; }
        [XmlAttribute] public int G022_SeqID { get; set; }
        [XmlAttribute] public double G024_CenterRate { get; set; }
        [XmlAttribute] public int G025_DefocusIdx { get; set; }
        [XmlAttribute] public int G026_PrjDensity { get; set; }
        [XmlAttribute] public int G027_StdAngle { get; set; }
        [XmlAttribute] public int G028_RealAreaH { get; set; }
        [XmlAttribute] public double G029_RealAreaL { get; set; }
        [XmlAttribute] public double G030_PxlAreaH { get; set; }
        [XmlAttribute] public double G031_PxlAreaL { get; set; }
        [XmlAttribute] public double G032_NumContours { get; set; }
        [XmlAttribute] public double G033_PrcGrayAvg { get; set; }
        [XmlAttribute] public int G034_PrcGrayAvgH { get; set; }
        [XmlAttribute] public double G035_PrcGrayAvgL { get; set; }
        [XmlAttribute] public double G036_PrcGrayMax { get; set; }
        [XmlAttribute] public double G037_PrcGrayMin { get; set; }
        [XmlAttribute] public double G038_PrcGrayStdDev { get; set; }
        [XmlAttribute] public double G039_RippleScore { get; set; }
        [XmlAttribute] public double G040_Kurtosis { get; set; }
        [XmlAttribute] public double G041_Contrast { get; set; }
        [XmlAttribute] public double G042_ContrastRatio { get; set; }
        [XmlAttribute] public double G043_PCA_Axis1 { get; set; }
        [XmlAttribute] public double G044_PCA_Axis2 { get; set; }
        [XmlAttribute] public double G045_PCA_Angle { get; set; }
        [XmlAttribute] public double G047_RoiArea { get; set; }
        [XmlAttribute] public double G048_Rsquare { get; set; }
        [XmlAttribute] public double G049_Rsquare2 { get; set; }
        [XmlAttribute] public double G050_Entropy { get; set; }
        [XmlAttribute] public double G051_InspType { get; set; }
        [XmlAttribute] public double G052_Correlation { get; set; }
        [XmlAttribute] public double G053_HistMaximaStdDev { get; set; }
        [XmlAttribute] public double G054_Dist_DefSx_GlassSx { get; set; }
        [XmlAttribute] public double G055_Dist_DefSy_GlassSy { get; set; }
        [XmlAttribute] public double G056_Dist_DefEx_GlassEx { get; set; }
        [XmlAttribute] public double G057_Dist_DefEy_GlassEy { get; set; }
        [XmlAttribute] public int G058_ParticleLayer { get; set; }
        [XmlAttribute] public double G059_ShadowDist { get; set; }
        [XmlAttribute] public double G060_OrgGrayAvg { get; set; }
        [XmlAttribute] public double G061_OrgGrayMax { get; set; }
        [XmlAttribute] public double G062_OrgGrayMin { get; set; }
        [XmlAttribute] public double G063_OrgGrayStdDev { get; set; }
        [XmlAttribute] public double G064_Anomaly_MaxScore { get; set; }
        [XmlAttribute] public double G066_OrgAvg_R { get; set; }
        [XmlAttribute] public double G067_OrgMax_R { get; set; }
        [XmlAttribute] public double G068_OrgMin_R { get; set; }
        [XmlAttribute] public double G069_OrgAvg_G { get; set; }
        [XmlAttribute] public double G070_OrgMax_G { get; set; }
        [XmlAttribute] public double G071_OrgMin_G { get; set; }
        [XmlAttribute] public double G072_OrgAvg_B { get; set; }
        [XmlAttribute] public double G073_OrgMax_B { get; set; }
        [XmlAttribute] public double G074_OrgMin_B { get; set; }
        [XmlAttribute] public double G075_AutoThreArea { get; set; }
        #endregion

        #region starting with L, layers L01-L08
        [XmlAttribute] public double L0101_Detection { get; set; }
        [XmlAttribute] public double L0102_AreaL { get; set; }
        [XmlAttribute] public double L0103_AreaH { get; set; }
        [XmlAttribute] public double L0104_FillRate { get; set; }
        [XmlAttribute] public double L0105_Prc_GrayAvgL { get; set; }
        [XmlAttribute] public double L0106_Prc_GrayAvgH { get; set; }
        [XmlAttribute] public double L0107_Prc_GrayMin { get; set; }
        [XmlAttribute] public double L0108_Prc_GrayMax { get; set; }
        [XmlAttribute] public double L0109_Similarity { get; set; }
        [XmlAttribute] public double L0110_AreaL_limit { get; set; }
        [XmlAttribute] public double L0111_AreaH_limit { get; set; }
        [XmlAttribute] public double L0112_Org_GrayAvgL { get; set; }
        [XmlAttribute] public double L0113_Org_GrayAvgH { get; set; }
        [XmlAttribute] public double L0114_Org_GrayMin { get; set; }
        [XmlAttribute] public double L0115_Org_GrayMax { get; set; }
        [XmlAttribute] public double L0116_Width { get; set; }
        [XmlAttribute] public double L0117_Height { get; set; }
        [XmlAttribute] public double L0118_Length { get; set; }
        [XmlAttribute] public double L0119_SimilarityBrt { get; set; }
        [XmlAttribute] public double L0120_SimilarityArea { get; set; }
        [XmlAttribute] public double L0121_Layer { get; set; }
        #endregion

        #region Model and version information
        [XmlAttribute] public double MLClass01 { get; set; }
        [XmlAttribute] public double MLClass02 { get; set; }
        [XmlAttribute] public double MLClass03 { get; set; }
        [XmlAttribute] public double MLClass04 { get; set; }
        [XmlAttribute] public double MLClass05 { get; set; }
        [XmlAttribute] public double MLClass06 { get; set; }
        [XmlAttribute] public double MLClass07 { get; set; }
        [XmlAttribute] public double MLClass08 { get; set; }
        [XmlAttribute] public double MLClass09 { get; set; }
        [XmlAttribute] public double MLClass10 { get; set; }
        [XmlAttribute] public double V001_AlgoVersion { get; set; }
        [XmlAttribute] public double V002_ClsfVersion { get; set; }
        [XmlAttribute] public double V003_VP_IP { get; set; }
        #endregion


        [XmlAttribute] public double G_DFTSX { get; set; }
        [XmlAttribute] public double G_DFTSY { get; set; }
        [XmlAttribute] public double G_DFTEX { get; set; }
        [XmlAttribute] public double G_DFTEY { get; set; }
        [XmlAttribute] public double G_REALX { get; set; }
        [XmlAttribute] public double G_REALY { get; set; }
        [XmlAttribute] public double M_BOX40_AVGGRAYLEVEL { get; set; }
        [XmlAttribute] public double P_DARK_M_LINK_CNT { get; set; }
        [XmlAttribute] public double P_BRIGHT_M_LINK_CNT { get; set; }
        

        // 第二次 GRAYLEVELL_RATE（动态输出，不定义重复属性）
        [XmlAnyAttribute]
        public System.Xml.XmlAttribute[] ExtraAttrs { get; set; }
    }

    public class IMG
    {
        [XmlAttribute("SEQ")] public int SEQ { get; set; }
        [XmlAttribute("NAME")] public string NAME { get; set; }
    }
    #endregion
}
