using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Test_4._0.Common;
using Test_4._0.Data;
using Test_4._0.Data.Model;

namespace TrainDEv.Pages
{
    public class register_traineeModel : PageModel
    {
        public string Message { get; set; }
        private readonly IDapperRepository<Trainee> _traineeDapperRepository;
        private readonly IDapperRepository<PrivacyUser> _userDapperRepository;
        private readonly ISendMail _sendMail;
        public register_traineeModel(IDapperRepository<Trainee> traineeDapperRepository, ISendMail sendMail, IDapperRepository<PrivacyUser> userDapperRepository)
        {
            _traineeDapperRepository = traineeDapperRepository;
            _userDapperRepository = userDapperRepository;
            _sendMail = sendMail;
        }
        [BindProperty]
        public Trainee Trainee { get; set; }
        [BindProperty]
        public PrivacyUser User { get; set; }
        [BindProperty]
        public EmailCode emailCode { get; set; }
        public IActionResult OnGet()
        {

            return Page();
        }
        public IActionResult OnPostSave()
        {
            Trainee.CreateDateTime = DateTime.Now;
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (emailCode.ValueCode != emailCode.SentCode)
            {
                TempData["Message"] = "Incorrect verification code";
                return Page();
            }
            var traineeId = _traineeDapperRepository.Add(Trainee);
            if (traineeId > 0)
            {
                User.UserType = "Trainee";
                User.FKId = traineeId;
                _userDapperRepository.Add(User);

                return RedirectToPage("login_trainee");
            }
            else
            {
                TempData["Message"] = "Registration failed, please contact the administrator";
                return Page();
            }
        }
        public IActionResult OnPostSendEmail([FromBody] dynamic my)
        {
            string vc = "";
            Random rNum = new Random();//随机生成类
            int num1 = rNum.Next(0, 9);//返回指定范围内的随机数
            int num2 = rNum.Next(0, 9);
            int num3 = rNum.Next(0, 9);
            int num4 = rNum.Next(0, 9);
            int[] nums = new int[4] { num1, num2, num3, num4 };
            for (int i = 0; i < nums.Length; i++)//循环添加四个随机生成数
            {
                vc += nums[i].ToString();
            }
            var returnValue= _sendMail.SendMailAsync(my, "", "PrivacyDB", "Hello"+vc).Result;
            var mes = "Sent successfully";
            if (returnValue == "0")
            {
                mes = "Sending failed, configuration error";
            }
            return new JsonResult(new {Mes= mes, Code=vc});
        }
    }
   
}
