using Domain.Data;
using Domain.Entities;
using Domain.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Repositories.IRepository;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Policy;
using System.Text;
namespace Repositories.Repository
{
    public class AccountRepository : IUserRepository
    {
        private readonly VezeetaDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _Configuration;

        public AccountRepository(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IConfiguration configuration,
            VezeetaDbContext context,
            RoleManager<IdentityRole> roleManager
        )
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _Configuration = configuration;
            _roleManager = roleManager; // Create new roles for entire application
        }

        public async Task<IdentityResult> Insert(SignUpModel entity)
        {
            await CreateRoles();
            
            var patient = new Patient()
            {
                EmailPatient = entity.Email,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                Gender = entity.Gender,
                ImagePath = entity.Image,
                PatientPhone = entity.Phone,
                DateOfBirth = DateTime.Parse(entity.DateOfBirth)
            };

            var user = new ApplicationUser()
            {
                UserName = entity.Email,
                Email = entity.Email,
            };
            _context.Patients.Add(patient);

             await SaveChanges();
             
            var result = await _userManager.CreateAsync(user, entity.Password); 

            if(result != null)
            {
                await _userManager.AddToRoleAsync(user, "PAT");
            }
             
            return result;
        }

        // Create roles [Admin, Patient, Doctor]
        private async Task CreateRoles()
        {
            IdentityRole IdentityRole = new IdentityRole();
            IdentityRole.Name = "PAT";
            IdentityRole.NormalizedName = "PAT";
            await _roleManager.CreateAsync(IdentityRole);
            IdentityRole = new IdentityRole();
            IdentityRole.Name = "DOC";
            IdentityRole.NormalizedName = "DOC";
            await _roleManager.CreateAsync(IdentityRole);
            IdentityRole = new IdentityRole();
            IdentityRole.Name = "ADMIN";
            IdentityRole.NormalizedName = "ADMIN";
            await _roleManager.CreateAsync(IdentityRole);
        }

        public async Task<string> SignIn(SignInModel userModel)
        {
            var result = await _signInManager.PasswordSignInAsync(
                userModel.Email,
                userModel.Password,
                false,
                true
            );

            if (!result.Succeeded)
            {
                return string.Empty;
            }
            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, userModel.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            var authSigninKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(
                _Configuration["JWT:Secret"]
                )
            );

            var token = new JwtSecurityToken(
                issuer: _Configuration["JWT:ValidIssuer"],
                audience: _Configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddDays(1),
                claims: authClaims,
                signingCredentials: new SigningCredentials(
                    authSigninKey, SecurityAlgorithms.HmacSha256Signature
                )
            ); 
            await SaveChanges();
            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        public AuthenticationProperties SignInWithGoogle(string redirectUrl)
        { 
            var properties =  _signInManager
                .ConfigureExternalAuthenticationProperties("Google", redirectUrl);

            return properties;

        }

        public async Task<string> SendResponseCallbackFromGoogle()
        {
            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {

                return "Error loading external login information.";
            }

            // If the user already has a login (i.e if there is a record in AspNetUserLogins
            // table) then sign-in the user with this external login provider
            var signInResult = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider,
                info.ProviderKey, isPersistent: false, bypassTwoFactor: true);

            if (signInResult.Succeeded)
            {
                return "true";
            }
            // If there is no record in AspNetUserLogins table, the user may not have
            // a local account
            else
            {
                // Get the email claim value
                var email = info.Principal.FindFirstValue(ClaimTypes.Email);

                if (email != null)
                {
                    // Create a new user without password if we do not have a user already
                    var user = await _userManager.FindByEmailAsync(email);

                    if (user == null)
                    {
                        user = new ApplicationUser
                        {
                            UserName = info.Principal.FindFirstValue(ClaimTypes.Email),
                            Email = info.Principal.FindFirstValue(ClaimTypes.Email)
                        };

                        await _userManager.CreateAsync(user);
                    }

                    // Add a login (i.e insert a row for the user in AspNetUserLogins table)
                    await _userManager.AddLoginAsync(user, info);
                    await _signInManager.SignInAsync(user, isPersistent: false);

                    return "true";
                }

                // If we cannot find the user email we cannot continue
                string errorMessage = "Email claim not received from: " +info.LoginProvider
                + " .\nPlease contact support on seeifeldina@gmail.com";

                return errorMessage; 
            }
        }
        public async Task SaveChanges()
        { 
            await _context.SaveChangesAsync(); 
        }
    }
}
