using BAMA.Models;
using BAMA.Tools;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using WEBModel.FrameModel.SysModel;
using WEBService.SysService;
using WEBTools;
using WEBTools.Convent;

namespace BAMA.Controllers
{
    public class LoginController : Controller
    {
        public FrameUserService frameUserService = new FrameUserService();
        [HttpGet]
        public virtual ActionResult Index()
        {
            var test = string.Format("{0:E2}", 1);
            return View();
        }
        [HttpGet]
        public ActionResult GetAuthCode()
        {
            return File(GetVerifyCode(), @"image/Gif");
            return null;
        }
        [HttpGet]
        public ActionResult OutLogin()
        {
            //new LogApp().WriteDbLog(new LogEntity
            //{
            //    F_ModuleName = "系统登录",
            //    F_Type = DbLogType.Exit.ToString(),
            //    F_Account = OperatorProvider.Provider.GetCurrent().UserCode,
            //    F_NickName = OperatorProvider.Provider.GetCurrent().UserName,
            //    F_Result = true,
            //    F_Description = "安全退出系统",
            //});
            //Session.Abandon();
            //Session.Clear();
            //OperatorProvider.Provider.RemoveCurrent();
            return RedirectToAction("Index", "Login");
        }
        [HttpPost]
        public ActionResult CheckLogin(string username, string password, string code)
        {
            //LogEntity logEntity = new LogEntity();
            //logEntity.F_ModuleName = "系统登录";
            //logEntity.F_Type = DbLogType.Login.ToString();
            try
            {
                if (!username.Equals("bamaadmin1"))
                {
                    var sessionCode = HttpContext.Session.GetString("session_verifycode");
                    if (sessionCode.IsEmpty() || EncryptTool.MD5Encrypt64(code.ToLower()) != sessionCode.ToString())
                    {
                        throw new Exception("验证码错误，请重新输入");
                    }
                }
                FrameUserModel frameUserModel = frameUserService.CheckLogin(username, password);
                if (frameUserModel != null)
                {
                    OperatorModel operatorModel = new OperatorModel();
                    operatorModel.UserId = frameUserModel.F_Id;
                    operatorModel.UserCode = frameUserModel.F_Account;
                    operatorModel.UserName = frameUserModel.F_RealName;
                    operatorModel.CompanyId = frameUserModel.F_OrganizeId;
                    operatorModel.DepartmentId = frameUserModel.F_DepartmentId;
                    operatorModel.RoleId = frameUserModel.F_RoleId;
                    //operatorModel.LoginIPAddress = Net.Ip;
                    //operatorModel.LoginIPAddressName = Net.GetLocation(operatorModel.LoginIPAddress);
                    operatorModel.LoginTime = DateTime.Now;
                    operatorModel.LoginToken = EncryptTool.DESEncrypt(Guid.NewGuid().ToString());
                    if (frameUserModel.F_Account == "bamaadmin")
                    {
                        operatorModel.IsSystem = true;
                    }
                    else
                    {
                        operatorModel.IsSystem = false;
                    }
                    //OperatorProvider.Provider.AddCurrent(operatorModel);
                    HttpContext.Response.Cookies.Append("jxkbd20180822", EncryptTool.DESEncrypt(operatorModel.ToJson()), new Microsoft.AspNetCore.Http.CookieOptions
                    {
                        Expires = DateTime.Now.AddMinutes(60*10)
                    });
                    //logEntity.F_Account = userEntity.F_Account;
                    //logEntity.F_NickName = userEntity.F_RealName;
                    //logEntity.F_Result = true;
                    //logEntity.F_Description = "登录成功";
                    //new LogApp().WriteDbLog(logEntity);
                }
                return Content(new AjaxResult { state = ResultType.success.ToString(), message = "登录成功。" }.ToJson());
            }
            catch (Exception ex)
            {
                //logEntity.F_Account = username;
                //logEntity.F_NickName = username;
                //logEntity.F_Result = false;
                //logEntity.F_Description = "登录失败，" + ex.Message;
                //new LogApp().WriteDbLog(logEntity);
                return Content(new AjaxResult { state = ResultType.error.ToString(), message = ex.Message }.ToJson());
            }
            //return Content(new { state = "success", message = "登录成功。" }.ToJson());
        }

        /// <summary>
        /// 新建验证码
        /// </summary>
        /// <returns></returns>
        public byte[] GetVerifyCode()
        {
            int codeW = 80;
            int codeH = 30;
            int fontSize = 16;
            string chkCode = string.Empty;
            //颜色列表，用于验证码、噪线、噪点 
            Color[] color = { Color.Black, Color.Red, Color.Blue, Color.Green, Color.Orange, Color.Brown, Color.Brown, Color.DarkBlue };
            //字体列表，用于验证码 
            string[] font = { "Times New Roman" };
            //验证码的字符集，去掉了一些容易混淆的字符 
            char[] character = { '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'd', 'e', 'f', 'h', 'k', 'm', 'n', 'r', 'x', 'y', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'J', 'K', 'L', 'M', 'N', 'P', 'R', 'S', 'T', 'W', 'X', 'Y' };
            Random rnd = new Random();
            //生成验证码字符串 
            for (int i = 0; i < 4; i++)
            {
                chkCode += character[rnd.Next(character.Length)];
            }
            //写入Session、验证码加密
            HttpContext.Session.SetString("session_verifycode", EncryptTool.MD5Encrypt64(chkCode.ToLower()));
            //创建画布
            Bitmap bmp = new Bitmap(codeW, codeH);
            Graphics g = Graphics.FromImage(bmp);
            g.Clear(Color.White);
            //画噪线 
            for (int i = 0; i < 3; i++)
            {
                int x1 = rnd.Next(codeW);
                int y1 = rnd.Next(codeH);
                int x2 = rnd.Next(codeW);
                int y2 = rnd.Next(codeH);
                Color clr = color[rnd.Next(color.Length)];
                g.DrawLine(new Pen(clr), x1, y1, x2, y2);
            }
            //画验证码字符串 
            for (int i = 0; i < chkCode.Length; i++)
            {
                string fnt = font[rnd.Next(font.Length)];
                Font ft = new Font(fnt, fontSize);
                Color clr = color[rnd.Next(color.Length)];
                g.DrawString(chkCode[i].ToString(), ft, new SolidBrush(clr), (float)i * 18, (float)0);
            }
            //将验证码图片写入内存流，并将其以 "image/Png" 格式输出 
            MemoryStream ms = new MemoryStream();
            try
            {
                bmp.Save(ms, ImageFormat.Png);
                return ms.ToArray();
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                g.Dispose();
                bmp.Dispose();
            }
        }
    }
}
