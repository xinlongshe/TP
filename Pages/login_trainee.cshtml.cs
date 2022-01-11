using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Test_4._0.Data;
using Test_4._0.Data.Model;

namespace TrainDEv.Pages
{
    public class login_traineeModel : PageModel
    {
        [TempData]
        public string Message { get; set; }

        public string UserType { get; set; }

        private readonly IDapperRepository<PrivacyUser> _userDapperRepository;
        private readonly IDapperRepository<Trainee> _traineeDapperRepository;
        private readonly IDapperRepository<Trainer> _trainerDapperRepository;
        public login_traineeModel(IDapperRepository<PrivacyUser> userDapperRepository, IDapperRepository<Trainee> traineeDapperRepository, IDapperRepository<Trainer> trainerDapperRepository)
        {
            _userDapperRepository = userDapperRepository;
            _traineeDapperRepository = traineeDapperRepository;
            _trainerDapperRepository = trainerDapperRepository;
        }

        [BindProperty]
        public PrivacyUser User{ get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        public IActionResult OnPostSave()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            string sql = "select * from PrivacyUser where Username='" + User.Username + "' and Password='" + User.Password + "'";
            var list = _userDapperRepository.GetList(sql, null);
            if (list != null && list.Count() > 0)
            {
                var userEntity = list.FirstOrDefault();
                if(userEntity.UserType=="Trainee")
                {
                    return RedirectToPage("trainee_profile");
                }
                else if (userEntity.UserType == "Trainer")
                {
                    return RedirectToPage("trainer_profile");
                }
                else
                {
                    return RedirectToPage("admin");
                }
            }
            else
            {
                TempData["Message"] = "Wrong password or account does not exist";
                return Page();
            }
        }

    }
}
