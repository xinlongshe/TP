using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;
using Test_4._0.Data;
using Test_4._0.Data.Model;

namespace TrainDEv.Pages
{
    public class trainer_profileModel : PageModel
    {
        private readonly IDapperRepository<Trainer> _trainerDapperRepository;
        private readonly IDapperRepository<PrivacyUser> _userDapperRepository;
        public trainer_profileModel(IDapperRepository<Trainer> trainerDapperRepository, IDapperRepository<PrivacyUser> userDapperRepository)
        {
            _trainerDapperRepository = trainerDapperRepository;
            _userDapperRepository = userDapperRepository;
        }
        [BindProperty]
        public Trainer Trainer { get; set; }

        [BindProperty]
        public PrivacyUser User { get; set; }
        public void OnGet()
        {
            var value = HttpContext.Session.GetString("UserId");
            if (!string.IsNullOrEmpty(value))
            {
                string sqlUser = "select * from PrivacyUser where FKId=  " + value;
                var privacyUser = _userDapperRepository.Query<PrivacyUser>(sqlUser, null)?.FirstOrDefault();
                if (privacyUser != null)
                {
                    User = privacyUser;
                }
                string sql = "select * from Trainer where Id=  " + value;
                var trainer = _trainerDapperRepository.Query<Trainer>(sql, null)?.FirstOrDefault();
                if (trainer != null)
                {
                    Trainer = trainer;
                }
            }
        }
    }
}
