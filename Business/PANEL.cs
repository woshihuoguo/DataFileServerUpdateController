using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Xml;
using BusinessTest;

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
        public RECIPE RECIPE { get; set; }
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

        [XmlAttribute] public double G_AREAOVERTHRES { get; set; }
        [XmlAttribute] public double G_AVGGRAYLEVELH { get; set; }
        [XmlAttribute] public double G_AVGGRAYLEVELH_ORG { get; set; }
        [XmlAttribute] public double G_AVGGRAYLEVELL { get; set; }
        [XmlAttribute] public double G_AVGGRAYLEVELL_ORG { get; set; }
        [XmlAttribute] public double G_DEFECTSIZE { get; set; }
        [XmlAttribute] public double G_DEFECTTYPE { get; set; }
        [XmlAttribute] public double G_GRAYMAX { get; set; }
        [XmlAttribute] public double G_GRAYMAX_ORG { get; set; }
        [XmlAttribute] public double G_GRAYMIN { get; set; }
        [XmlAttribute] public double G_GRAYMIN_ORG { get; set; }
        [XmlAttribute] public int G_PTNTYPE { get; set; }
        [XmlAttribute] public int G_SHOTNO { get; set; }
        [XmlAttribute] public int G_VPNO { get; set; }
        [XmlAttribute] public int G_ZONENO { get; set; }
        [XmlAttribute] public int G_CAMNO { get; set; }
        [XmlAttribute] public int G_CELLNO { get; set; }
        [XmlAttribute] public int G_DEFECTID { get; set; }
        [XmlAttribute] public int G_DFTEX { get; set; }
        [XmlAttribute] public int G_DFTEY { get; set; }
        [XmlAttribute] public int G_DFTSX { get; set; }
        [XmlAttribute] public int G_DFTSY { get; set; }
        [XmlAttribute] public int G_GRIDNUM { get; set; }
        [XmlAttribute] public double G_HEIGHT { get; set; }
        [XmlAttribute] public int G_OPTICTYPE { get; set; }
        [XmlAttribute] public int G_POSX { get; set; }
        [XmlAttribute] public int G_POSY { get; set; }
        [XmlAttribute] public int G_PTNNO { get; set; }
        [XmlAttribute] public double G_REALPANELX { get; set; }
        [XmlAttribute] public double G_REALPANELY { get; set; }
        [XmlAttribute] public double G_REALX { get; set; }
        [XmlAttribute] public double G_REALY { get; set; }
        [XmlAttribute] public double G_WIDTH { get; set; }

        // 第一次 GRAYLEVELL_RATE
        [XmlAttribute] public int GRAYLEVELL_RATE { get; set; }

        [XmlAttribute] public double L_LDPEAKDIFFERENCE { get; set; }
        [XmlAttribute] public double L_LDWIDTH { get; set; }

        [XmlAttribute] public double M_COMPRESSION { get; set; }
        [XmlAttribute] public double M_DEFOCUS_INDEX { get; set; }
        [XmlAttribute] public double M_INTENSITY { get; set; }
        [XmlAttribute] public double M_LEVEL_DATA { get; set; }
        [XmlAttribute] public double M_N1 { get; set; }
        [XmlAttribute] public double M_N2 { get; set; }
        [XmlAttribute] public double M_N3 { get; set; }
        [XmlAttribute] public double M_N3_NEW { get; set; }
        [XmlAttribute] public double M_N4 { get; set; }
        [XmlAttribute] public double M_N5 { get; set; }
        [XmlAttribute] public double M_NLD_AREA { get; set; }
        [XmlAttribute] public double M_NLD_AVG { get; set; }
        [XmlAttribute] public double M_NLD_AVG_DIFF { get; set; }
        [XmlAttribute] public double M_NLD_DIFF { get; set; }
        [XmlAttribute] public double M_NLD_REF { get; set; }
        [XmlAttribute] public double M_NLD_SHOT2_AVG_DIFF { get; set; }
        [XmlAttribute] public double M_NLD_SHOT2_NLD_VAR_DST { get; set; }
        [XmlAttribute] public double M_NLD_SHOT2_VAR { get; set; }
        [XmlAttribute] public double M_NLD_TAR { get; set; }
        [XmlAttribute] public double M_NLD_VAR { get; set; }
        [XmlAttribute] public int M_ROI_TYPE { get; set; }
        [XmlAttribute] public double M_BVALUEH { get; set; }
        [XmlAttribute] public double M_BVALUEL { get; set; }
        [XmlAttribute] public double M_BOX40_AVGGRAYLEVEL { get; set; }
        [XmlAttribute] public double M_BOX40_MAXVAL { get; set; }
        [XmlAttribute] public double M_BOX40_MINVAL { get; set; }
        [XmlAttribute] public double M_BOX40_STDDEVIATION { get; set; }
        [XmlAttribute] public double M_GVALUEH { get; set; }
        [XmlAttribute] public double M_GVALUEL { get; set; }
        [XmlAttribute] public double M_LVALUEH { get; set; }
        [XmlAttribute] public double M_LVALUEL { get; set; }
        [XmlAttribute] public double M_POCB_DEFECT { get; set; }
        [XmlAttribute] public double M_RVALUEH { get; set; }
        [XmlAttribute] public double M_RVALUEL { get; set; }
        [XmlAttribute] public double M_UVALUEH { get; set; }
        [XmlAttribute] public double M_UVALUEL { get; set; }
        [XmlAttribute] public double M_VVALUEH { get; set; }
        [XmlAttribute] public double M_VVALUEL { get; set; }

        [XmlAttribute] public double P_AVGGRAYLEVELH_148 { get; set; }
        [XmlAttribute] public double P_AVGGRAYLEVELH_B { get; set; }
        [XmlAttribute] public double P_AVGGRAYLEVELH_G { get; set; }
        [XmlAttribute] public double P_AVGGRAYLEVELH_R { get; set; }
        [XmlAttribute] public double P_AVGGRAYLEVELL_108 { get; set; }
        [XmlAttribute] public double P_AVGGRAYLEVELL_B { get; set; }
        [XmlAttribute] public double P_AVGGRAYLEVELL_G { get; set; }
        [XmlAttribute] public double P_AVGGRAYLEVELL_PRC { get; set; }
        [XmlAttribute] public double P_AVGGRAYLEVELL_R { get; set; }
        [XmlAttribute] public double P_BRIGHT_M_LINK_CNT { get; set; }
        [XmlAttribute] public double P_DARK_M_LINK_RGB { get; set; }
        [XmlAttribute] public double P_DARK_M_LINK_CNT { get; set; }
        [XmlAttribute] public double P_NEAR_PIXEL_VAL { get; set; }
        [XmlAttribute] public double P_OMIT_AVG_ORG { get; set; }
        [XmlAttribute] public double P_OMIT_AVG_PRC { get; set; }
        [XmlAttribute] public double P_OMIT_MAX_ORG { get; set; }
        [XmlAttribute] public double P_OMIT_MAX_PRC { get; set; }
        [XmlAttribute] public double P_PIXELINFO { get; set; }
        [XmlAttribute] public double P_PIXEL_PITCH { get; set; }
        [XmlAttribute] public double P_PIXEL_SIZE_X { get; set; }
        [XmlAttribute] public double P_PIXEL_SIZE_Y { get; set; }
        [XmlAttribute] public double P_SHARE_AVGGRAYLEVELL_ORG_B { get; set; }
        [XmlAttribute] public double P_SHARE_AVGGRAYLEVELL_ORG_G { get; set; }
        [XmlAttribute] public double P_SHARE_AVGGRAYLEVELL_ORG_R { get; set; }
        [XmlAttribute] public double P_SHARE_DARK_M_LINK_CNT_B { get; set; }
        [XmlAttribute] public double P_SHARE_DARK_M_LINK_CNT_G { get; set; }
        [XmlAttribute] public double P_SHARE_DARK_M_LINK_CNT_R { get; set; }
        [XmlAttribute] public double P_SHARE_NEAR_PIXEL_VAL_B { get; set; }
        [XmlAttribute] public double P_SHARE_NEAR_PIXEL_VAL_G { get; set; }
        [XmlAttribute] public double P_SHARE_NEAR_PIXEL_VAL_R { get; set; }
        [XmlAttribute] public double P_SHARE_POSX_B { get; set; }
        [XmlAttribute] public double P_SHARE_POSX_G { get; set; }
        [XmlAttribute] public double P_SHARE_POSX_R { get; set; }
        [XmlAttribute] public double P_SHARE_POSY_B { get; set; }
        [XmlAttribute] public double P_SHARE_POSY_G { get; set; }
        [XmlAttribute] public double P_SHARE_POSY_R { get; set; }
        [XmlAttribute] public double P_HIGH_AREA_GRAYLEVEL_COUNT { get; set; }
        [XmlAttribute] public double P_LOW_AREA_GRAYLEVEL_COUNT { get; set; }

        [XmlAttribute] public double Q_DISPLAY_ROI { get; set; }
        [XmlAttribute] public double Q_ZONE1_MIN { get; set; }
        [XmlAttribute] public double Q_ZONE2_MIN { get; set; }
        [XmlAttribute] public double Q_ZONE3_MIN { get; set; }

        [XmlAttribute] public double T_ASYNC_1 { get; set; }
        [XmlAttribute] public double T_ASYNC_10 { get; set; }
        [XmlAttribute] public double T_ASYNC_2 { get; set; }
        [XmlAttribute] public double T_ASYNC_3 { get; set; }
        [XmlAttribute] public double T_ASYNC_4 { get; set; }
        [XmlAttribute] public double T_ASYNC_5 { get; set; }
        [XmlAttribute] public double T_ASYNC_6 { get; set; }
        [XmlAttribute] public double T_ASYNC_7 { get; set; }
        [XmlAttribute] public double T_ASYNC_8 { get; set; }
        [XmlAttribute] public double T_ASYNC_9 { get; set; }
        [XmlAttribute] public double T_ASYNC_INDEX { get; set; }
        [XmlAttribute] public double T_ASYNC_STD1 { get; set; }
        [XmlAttribute] public double T_ASYNC_STD10 { get; set; }
        [XmlAttribute] public double T_ASYNC_STD2 { get; set; }
        [XmlAttribute] public double T_ASYNC_STD3 { get; set; }
        [XmlAttribute] public double T_ASYNC_STD4 { get; set; }
        [XmlAttribute] public double T_ASYNC_STD5 { get; set; }
        [XmlAttribute] public double T_ASYNC_STD6 { get; set; }
        [XmlAttribute] public double T_ASYNC_STD7 { get; set; }
        [XmlAttribute] public double T_ASYNC_STD8 { get; set; }
        [XmlAttribute] public double T_ASYNC_STD9 { get; set; }

        [XmlAttribute] public double U1_OMIT_AREA_PRC { get; set; }
        [XmlAttribute] public double U2_OMIT_AREA_PRC { get; set; }
        [XmlAttribute] public double U3_ASYNC_INDEX2 { get; set; }
        [XmlAttribute] public double U4_OMIT_WID { get; set; }
        [XmlAttribute] public double U5_OMIT_HGT { get; set; }
        [XmlAttribute] public double U_ANOTHER_OMIT_AREA { get; set; }
        [XmlAttribute] public double U_ANOTHER_OMIT_AVG_PRC { get; set; }
        [XmlAttribute] public double U_ANOTHER_OMIT_HGT { get; set; }
        [XmlAttribute] public double U_ANOTHER_OMIT_MAX_PRC { get; set; }
        [XmlAttribute] public double U_ANOTHER_OMIT_MIN_PRC { get; set; }
        [XmlAttribute] public double U_ANOTHER_OMIT_WID { get; set; }
        [XmlAttribute] public double U_OMIT_AREA { get; set; }
        [XmlAttribute] public double U_OMIT_HGT { get; set; }
        [XmlAttribute] public double U_OMIT_WID { get; set; }

        [XmlAttribute] public double X001_INSPTYPE { get; set; }
        [XmlAttribute] public double X002_OMIT_BLOB_AREA { get; set; }
        [XmlAttribute] public double X003_OMIT_BLOB_LX { get; set; }
        [XmlAttribute] public double X004_OMIT_BLOB_LY { get; set; }
        [XmlAttribute] public double X005_OMIT_BLOB_BOX { get; set; }
        [XmlAttribute] public double X008_NLD_LEFTNOTCHDIFF { get; set; }
        [XmlAttribute] public double X009_NLD_RIGHTNOTCHDIFF { get; set; }
        [XmlAttribute] public double X065_LABAREA { get; set; }
        [XmlAttribute] public double X066_LDGRAYMAX { get; set; }
        [XmlAttribute] public double X067_LDGRAYMIN { get; set; }
        [XmlAttribute] public double X106_FMM_STRENGTH { get; set; }
        [XmlAttribute] public double X107_FMM_AVGR { get; set; }
        [XmlAttribute] public double X108_FMM_AVGG { get; set; }
        [XmlAttribute] public double X109_FMM_AVGB { get; set; }
        [XmlAttribute] public double X110_FMM_L { get; set; }
        [XmlAttribute] public double X111_FMM_U { get; set; }
        [XmlAttribute] public double X112_FMM_V { get; set; }
        [XmlAttribute] public double X113_FMM_BASEL { get; set; }
        [XmlAttribute] public double X114_FMM_BASEU { get; set; }
        [XmlAttribute] public double X115_FMM_BASEV { get; set; }
        [XmlAttribute] public double X116_FMM_DEG { get; set; }
        [XmlAttribute] public double X117_FMM_SIZE { get; set; }
        [XmlAttribute] public double X118_FMM_HEI { get; set; }
        [XmlAttribute] public double X119_FMM_WID { get; set; }
        [XmlAttribute] public double X120_FMM_U_DIFF { get; set; }
        [XmlAttribute] public double X121_FMM_V_DIFF { get; set; }
        [XmlAttribute] public double X122_FMM_U_DIFF_GR { get; set; }
        [XmlAttribute] public double X123_FMM_U_DEV { get; set; }
        [XmlAttribute] public double X124_FMM_V_DEV { get; set; }
        [XmlAttribute] public double X125_FMM_U_DEV_GR { get; set; }
        [XmlAttribute] public double X126_FMM_STRENGTH { get; set; }
        [XmlAttribute] public double X127_FMM_U_DUV { get; set; }
        [XmlAttribute] public double X128_FMM_V_DUV { get; set; }
        [XmlAttribute] public double X129_FMM_U_COLORANGLE { get; set; }
        [XmlAttribute] public double X130_FMM_V_COLORANGLE { get; set; }
        [XmlAttribute] public double X137_CHANENL { get; set; }
        [XmlAttribute] public double X138_REDPRC { get; set; }
        [XmlAttribute] public double X139_GREENPRC { get; set; }
        [XmlAttribute] public double X140_BLUEPRC { get; set; }
        [XmlAttribute] public double X141_COLORDEG { get; set; }
        [XmlAttribute] public double X142_REDDIFF { get; set; }
        [XmlAttribute] public double X143_GREENDIFF { get; set; }
        [XmlAttribute] public double X144_BLUEDIFF { get; set; }
        [XmlAttribute] public double X145_FMM_T_REMAIN { get; set; }
        [XmlAttribute] public double X146_FMM_T_INTERSECTION { get; set; }
        [XmlAttribute] public double X147_FMM_MAXDIFFUV { get; set; }
        [XmlAttribute] public double X158_IQ_PROB1 { get; set; }
        [XmlAttribute] public double X159_IQ_PROB2 { get; set; }
        [XmlAttribute] public double X160_IQ_FLAG { get; set; }
        [XmlAttribute] public double X161_FMM_LINE_PROJ_MAX { get; set; }
        [XmlAttribute] public double X162_FMM_LINE_PROJ_MEAN { get; set; }
        [XmlAttribute] public double X163_FMM_STRENGTH_MAX10 { get; set; }
        [XmlAttribute] public double X164_FMM_STRENGTH_MIN10 { get; set; }
        [XmlAttribute] public double X165_FMM_U_DIFF_MAX { get; set; }
        [XmlAttribute] public double X166_FMM_U_DIFF_MIN { get; set; }
        [XmlAttribute] public double X167_FMM_V_DIFF_MAX { get; set; }
        [XmlAttribute] public double X168_FMM_V_DIFF_MIN { get; set; }
        [XmlAttribute] public double X200_SIYA_HORI_LD_GRIDMIN_CENTER { get; set; }
        [XmlAttribute] public double X201_SIYA_HORI_LD_GRIDMIN_DFT { get; set; }
        [XmlAttribute] public double X202_INSP_PROB_C1 { get; set; }
        [XmlAttribute] public double X203_INSP_PROB_C2 { get; set; }
        [XmlAttribute] public double X204_INSP_PROB_C3 { get; set; }
        [XmlAttribute] public double X205_INSP_PROB_C4 { get; set; }
        [XmlAttribute] public double X206_INSP_PROB_C5 { get; set; }
        [XmlAttribute] public double X207_INSP_PROB_C6 { get; set; }
        [XmlAttribute] public double X208_EDGE_GROUPING { get; set; }

        // 第二次 GRAYLEVELL_RATE（动态输出，不定义重复属性）
        [XmlAnyAttribute]
        public System.Xml.XmlAttribute[] ExtraAttrs { get; set; }

        [XmlAttribute("IMAGE_FILE_NO")] public int IMAGE_FILE_NO { get; set; }

        [XmlElement("IMG")] public List<IMG> IMG { get; set; }
    }

    public class IMG
    {
        [XmlAttribute("SEQ")] public int SEQ { get; set; }
        [XmlAttribute("NAME")] public string NAME { get; set; }
    }
    #endregion
}
