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
        public static bool GetLastProcessResultEntity(Frame.Frame frame,long sysId, out ProcessResultEntity entity, out string reason)
        {
            reason = string.Empty;
            entity = null;
            try
            {
                var entities = frame.Storage.CreateQuery<ProcessResultEntity>()
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
        public static bool GetLastInspectSummaryEntity(Frame.Frame frame,long sysId, out InspectSummaryEntity entity, out string reason)
        {
            reason = string.Empty;
            entity = null;
            try
            {
                var entities = frame.Storage.CreateQuery<InspectSummaryEntity>()
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

        public static bool GetLastCellDFSInfoEntity(Frame.Frame frame, int sendFlag, out CellDFSInfoEntity entity, out string reason)
        {
            reason = string.Empty;
            entity = null;
            try
            {
                var entities = frame.Storage.CreateQuery<CellDFSInfoEntity>()
                     .EqualTo("SendFlag", sendFlag.ToString()).GetRange(0, 1).ToList();
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
                Logger.Log("Error", "执行GetLastCellDFSInfoEntityntity发生异常," + ex.Message, ex);
                return true;
            }
        }

        public static bool SetLastCellDFSInfoEntity(Frame.Frame frame,int sendFlag,CellDFSInfoEntity entity, out string reason)
        {
            entity.SendFlag = sendFlag;
            reason = string.Empty;
            try
            {
                CellDFSInfoEntity[] cellDFSInfoEntities = { entity };
                frame.Storage.Update<CellDFSInfoEntity>(cellDFSInfoEntities);

                return true;
            }
            catch (Exception ex)
            {
                reason = "Update数据发生异常," + ex.Message;
                Logger.Log("Error", "执行SetLastCellDFSInfoEntity发生异常," + ex.Message, ex);

                return false;
            }
        }

        public static bool GetPanelInfoByUniqueId(Frame.Frame frame,string uniqueId,out List<PanelInfoEntity> entities, out string reason)
        {
            reason = string.Empty;
            entities = null;
            try
            {
                entities = frame.Storage.CreateQuery<PanelInfoEntity>().EqualTo("UniqueId", uniqueId).ToList();
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
  
        public static bool GetProcessResultEntityFromUniqueId(Frame.Frame frame,string UniqueID, out List<ProcessResultEntity> entities, out string reason)
        {
            reason = string.Empty;
            entities = new List<ProcessResultEntity>();
            try
            {
                entities = frame.Storage.CreateQuery<ProcessResultEntity>()
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
        public static bool GetICW_LCD_SurfaceResultEntityByUniqueID(Frame.Frame frame, string uniqueId, out ICW_LCD_SurfaceResultEntity entity, out string reason)
        {
            reason = string.Empty;
            entity = null;
            try
            {
                var entities = frame.Storage.CreateQuery<ICW_LCD_SurfaceResultEntity>().EqualTo("UniqueID", uniqueId).ToList();
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
                Logger.Log("Error", "执行GetICW_LCD_SurfaceResultEntityByUniqueID发生异常," + ex.Message, ex);
                return true;
            }
        }


        public static bool GetICW_LCD_SurfaceDefectEntityByUniqueID(Frame.Frame frame, string uniqueId, out List<ICW_LCD_SurfaceDefectEntity> entities,
           out string reason)
        {
            reason = string.Empty;
            entities = new List<ICW_LCD_SurfaceDefectEntity>();
            try
            {
                entities = frame.Storage.CreateQuery<ICW_LCD_SurfaceDefectEntity>()
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
