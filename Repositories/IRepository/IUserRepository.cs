using Domain.Entities;
using Domain.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity; 

namespace Repositories.IRepository
{
    public interface IUserRepository 
    {
        public Task<string> SignIn(SignInModel userModel);
        public Task<IdentityResult> Insert(SignUpModel entity);  
        public Task SaveChanges();
        public AuthenticationProperties SignInWithGoogle(string redirectUrl);

        public Task<string> SendResponseCallbackFromGoogle();
    }
}
