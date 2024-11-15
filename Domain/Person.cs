using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseConnectionADO.Domain
{
    public class Person
    {
        private int?    _id;
        private string? _name;
        private string? _email;
        private string? _city;

        public int? Id 
        { 
            get => _id; 
            set => _id = value; 
        }

        public string Name
        {
            get => _name;

            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Nome não pode ser vazio.");
                }

                if (value.Length < 3)
                {
                    throw new ArgumentException("Nome deve ter pelo menos 3 caracteres.");
                }

                _name = value;
            }
        }

        public string Email
        {
            get => _email;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("O email não pode ser vazio.");
                }

                // REGEX for email validation
                var emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
                if (!System.Text.RegularExpressions.Regex.IsMatch(value, emailPattern))
                {
                    throw new ArgumentException("O email fornecido é inválido.");
                }

                _email = value;
            }
        }

        public string City
        {
            get => _city;

            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Nome da cidade não pode estar vazio");
                }

                _city = value;
            }
        }

    }
}
