using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcMovie.Services
{
    public class FlagService
    {
        private IFlagRepository _repository;

        public FlagService(IFlagRepository repository)
        {
            _repository = repository;
        }

        public Flag GetFlagForCountory(string country)
        {
            return _repository.GetFlag(country);
        }
    }
}
