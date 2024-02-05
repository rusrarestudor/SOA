using REST.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace REST.Repository
{
    public class DCandidateRepository
    {
        private readonly List<DCandidate> _candidates = new List<DCandidate>();

        public DCandidateRepository()
        {
            _candidates.Add(new DCandidate { id = 1, fullName = "John Doe", mobile = "123-456-7890", email = "johndoe@email.com", age = 30, bloodGroup = "A+", address = "123 Elm St" });
            _candidates.Add(new DCandidate { id = 2, fullName = "Jane Smith", mobile = "234-567-8901", email = "janesmith@email.com", age = 28, bloodGroup = "B+", address = "456 Oak St" });
            _candidates.Add(new DCandidate { id = 3, fullName = "Bob Johnson", mobile = "345-678-9012", email = "bobjohnson@email.com", age = 35, bloodGroup = "AB+", address = "789 Pine St" });
            _candidates.Add(new DCandidate { id = 4, fullName = "Alice Williams", mobile = "456-789-0123", email = "alicewilliams@email.com", age = 32, bloodGroup = "O-", address = "321 Maple St" });
            _candidates.Add(new DCandidate { id = 5, fullName = "Chris Brown", mobile = "567-890-1234", email = "chrisbrown@email.com", age = 29, bloodGroup = "A-", address = "654 Spruce St" });
            _candidates.Add(new DCandidate { id = 6, fullName = "Diana Prince", mobile = "678-901-2345", email = "dianaprince@email.com", age = 27, bloodGroup = "B-", address = "987 Cedar St" });
            _candidates.Add(new DCandidate { id = 7, fullName = "Ethan Hunt", mobile = "789-012-3456", email = "ethanhunt@email.com", age = 34, bloodGroup = "AB-", address = "159 Dogwood St" });
            _candidates.Add(new DCandidate { id = 8, fullName = "Fiona Gallagher", mobile = "890-123-4567", email = "fionagallagher@email.com", age = 30, bloodGroup = "O+", address = "753 Hemlock St" });
            _candidates.Add(new DCandidate { id = 9, fullName = "George King", mobile = "901-234-5678", email = "georgeking@email.com", age = 31, bloodGroup = "A+", address = "357 Redwood St" });
            _candidates.Add(new DCandidate { id = 10, fullName = "Hannah Abbott", mobile = "012-345-6789", email = "hannahabbott@email.com", age = 26, bloodGroup = "B+", address = "951 Cypress St" });
            _candidates.Add(new DCandidate { id = 11, fullName = "Ian Malcolm", mobile = "123-456-7890", email = "ianmalcolm@email.com", age = 29, bloodGroup = "AB+", address = "123 Elm St, Apt 2" });
            _candidates.Add(new DCandidate { id = 12, fullName = "Jill Valentine", mobile = "234-567-8901", email = "jillvalentine@email.com", age = 28, bloodGroup = "O-", address = "456 Oak St, Apt 2" });
            _candidates.Add(new DCandidate { id = 13, fullName = "Kyle Reese", mobile = "345-678-9012", email = "kylereese@email.com", age = 33, bloodGroup = "A-", address = "789 Pine St, Apt 2" });
            _candidates.Add(new DCandidate { id = 14, fullName = "Lara Croft", mobile = "456-789-0123", email = "laracroft@email.com", age = 32, bloodGroup = "B-", address = "321 Maple St, Apt 2" });
        }

        public async Task<List<DCandidate>> GetAllAsync()
        {
            return await Task.FromResult(_candidates);
        }

        public async Task<DCandidate> GetByIdAsync(int id)
        {
            return await Task.FromResult(_candidates.FirstOrDefault(c => c.id == id));
        }

        public async Task AddAsync(DCandidate candidate)
        {
            _candidates.Add(candidate);
            await Task.CompletedTask;
        }

        public async Task UpdateAsync(int id, DCandidate candidate)
        {
            var index = _candidates.FindIndex(c => c.id == id);
            if (index != -1)
            {
                _candidates[index] = candidate;
            }
            await Task.CompletedTask;
        }

        public async Task DeleteAsync(int id)
        {
            var candidate = _candidates.FirstOrDefault(c => c.id == id);
            if (candidate != null)
            {
                _candidates.Remove(candidate);
            }
            await Task.CompletedTask;
        }
    }

}

