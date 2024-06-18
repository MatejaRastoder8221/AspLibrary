using Library.Application.UseCases.Commands;
using Library.DataAccess;
using Library.domain;
using FluentValidation;
using BCrypt.Net;
using Library.Application.UseCases.Commands.Users;
using Library.Implementation.Validators;
using Library.Application.DTO;

namespace Library.Implementation.UseCases.Commands
{
    public class EfRegisterUserCommand : EfUseCase, IRegisterUserCommand
    {
        public int Id => 2;

        public string Name => "UserRegistration";

        public string Description => "Registering a user";

        private readonly RegisterUserDtoValidator _validator;

        public EfRegisterUserCommand(AspContext context, RegisterUserDtoValidator validator)
            : base(context)
        {
            _validator = validator;
        }

        public void Execute(RegisterUserDto data)
        {
            // Validate the incoming data
            _validator.ValidateAndThrow(data);

            // Default image path
            string defaultImagePath = Path.Combine("images", "default_user.png");

            // Create the user object
            var user = new User
            {
                BirthDate = data.BirthDate,
                Email = data.Email,
                FirstName = data.FirstName,
                LastName = data.LastName,
                Password = BCrypt.Net.BCrypt.HashPassword(data.Password),
                Image = Context.Images.FirstOrDefault(x => x.Path.Contains(defaultImagePath)) ?? new Image { Path = defaultImagePath },
                Username = data.Username,
                UserUseCases = new List<UserUseCase>
                {
                    new UserUseCase { UseCaseId = 5 }
                },
                CreatedAt = DateTime.Now,  // Set CreatedAt explicitly, though it should default
                IsActive = true           // Set IsActive explicitly, though it should default
            };

            try
            {
                // Add the user to the context
                Context.Users.Add(user);

                // Save changes to the database
                Context.SaveChanges();
            }
            catch (Exception ex)
            {
                // Handle exceptions (optional: log the error)
                throw new ApplicationException("An error occurred while registering the user.", ex);
            }
        }
    }
}
