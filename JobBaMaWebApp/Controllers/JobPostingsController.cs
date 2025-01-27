using JobBaMaWebApp.Constants;
using JobBaMaWebApp.Models;
using JobBaMaWebApp.Repositories;
using JobBaMaWebApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace JobBaMaWebApp.Controllers
{
    [Authorize]
    public class JobPostingsController : Controller
    {
        private readonly IRepository<JobPosting> _repository;
        private readonly UserManager<IdentityUser> _userManager;

        public JobPostingsController(
            IRepository<JobPosting> repository,
            UserManager<IdentityUser> userManager)
        {
            _repository = repository;
            _userManager = userManager;
        }
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var jobPostings = await _repository.GetAllAsync();

            if (User.IsInRole(Roles.Employer))
            {
                var userId = _userManager.GetUserId(User);
                var filteredJobPostings = jobPostings.Where(x => x.UserId == userId);
                return View(filteredJobPostings);
            }
            return View(jobPostings);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Employer")]
        public async Task<IActionResult> Create(JobPostingViewModel jobPostingViewModel)
        {
            if (ModelState.IsValid)
            {
                var jobPosting = new JobPosting
                {
                    Title = jobPostingViewModel.Title,
                    Description = jobPostingViewModel.Description,
                    Company = jobPostingViewModel.Company,
                    Location = jobPostingViewModel.Location,
                    UserId = _userManager.GetUserId(User)
                };
                await _repository.AddAsync(jobPosting);

                return RedirectToAction(nameof(Index));
            }
            return View(jobPostingViewModel);
        }

        [HttpDelete]

        public async Task<IActionResult> Delete(int id)
        {
            var jobPosting =  await _repository.GetByIdAsync(id);
            var userId = _userManager.GetUserId(User);

            if (jobPosting == null)
            {
                return NotFound();
            }

            if (User.IsInRole(Roles.Admin) == false && jobPosting.UserId != userId)
            {
                return Forbid();
            }
            await _repository.DeleteAsync(id);

            return Ok();
        }
    }
}
