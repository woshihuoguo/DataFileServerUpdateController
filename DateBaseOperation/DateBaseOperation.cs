using LT.Common.Logger;
using LT.SuperTranCockpit.Entity;
using Frame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

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
                     .GreaterThanOrEqualTo("SysId", sysId.ToString()).GetRange(0, 1).ToList();
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
                Logger.Log("Error", "执行GetLastInspectSummaryEntity发生异常," + ex.Message, ex);
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


        /// <summary>
        /// 根据uniqueId标识获取表面缺陷信息
        /// </summary>
        /// <param name="uniqueId"></param>
        /// <param name="entity"></param>
        /// <param name="reason"></param>
        /// <returns></returns>
        public static bool GetICW_LCD_SurfaceResultEntityByUniqueID(string uniqueId, out ICW_LCD_SurfaceResultEntity entity, out string reason)
        {
            reason = string.Empty;
            entity = null;
            try
            {
                var entities = Frame.Frame.Instance.Storage.CreateQuery<ICW_LCD_SurfaceResultEntity>().EqualTo("UniqueID", uniqueId).ToList();
                if (entities != null && entities.Count > 0)
                {
                   //entity = entities.Last();
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
                Logger.Log("Error", "执行GetICW_LCD_SurfaceResultEntityByUniqueID发生异常," + ex.Message, ex);
                return true;
            }
        }


        public static bool GetICW_LCD_SurfaceDefectEntityByUniqueID(string uniqueId, out List<ICW_LCD_SurfaceDefectEntity> entities,
           out string reason)
        {
            reason = string.Empty;
            entities = new List<ICW_LCD_SurfaceDefectEntity>();
            try
            {
                entities = Frame.Frame.Instance.Storage.CreateQuery<ICW_LCD_SurfaceDefectEntity>()
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
                Logger.Log("Error", "执行GetICW_LCD_SurfaceDefectEntityByUniqueID发生异常," + ex.Message, ex);
                return true;
            }
        }

    }
}
