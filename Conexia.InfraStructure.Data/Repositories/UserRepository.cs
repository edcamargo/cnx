using Conexia.Domain.Entities;
using Conexia.InfraStructure.Data.Context;
using Conexia.Domain.Repositories;
using Conexia.Domain.Shared.Facades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Conexia.InfraStructure.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        private readonly IEncryptionFacade _encryption;

        public UserRepository(DataContext context, IEncryptionFacade encryption)
        {
            _context = context;
            _encryption = encryption;
        }

        public void Create(User user)
        {
            _context.User.Add(user);
            _context.SaveChanges();
        }
        public void Update(User user)
        {
            _context.Entry(user).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete(User user)
        {
            _context.User.Remove(user);
            _context.SaveChanges();
        }

        public IEnumerable<User> GetAll()
        {
            return _context.User.AsNoTracking().OrderBy(x => x.Name);
        }

        public User GetById(Guid id)
        {
            return _context.User.FirstOrDefault(x => x.Id == id);
        }

        public User GetByEmail(string email)
        {
            return _context.User.FirstOrDefault(x => x.Email == email);
        }

        public User GetPasswordById(Guid id)
        {
            return (User)_context.User
                .AsNoTracking()
                .Where(x => x.Id.Equals(id));
        }

        public User Autenticate(string email, string password)
        {
            return _context.User.FirstOrDefault(x => x.Email == email && x.Password == password);
        }
    }
}
