using System.Collections.Generic;
using System.Linq;
using BusinessEntities;
using Common;
using Data.Extensions;
using Data.Indexes;
using Raven.Client;

namespace Data.Repositories
{
    [AutoRegister]
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly IDocumentSession _documentSession;

        public UserRepository(IDocumentSession documentSession) : base(documentSession)
        {
            _documentSession = documentSession;
        }

        public IEnumerable<User> Get(UserTypes? userType = null, string name = null, string email = null, IEnumerable<string> tags = null)
        {
            var query = _documentSession.Advanced.DocumentQuery<User, UsersListIndex>();

            var hasFilter = false;

            if (userType != null)
                query = query.AndIfHasFilter(ref hasFilter).WhereEquals("Type", (int)userType);

            if (!string.IsNullOrWhiteSpace(name))
                query = query.AndIfHasFilter(ref hasFilter).Where($"Name:*{name}*");

            if (!string.IsNullOrWhiteSpace(email))
                query = query.AndIfHasFilter(ref hasFilter).WhereEquals("Email", email);

            if (tags != null && tags.Any())
                query = query.AndIfHasFilter(ref hasFilter).WhereIn("Tags", tags.ToArray());

            return query.ToList();
        }

        public void DeleteAll()
        {
            base.DeleteAll<UsersListIndex>();
        }
    }
}