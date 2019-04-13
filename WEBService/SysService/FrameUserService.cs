using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WEBDAO.SysDAO;
using WEBModel.FrameDTO.SysDTO;
using WEBModel.FrameModel.SysModel;
using WEBTools;
using WEBTools.Convent;

namespace WEBService.SysService
{
    public class FrameUserService
    {
        public FrameUserDAO frameUserDAO = new FrameUserDAO();
        public FrameUserModel CheckLogin(string username, string password)
        {
            FrameUserModel userEntity = frameUserDAO.Query(t => t.F_Account == username).FirstOrDefault();
            if (userEntity != null)
            {
                if (userEntity.F_EnabledMark == true)
                {
                    //UserLogOnEntity userLogOnEntity = userLogOnApp.GetForm(userEntity.F_Id);
                    string dbPassword = EncryptTool.MD5Encrypt64(password);
                    if (dbPassword == userEntity.F_UserPassword)
                    {
                        //DateTime lastVisitTime = DateTime.Now;
                        //int LogOnCount = (userLogOnEntity.F_LogOnCount).ToInt() + 1;
                        //if (userLogOnEntity.F_LastVisitTime != null)
                        //{
                        //    userLogOnEntity.F_PreviousVisitTime = userLogOnEntity.F_LastVisitTime.ToDate();
                        //}
                        //userLogOnEntity.F_LastVisitTime = lastVisitTime;
                        //userLogOnEntity.F_LogOnCount = LogOnCount;
                        //userLogOnApp.UpdateForm(userLogOnEntity);
                        return userEntity;
                    }
                    else
                    {
                        throw new Exception("密码不正确，请重新输入");
                    }
                }
                else
                {
                    throw new Exception("账户被系统锁定,请联系管理员");
                }
            }
            else
            {
                throw new Exception("账户不存在，请重新输入");
            }
        }

        public List<FrameUserModel> GetList(string keyword , Pagination pagination)
        {
            return frameUserDAO.FindList(keyword, pagination);
        }
        public FrameUserModel GetForm(string keyValue)
        {
            return frameUserDAO.GetById(keyValue);
        }
        public void DeleteForm(string keyValue,string userId)
        {
            frameUserDAO.Delete(keyValue, userId);
        }

        public void SubmitForm(FrameUserDTO userEntity, string keyValue,string userId)
        {
            FrameUserModel frameUserModel = new FrameUserModel();
            if (!string.IsNullOrEmpty(keyValue))
            {
                frameUserModel = frameUserDAO.GetById(keyValue);
                frameUserModel.F_Account = userEntity.F_Account;
                //frameUserModel.F_UserPassword = userEntity.F_UserPassword;
                frameUserModel.F_RealName = userEntity.F_RealName;
                frameUserModel.F_NickName = userEntity.F_NickName;
                frameUserModel.F_HeadIcon = userEntity.F_HeadIcon;
                frameUserModel.F_Gender = userEntity.F_Gender;
                frameUserModel.F_Birthday = userEntity.F_Birthday;
                frameUserModel.F_MobilePhone = userEntity.F_MobilePhone;
                frameUserModel.F_Email = userEntity.F_Email;
                frameUserModel.F_WeChat = userEntity.F_WeChat;
                frameUserModel.F_ManagerId = userEntity.F_ManagerId;
                frameUserModel.F_SecurityLevel = userEntity.F_SecurityLevel;
                frameUserModel.F_Signature = userEntity.F_Signature;
                frameUserModel.F_OrganizeId = userEntity.F_OrganizeId;
                frameUserModel.F_DepartmentId = userEntity.F_DepartmentId;
                frameUserModel.F_RoleId = userEntity.F_RoleId;
                frameUserModel.F_DutyId = userEntity.F_DutyId;
                frameUserModel.F_IsAdministrator = userEntity.F_IsAdministrator;
                frameUserModel.F_SortCode = userEntity.F_SortCode;
                frameUserModel.F_EnabledMark = userEntity.F_EnabledMark;
                frameUserModel.F_Description = userEntity.F_Description;
                frameUserDAO.Update(frameUserModel,userId);
            }
            else
            {
                frameUserModel = AutoMapperHelper.MapTo<FrameUserModel>(userEntity);
                frameUserModel.F_UserPassword = EncryptTool.MD5Encrypt64(userEntity.F_UserPassword);
                frameUserDAO.Create(frameUserModel, userId);
            }
        }

        public void UpdateForm(bool canUsed,string keyValue, string userId)
        {
            FrameUserModel frameUserModel = frameUserDAO.GetById(keyValue);
            frameUserModel.F_EnabledMark = canUsed;
            frameUserDAO.Update(frameUserModel, userId);
        }

        public void RevisePassword(string userPassword, string keyValue, string userId)
        {
            FrameUserModel frameUserModel = frameUserDAO.GetById(keyValue);
            frameUserModel.F_UserPassword = EncryptTool.MD5Encrypt64(userPassword);
            frameUserDAO.Update(frameUserModel, userId);
        }
    }
}
