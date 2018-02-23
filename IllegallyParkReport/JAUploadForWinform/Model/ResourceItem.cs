using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JAUploadForWinform.Model
{
    public class ResourceItem
    {
        public string ResCode { get; set; }
        public string ResName { get; set; }
        public EmResType ResType { get; set; }
        public string OrgCode { get; set; }
        public string OrgName { get; set; }
    }
    public enum EmResType
    {
        IMOS_TYPE_ORG = 1, /**< 组织域 */
        IMOS_TYPE_OUTER_DOMAIN = 2, /**< 外域 */
        IMOS_TYPE_LOCAL_DOMAIN = 3, /**< 本域 */
        IMOS_TYPE_DEVICE_MIN_VALUE = 10, /**< 设备资源类型最小值 */
        IMOS_TYPE_DM = 11, /**< DM */
        IMOS_TYPE_MS = 12, /**< MS */
        IMOS_TYPE_VX500 = 13, /**< VX500 */
        IMOS_TYPE_MONITOR = 14, /**< 监视器 */
        IMOS_TYPE_EC = 15, /**< EC */
        IMOS_TYPE_DC = 16, /**< DC */
        IMOS_TYPE_GENERAL = 17, /**< 通用设备 */
        IMOS_TYPE_ECR = 18, /**< ECR */
        IMOS_TYPE_TS = 19, /**< TS */
        IMOS_TYPE_LS = 20, /**<LS 日志审计服务器*/
        IMOS_TYPE_VOD = 21, /**<VOD*/
        IMOS_TYPE_TMS = 30, /**< 交通媒体交换服务器 */
        IMOS_TYPE_TOLLGATE = 31, /**< 卡口 */
        IMOS_TYPE_DR = 32, /**< 数据搜索服务器 */
        IMOS_TYPE_MAPM = 33, /**< 地图服务器 */
        IMOS_TYPE_IAHR = 34, /**< 智能人脸卡口服务器 */
        IMOS_TYPE_CDS = 35, /**< CDS 系统 */
        IMOS_TYPE_IOT = 36, /**< 物联网设备 */
        IMOS_TYPE_DB8500 = 40, /**< 数据存储服务器 - DB8500 */
        IMOS_TYPE_DB9500 = 41, /**< 数据存储服务器 - DB9500 */
        IMOS_TYPE_MCU = 201, /**< MCU */
        IMOS_TYPE_MG = 202, /**< MG */
        IMOS_TYPE_CAMERA = 1001, /**< 摄像机 */
        IMOS_TYPE_TOLLGATE_CAMERA = 1002, /**< 卡口相机 */
        IMOS_TYPE_ALARM_SOURCE = 1003, /**< 告警源 */
        IMOS_TYPE_STORAGE_DEV = 1004, /**< 存储设备 */
        IMOS_TYPE_TRANS_CHANNEL = 1005, /**< 透明通道 */
        IMOS_TYPE_ICSCI = 1006, /**< 图侦客户端 */
        IMOS_TYPE_ALARM_OUTPUT = 1200, /**< 告警输出 */
        IMOS_TYPE_BM = 1300, /**< BM */
        IMOS_TYPE_SEMAPHORE_OUTPUT = 1400, /**<开关量输出 */
        IMOS_TYPE_CDM = 1500, /**< CDM */
        IMOS_TYPE_CDV = 1501, /**< CDV */
        IMOS_TYPE_A8 = 1502, /**< A8 设备 */
        IMOS_TYPE_FLASHLIGHT = 1999, /**< 闪光灯 */
        IMOS_TYPE_DEVICE_MAX_VALUE = 2000, /**< 设备资源类型最大值 */
        IMOS_TYPE_GUARD_TOUR_RESOURCE = 2001, /**< 轮切资源 */
        IMOS_TYPE_GUARD_TOUR_PLAN = 2002, /**< 轮切计划 */
        IMOS_TYPE_MAP = 2003, /**< 地图 */
        IMOS_TYPE_XP = 2005, /**< XP */
        IMOS_TYPE_XP_WIN = 2006, /**< XP 窗格 */
        IMOS_TYPE_GUARD_PLAN = 2007, /**< 布防计划 */
        IMOS_TYPE_DEV_ALL = 2008, /**< 所有的设备类型(EC/DC/MS/DM/VX500/摄像头/监视器) */
        IMOS_TYPE_GUARD_POSITION_PLAN = 2009, /**< 看守计划 */
        IMOS_TYPE_WINDOW_A8 = 2999, /**< A8 窗口 */
        IMOS_TYPE_TV_WALL_A8 = 3000, /**< A8 电视墙 */
        IMOS_TYPE_TV_WALL = 3001, /**< 电视墙 */
        IMOS_TYPE_CAMERA_GROUP = 3002, /**< 摄像机组 */
        IMOS_TYPE_MONITOR_GROUP = 3003, /**< 监视器组 */
        IMOS_TYPE_SALVO_RESOURCE = 3004, /**< 组显示资源 */
        IMOS_TYPE_BROADCAST_GROUP = 3005, /**< 广播组 */
        IMOS_TYPE_IMAGE_MOSAIC = 3006, /**< 图像拼接资源 */
        IMOS_TYPE_BALLLINKAGE_GROUP = 3007, /**< 枪球联动组资源 */
        IMOS_TYPE_GROUP_SWITCH_RESOURCE = 3010, /**< 组轮巡资源 */
        IMOS_TYPE_GROUP_SWITCH_PLAN = 3011, /**< 组轮巡计划资源 */
        IMOS_TYPE_AUTO_SWITCH_RESOURCE = 3012, /**< 自动布局轮巡资源 */
        IMOS_TYPE_GROUP_SWITCH_RESOURCE_BOTH = 3013, /**< 两种(自动、组显示)轮巡资源 */
        IMOS_TYPE_SCENE = 3015, /**< 场 景 资 源 ( 参 见 DAO 宏 定 义 #DAO_RES_AGG_TYPE_SCENE,如有改动，必须同步) */
        IMOS_TYPE_ACTION_PLAN = 3016, /**< 预案资源 */
        IMOS_TYPE_TRUNK = 3050, /**< 干线 */
        IMOS_TYPE_CONFERENCE = 4001, /**< 会议资源 */
        IMOS_TYPE_USER = 5001, /**< 用户资源 */
        IMOS_TYPE_MICROPHONE = 8001, /**< 语音对讲资源 */
        IMOS_TYPE_SPEAKER = 8002, /**< 语音广播资源 */
        IMOS_TYPE_AUDIO_INPUT = 8003, /**< 独立音频输入 */
        IMOS_TYPE_AUDIO_OUTPUT = 8004, /**< 独立音频输出 */
        IMOS_TYPE_IMAGE_MOSAIC_V2 = 9000, /**< ADU 图像拼接资源 */
    }
}
