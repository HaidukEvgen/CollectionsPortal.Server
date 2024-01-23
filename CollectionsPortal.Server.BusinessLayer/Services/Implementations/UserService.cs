using AutoMapper;
using CollectionsPortal.Server.BusinessLayer.Exceptions;
using CollectionsPortal.Server.BusinessLayer.Models.User;
using CollectionsPortal.Server.BusinessLayer.Services.Interfaces;
using CollectionsPortal.Server.DataLayer.Models;
using CollectionsPortal.Server.DataLayer.Models.Enums;
using CollectionsPortal.Server.DataLayer.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace CollectionsPortal.Server.BusinessLayer.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(IUserRepository userRepository,
            IJwtTokenGenerator jwtTokenGenerator,
            IMapper mapper,
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager,
            IHttpContextAccessor httpContextAccessor)
        {
            _userRepository = userRepository;
            _jwtTokenGenerator = jwtTokenGenerator;
            _mapper = mapper;
            _userManager = userManager;
            _roleManager = roleManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IEnumerable<UserDto>> GetAllAsync()
        {
            var users = await _userRepository.GetAllAsync();

            return _mapper.Map<IEnumerable<UserDto>>(users);
        }

        public async Task RegisterAsync(RegisterUserDto registerUserDto)
        {
            var user = _mapper.Map<User>(registerUserDto);

            var result = await _userManager.CreateAsync(user, registerUserDto.Password);

            if (!result.Succeeded)
            {
                throw new RegisterException(result.Errors.FirstOrDefault().Description);
            }

            await _userManager.AddToRoleAsync(user, UserRoles.User);
        }

        public async Task<string> LoginAsync(LoginUserDto loginUserDto)
        {
            var user = await _userManager.FindByNameAsync(loginUserDto.UserName);

            var isPasswordCorrect = await _userManager.CheckPasswordAsync(user, loginUserDto.Password);

            if (user == null || isPasswordCorrect == false)
            {
                throw new LoginException();
            }

            if (await IsUserBanned(user.UserName))
            {
                throw new UserBannedException();
            }

            user.LastLogin = DateTime.Now;

            await _userRepository.SaveChangesAsync();

            var userRoles = await _userManager.GetRolesAsync(user);

            var token = _jwtTokenGenerator.GenerateToken(user, userRoles);

            return token;
        }

        public async Task SetStatusesAsync(List<string> userIds, Status status)
        {
            await _userRepository.SetStatusesAsync(userIds, status);
        }

        public async Task DeleteUsersAsync(List<string> userIds)
        {
            await _userRepository.DeleteUsersAsync(userIds);
        }

        public async Task<bool> IsUserBanned(string username)
        {
            var user = await _userManager.FindByNameAsync(username);

            if (user is null)
            {
                return false;
            }

            return user.Status == Status.Blocked;
        }

        public async Task<bool> IsUserExist(string username)
        {
            var user = await _userManager.FindByNameAsync(username);

            return user is not null;
        }

        public async Task<User> GetCurrentUserAsync()
        {
            var id = GetCurrentUserId();
            return await _userManager.FindByIdAsync(id);
        }

        private string GetCurrentUserId()
        {
            return _httpContextAccessor
                .HttpContext
                .User
                .Claims
                .First(x => x.Type == ClaimTypes.NameIdentifier)
                .Value;
        }
    }
}
