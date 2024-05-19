using Exceptions.db;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.validator.creator
{
    public class ValidatorCreator<T> where T : new()
    {
        private static T instance;

        public static T Create()
        {
            if (instance == null)
            {
                instance = new T();
            }
            if (instance == null)
            {
                throw new ValidatorException("Validator instance is null");
            }
            return instance;
        }
    }
}
