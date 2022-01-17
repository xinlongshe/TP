using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Test_4._0.Data;
using Test_4._0.Data.Model;

namespace TrainDEv.Pages
{
    public class trainee_profile_editModel : PageModel
    {
        private readonly IDapperRepository<Trainee> _traineeDapperRepository;
        private readonly IDapperRepository<PrivacyUser> _userDapperRepository;
        public trainee_profile_editModel(IDapperRepository<Trainee> traineeDapperRepository, IDapperRepository<PrivacyUser> userDapperRepository)
        {
            _traineeDapperRepository = traineeDapperRepository;
            _userDapperRepository = userDapperRepository;
        }
        [BindProperty]
        public Trainee Trainee { get; set; }

        [BindProperty]
        public PrivacyUser User { get; set; }
        public void OnGet()
        {
            var value = HttpContext.Session.GetString("UserId");
            if (!string.IsNullOrEmpty(value))
            {
                string sqlUser = "select * from PrivacyUser where FKId=  " + value;
                var privacyUser = _traineeDapperRepository.Query<PrivacyUser>(sqlUser, null)?.FirstOrDefault();
                if (privacyUser != null)
                {
                    User = privacyUser;
                }
                string sql = "select * from Trainee where Id=  " + value;
                var trainee = _traineeDapperRepository.Query<Trainee>(sql, null)?.FirstOrDefault();
                if (trainee != null)
                {
                    Trainee = trainee;
                }
            }
        }
        public IActionResult OnPostSave()
        {
            Trainee.CreateDateTime = DateTime.Now;
            var traineeId = _traineeDapperRepository.Update(Trainee);
            if (traineeId)
            {
                return RedirectToPage("trainee_profile");
            }
            else
            {
                TempData["Message"] = "save failed, please contact the administrator";
                return Page();
            }
        }
    }
}
