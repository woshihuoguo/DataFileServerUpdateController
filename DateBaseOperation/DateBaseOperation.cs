using LT.Common.Logger;
using LT.SuperTranCockpit.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DateBaseOperation
{
    public static class DateBaseOperation
    {
        public static bool GetLastProcessResultEntity(long sysId, out ProcessResultEntity entity, out string reason)
        {
            reason = string.Empty;
            entity = null;
            try
            {
                var entities = Frame.Frame.Instance.Storage.CreateQuery<ProcessResultEntity>()
                    .EqualTo("SysId", sysId.ToString()).GetRange(0, 1).ToList();
                if (entities != null && entities.Count > 0)
                {
                    entity = entities[0];
                    return true;
                }
                else
                {
                    reason = "未找到数据";
                    return false;
                }
            }
            catch (Exception ex)
            {
                reason = "查询数据发生异常," + ex.Message;
                Logger.Log("Error", "执行 GetProcessResultEntityFromUniqueId 发生异常," + ex.Message + $"", ex);
                return true;
            }
        }
        public static bool GetLastInspectSummaryEntity(long sysId, out InspectSummaryEntity entity, out string reason)
        {
            reason = string.Empty;
            entity = null;
            try
            {
                var entities = Frame.Frame.Instance.Storage.CreateQuery<InspectSummaryEntity>()
                    .GreaterThan("SysId", sysId.ToString()).GetRange(0, 1).ToList();
                if (entities != null && entities.Count > 0)
                {
                    entity = entities[0];
                    return true;
                }
                else
                {
                    reason = "未找到数据";
                    return false;
                }
            }
            catch (Exception ex)
            {
                reason = "查询数据发生异常," + ex.Message;
                Logger.Log("Error", "执行GetLastAoiResultEntity发生异常," + ex.Message, ex);
                return true;
            }
        }
        public static bool GetLastAoiResultEntity(long sysId, out ICW_LCD_InspectionResultEntity entity, out string reason)
        {
            reason = string.Empty;
            entity = null;
            try
            {
                var entities = Frame.Frame.Instance.Storage.CreateQuery<ICW_LCD_InspectionResultEntity>()
                    .GreaterThan("SysID", sysId.ToString()).GetRange(0, 1).ToList();
                if (entities != null && entities.Count > 0)
                {
                    entity = entities[0];
                    return true;
                }
                else
                {
                    reason = "未找到数据";
                    return false;
                }
            }
            catch (Exception ex)
            {
                reason = "查询数据发生异常," + ex.Message;
                Logger.Log("Error", "执行GetLastAoiResultEntity发生异常," + ex.Message, ex);
                return true;
            }
        }

        public static bool GetPanelInfoByUniqueId(string uniqueId,out List<PanelInfoEntity> entities, out string reason)
        {
            reason = string.Empty;
            entities = null;
            try
            {
                entities = Frame.Frame.Instance.Storage.CreateQuery<PanelInfoEntity>().EqualTo("UniqueId", uniqueId).ToList();
                if (entities != null && entities.Count > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                reason = "查询数据发生异常," + ex.Message;
                Logger.Log("Error", "执行GetAoiResultEntityFromUniqueId发生异常," + ex.Message, ex);
                return true;
            }
        }

        public static bool GetAoiResultEntityFromUniqueId(string uniqueId, out ICW_LCD_InspectionResultEntity entity, out string reason)
        {
            reason = string.Empty;
            entity = null;
            try
            {
                var entities = Frame.Frame.Instance.Storage.CreateQuery<ICW_LCD_InspectionResultEntity>()
                    .EqualTo("UniqueID", uniqueId).ToList();
                if (entities != null && entities.Count > 0)
                {
                    entity = entities.Last();
                    return true;
                }
                else
                {
                    reason = "未找到数据";
                    return false;
                }
            }
            catch (Exception ex)
            {
                reason = "查询数据发生异常," + ex.Message;
                Logger.Log("Error", "执行GetAoiResultEntityFromUniqueId发生异常," + ex.Message, ex);
                return true;
            }
        }

        /*public static bool GetSurfaceInspectionResultImageEntityFromUniqueId(string uniqueId, out List<InspectionResultImage> entities,
            out string reason)
        {
            reason = string.Empty;
            entities = new List<InspectionResultImage>();
            try
            {
                entities = Frame.Frame.Instance.Storage.CreateQuery<InspectionResultImage>()
                    .EqualTo("UniqueID", uniqueId).ToList();
                if (entities != null && entities.Count > 0)
                {
                    return true;
                }
                else
                {
                    reason = "未找到数据";
                    return false;
                }
            }
            catch (Exception ex)
            {
                reason = "查询数据发生异常," + ex.Message;
                Logger.Log("Error", "执行GetSurfaceInspectionResultImageEntityFromUniqueId发生异常," + ex.Message, ex);
                return true;
            }
        }*/

        public static bool GetAoiDefectEntityFromUniqueId(string guid, out List<ICW_LCD_AoiDefectEntity> entities,
            out string reason)
        {
            reason = string.Empty;
            entities = new List<ICW_LCD_AoiDefectEntity>();
            try
            {
                //entities = Frame.Frame.Instance.Storage.CreateQuery<ICW_LCD_AoiDefectEntity>().EqualTo("GUID_IVS_LCD_InspectionResult", guid).ToList();
                entities = Frame.Frame.Instance.Storage.CreateQuery<ICW_LCD_AoiDefectEntity>()
                    .EqualTo("UniqueID", guid).ToList();
                if (entities != null && entities.Count > 0)
                {
                    return true;
                }
                else
                {
                    reason = "未找到数据";
                    return false;
                }
            }
            catch (Exception ex)
            {
                reason = "查询数据发生异常," + ex.Message;
                Logger.Log("Error", "执行GetAoiDefectEntityFromUniqueId发生异常," + ex.Message, ex);
                return true;
            }
        }
        public static bool GetInspectionResultEntityFromUniqueId(string uniqueId, out ICW_LCD_InspectionResultEntity entity, out string reason)
        {
            reason = string.Empty;
            entity = null;
            try
            {
                var entities = Frame.Frame.Instance.Storage.CreateQuery<ICW_LCD_InspectionResultEntity>()
                    .EqualTo("UniqueID", uniqueId).ToList();
                if (entities != null && entities.Count > 0)
                {
                    entity = entities.Last();
                    return true;
                }
                else
                {
                    reason = "未找到数据";
                    return false;
                }
            }
            catch (Exception ex)
            {
                reason = "查询数据发生异常," + ex.Message;
                Logger.Log("Error", "执行GetInspectionResultEntityFromUniqueId发生异常," + ex.Message + $"uniqueId:{uniqueId}", ex);
                return true;
            }
        }

        public static bool GetProcessResultEntityFromUniqueId(string UniqueID, out List<ProcessResultEntity> entities, out string reason)
        {
            reason = string.Empty;
            entities = new List<ProcessResultEntity>();
            try
            {
                entities = Frame.Frame.Instance.Storage.CreateQuery<ProcessResultEntity>()
                    .EqualTo("UniqueId", UniqueID).ToList();

                if (entities != null && entities.Count > 0)
                {
                    return true;
                }
                else
                {
                    reason = "未找到数据";
                    return false;
                }
            }
            catch (Exception ex)
            {
                reason = "查询数据发生异常," + ex.Message;
                Logger.Log("Error", "执行 GetProcessResultEntityFromUniqueId 发生异常," + ex.Message + $"", ex);
                return true;
            }
        }
        
    }
}
