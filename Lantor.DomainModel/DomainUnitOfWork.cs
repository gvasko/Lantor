using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Lantor.DomainModel
{
    public class DomainUnitOfWork : IDomainUnitOfWork
    {
        private IBasicCrudUnitOfWork _basicUow;
        private User _currentUser;

        public DomainUnitOfWork(IBasicCrudUnitOfWork uow)
        {
            _basicUow = uow;
            _currentUser = User.GetNullUser();
        }

        public IBasicCrudUnitOfWork BasicCrudOperations { get => _basicUow; }

        public async Task<Alphabet> CreateAlphabetAsync(string name, int dim)
        {
            var abc = new Alphabet(name, dim, new RandomVectorFactory());
            return await _basicUow.CreateAlphabetAsync(abc);
        }

        public async Task<Alphabet> GetDefaultAlphabetAsync()
        {
            var allAbc = await _basicUow.GetAllAlphabetListInfoAsync();
            var defaultShallowAbc = allAbc.First(a => a.Name == "Default");
            var defaultAbc = await _basicUow.GetAlphabetAsync(defaultShallowAbc.Id);

            if (defaultAbc == null)
            {
                throw new InvalidOperationException("Default alphabet not found");
            }

            return defaultAbc;
        }

        public async Task RemoveAlphabetAsync(int alphabetId)
        {
            // EF Core will cascade delete
            //_basicUow.RemoveAlphabetFromCache(alphabetId);
            await _basicUow.RemoveAlphabetAsync(alphabetId);
        }

        public async Task<MultilingualSample> GetDefaultSamplesAsync()
        {
            var allSamples = await _basicUow.GetAllMultilingualSamplesAsync();
            var defaultSamples = allSamples.First(s => s.Name == "Default");
            return defaultSamples;
        }

        public async Task RemoveMultilingualSampleAsync(int multilingualSampleId)
        {
            // EF Core will cascade delete
            //await _basicUow.RemoveMultilingualSampleFromCacheAsync(multilingualSampleId);
            await _basicUow.RemoveCascadeMultilingualSampleAsync(multilingualSampleId);
        }

        public async Task RemoveLanguageSampleAsync(int sampleId)
        {
            // EF Core will cascade delete
            //_basicUow.RemoveLanguageSampleFromCache(sampleId);
            await _basicUow.RemoveLanguageSampleAsync(sampleId);
        }

        public async Task Save()
        {
            await _basicUow.Save();
        }

        public void UpdateLanguageSample(LanguageSample updated)
        {
            _basicUow.UpdateLanguageSample(updated);
            _basicUow.RemoveLanguageSampleFromCache(updated.Id);
        }

        public async Task<User> EnsureCurrentUserAsync(User user)
        {
            if (user.Id == 0)
            {
                var dbUser = await _basicUow.GetUserByEmailAsync(user.Email);
                if (dbUser == null)
                {
                    dbUser = await _basicUow.CreateUserAsync(user);
                    await _basicUow.Save();
                }
                _currentUser = dbUser;
            }
            else
            {
                var dbUser = await _basicUow.GetUserByIdAsync(user.Id);
                if (dbUser == null)
                {
                    throw new ArgumentException($"User with id {user.Id} does not exist");
                }
                else
                {
                    _currentUser = dbUser;
                }
            }
            return _currentUser;
        }

        public async Task<User> EnsureCurrentUserAsync(string name, string userName, string email, string externalId)
        {

            if (string.IsNullOrEmpty(userName))
            {
                userName = email;
            }

            if (string.IsNullOrEmpty(email))
            {
                email = userName;
            }

            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(email))
            {
                throw new ArgumentException("User cannot be created, userName or email must be provided");
            }

            if (!MailAddress.TryCreate(email, out var _))
            {
                throw new ArgumentException("User email must be provided");
            }

            var userObject = new User(name, userName, email, externalId);

            return await EnsureCurrentUserAsync(userObject);

        }

        public User GetCurrentUser()
        {
            throw new NotImplementedException();
        }
    }
}
