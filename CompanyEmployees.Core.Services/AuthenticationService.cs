using AutoMapper;
using CompanyEmployees.Core.Domain.Entities;
using CompanyEmployees.Core.Services.Abstractions;
using LoggingService;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Shared.DataTransferObjects;

namespace CompanyEmployees.Core.Services
{
    public sealed class AuthenticationService : IAuthenticationService
    {
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AuthenticationService(ILoggerManager logger, IMapper mapper,
            UserManager<User> userManager, IConfiguration configuration, RoleManager<IdentityRole> roleManager)
        {
            _logger = logger;
            _mapper = mapper;
            _userManager = userManager;
            _configuration = configuration;
            _roleManager = roleManager;
        }

        public async Task<IdentityResult> RegisterUser(UserForRegistrationDto userForRegistration)
        {

            var invalidRoles = new List<string>();

            foreach (var role in userForRegistration.Roles ?? Enumerable.Empty<string>())
            {
                if (string.IsNullOrWhiteSpace(role))
                    continue;

                if (!await _roleManager.RoleExistsAsync(role))
                {
                    invalidRoles.Add(role);
                }
            }

            if (invalidRoles.Any())
            {
                var errors = invalidRoles.Select(role => new IdentityError
                {
                    Code = "InvalidRole",
                    Description = $"Role '{role}' does not exist."
                }).ToArray();

                return IdentityResult.Failed(errors);
            }


            var user = _mapper.Map<User>(userForRegistration);
            var result = await _userManager.CreateAsync(user, userForRegistration.Password!);
            if (result.Succeeded)
                await _userManager.AddToRolesAsync(user, userForRegistration.Roles!);
            return result;
        }
    }
}
