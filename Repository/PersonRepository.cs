using DatabaseConnectionADO.Domain;
using DatabaseConnectionADO.Infrastructure;
using MySql.Data.MySqlClient;
using System.Reflection.PortableExecutable;

namespace DatabaseConnectionADO.Repository
{
    public class PersonRepository
    {
        private readonly DbConnection _dbConnection;
        public string? Message;

        public PersonRepository()
        {
            _dbConnection = new DbConnection();
        }

        //CREATE: Insert Person into the Database
        public void Insert(Person person)
        {
            using (var connection = _dbConnection.Connect())
            {
                var command = new MySqlCommand("INSERT INTO Person (FullName, Email, City) VALUES (@FullName, @Email, @City)", connection);
                command.Parameters.AddWithValue("@FullName", person.Name);
                command.Parameters.AddWithValue("@Email", person.Email);
                command.Parameters.AddWithValue("@City", person.City);
                command.ExecuteNonQuery();
            }
        }

        // READ: Get Person from the Database
        public Person GetById(int personId)
        {
            Person person = new Person();
            const string commandText = "SELECT * FROM Person WHERE Id = @PersonId";

            try
            {
                using (var cmd = new MySqlCommand(commandText, _dbConnection.Connect()))
                {
                    cmd.Parameters.AddWithValue("@PersonId", personId);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            person = new Person
                            {
                                Id    = reader.GetInt32("Id"),
                                Name  = reader.GetString("FullName"),
                                Email = reader.GetString("Email"),
                                City  = reader.GetString("City")
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Message = $"Erro ao Conectar: {ex.Message}\n{ex.StackTrace}";
            }
            finally
            {
                _dbConnection.Disconnect();
            }

            return person;
        }

        // READ: Get all persons
        public List<Person> GetPersonList()
        {
            var persons = new List<Person>();
            const string commandText = "SELECT * FROM Person";

            try
            {
                using (var cmd = new MySqlCommand(commandText, _dbConnection.Connect()))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            persons.Add(new Person
                            {
                                Id = reader.GetInt32("Id"),
                                Name = reader.GetString("FullName"),
                                Email = reader.GetString("Email"),
                                City = reader.GetString("City")
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Message = $"Erro ao Conectar: {ex.Message}\n{ex.StackTrace}";
            }
            finally
            {
                _dbConnection.Disconnect();
            }

            return persons;
        }

        // UPDATE: Update the data of a person
        public void Update(Person person)
        {
            const string commandText = "UPDATE Person SET FullName = @FullName, Email = @Email, City = @City WHERE Id = @PersonId";

            try
            {
                using (var cmd = new MySqlCommand(commandText, _dbConnection.Connect()))
                {
                    cmd.Parameters.AddWithValue("@FullName", person.Name);
                    cmd.Parameters.AddWithValue("@Email", person.Email);
                    cmd.Parameters.AddWithValue("@City", person.City);
                    cmd.Parameters.AddWithValue("@PersonId", person.Id);

                    cmd.ExecuteNonQuery();
                    Message = "Dados atualizados com sucesso";
                }
            }
            catch (Exception ex)
            {
                Message = $"Erro ao Conectar: {ex.Message}\n{ex.StackTrace}";
            }
            finally
            {
                _dbConnection.Disconnect();
            }
        }

        // DELETE: Delete a Person
        public void Delete(int personId)
        {
            const string commandText = "DELETE FROM Person WHERE Id = @PersonId";

            try
            {
                using (var cmd = new MySqlCommand(commandText, _dbConnection.Connect()))
                {
                    cmd.Parameters.AddWithValue("@PersonId", personId);
                    cmd.ExecuteNonQuery();
                    Message = "Pessoa excluída com sucesso";
                }
            }
            catch (Exception ex)
            {
                Message = $"Erro ao Conectar: {ex.Message}\n{ex.StackTrace}";
            }
            finally
            {
                _dbConnection.Disconnect();
            }
        }

        // DELETE ALL: Delete all persons from the Database
        public void DeleteAll()
        {
            const string commandText = "DELETE FROM Person";

            try
            {
                using (var cmd = new MySqlCommand(commandText, _dbConnection.Connect()))
                {
                    // Executes DELETE
                    int rowsAffected = cmd.ExecuteNonQuery();

                    // Shows the number of deleted persons
                    if (rowsAffected > 0)
                    {
                        Message = $"{rowsAffected} pessoa(s) deletada(s) com sucesso";
                    }
                    else
                    {
                        Message = "Nenhuma pessoa encontrada para deletar";
                    }
                }
            }
            catch (Exception ex)
            {
                Message = $"Erro ao Deletar Pessoas: {ex.Message}\n{ex.StackTrace}";
            }
            finally
            {
                _dbConnection.Disconnect();
            }
        }


        /*LEGACY METHODS - OLD METHODS*/


        //CREATE: Insert Person into the Database - Old Method
        public void InsertLEGACY(Person person)
        {
            //SQL Command
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "INSERT INTO Person (FullName,Email,City) values (@FullName,@Email,@City)";

            //Parameters
            cmd.Parameters.AddWithValue("@FullName", person.Name);
            cmd.Parameters.AddWithValue("@Email", person.Email);
            cmd.Parameters.AddWithValue("@City", person.City);

            try
            {
                //Connect to Database
                cmd.Connection = _dbConnection.Connect();

                //Execute command
                cmd.ExecuteNonQuery();

                //Alert Message
                this.Message = "Cadastrado com Sucesso";
            }
            catch (Exception ex)
            {
                this.Message = $"Erro ao Conectar:{ex.Message}\n{ex.StackTrace}";
            }
            finally
            {
                _dbConnection.Disconnect();
            }
        }

        // READ: Get Person from the Database - Old Method
        public Person GetByIdLEGACY(int personId)
        {
            Person person = new Person();

            //SQL Command
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "SELECT * FROM Person WHERE PersonId = @PersonId";

            //Parameters
            cmd.Parameters.AddWithValue("@PersonId", personId);

            try
            {
                //Connect to Database
                cmd.Connection = _dbConnection.Connect();

                //Execute command
                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    person.Id    = reader.GetInt32("PersonId");
                    person.Name  = reader.GetString("FullName");
                    person.Email = reader.GetString("Email");
                    person.City  = reader.GetString("City");
                }

                //Alert Message
                this.Message = "Pessoa Resgatada com Sucesso";
            }
            catch (Exception ex)
            {
                this.Message = $"Erro ao Resgatar Pessoa:{ex.Message}\n{ex.StackTrace}";
            }
            finally
            {
                _dbConnection.Disconnect();
            }

            return person;
        }

        // READ: Get all persons - Old Method
        public List<Person> GetPersonListLEGACY()
        {
            List<Person> persons = new List<Person>();
            string commandText = "SELECT * FROM Person";

            //SQL Command
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = commandText;

            try
            {
                // Connect to Database
                cmd.Connection = _dbConnection.Connect();

                // Execute command
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Person person = new Person
                    {
                        Id    = reader.GetInt32("Id"),
                        Name  = reader.GetString("FullName"),
                        Email = reader.GetString("Email"),
                        City  = reader.GetString("City")
                    };
                    persons.Add(person);
                }

                reader.Close();
                Message = "Pessoas Resgatadas com Sucesso";
            }
            catch (Exception ex)
            {
                Message = $"Erro ao Conectar: {ex.Message}\n{ex.StackTrace}";
            }
            finally
            {
                _dbConnection.Disconnect();
            }

            return persons;
        }

        // UPDATE: Update a person in the Database
        public void UpdateLEGACY(Person person)
        {
            string commandText = "UPDATE Person SET FullName = @FullName, Email = @Email, City = @City WHERE Id = @PersonId";

            //SQL Command
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = commandText;

            // Parameters
            cmd.Parameters.AddWithValue("@FullName", person.Name);
            cmd.Parameters.AddWithValue("@Email", person.Email);
            cmd.Parameters.AddWithValue("@City", person.City);
            cmd.Parameters.AddWithValue("@PersonId", person.Id);

            try
            {
                // Conectar ao Banco de Dados
                cmd.Connection = _dbConnection.Connect();

                // Executar o comando
                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    Message = "Pessoa atualizada com sucesso";
                }
                else
                {
                    Message = "Nenhuma pessoa foi encontrada para atualizar";
                }
            }
            catch (Exception ex)
            {
                Message = $"Erro ao Atualizar Pessoa: {ex.Message}\n{ex.StackTrace}";
            }
            finally
            {
                // Desconectar do Banco de Dados
                _dbConnection.Disconnect();
            }
        }

        // DELETE: Delete a person from the Database
        public void DeleteLEGACY(int personId)
        {
            string commandText = "DELETE FROM Person WHERE Id = @PersonId";

            //SQL Command
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = commandText;

            // Parameters
            cmd.Parameters.AddWithValue("@PersonId", personId);

            try
            {
                // Conectar ao Banco de Dados
                cmd.Connection = _dbConnection.Connect();

                // Executar o comando
                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    Message = "Pessoa deletada com sucesso";
                }
                else
                {
                    Message = "Nenhuma pessoa foi encontrada para deletar";
                }
            }
            catch (Exception ex)
            {
                Message = $"Erro ao Deletar Pessoa: {ex.Message}\n{ex.StackTrace}";
            }
            finally
            {
                // Desconectar do Banco de Dados
                _dbConnection.Disconnect();
            }
        }

        // DELETE ALL: Delete all persons from the Database (legacy)
        public void DeleteAllLEGACY()
        {
            string commandText = "DELETE FROM Person";

            // SQL Command
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = commandText;

            try
            {
                // Connect to the Database
                cmd.Connection = _dbConnection.Connect();

                // Executes DELETE Command
                int rowsAffected = cmd.ExecuteNonQuery();

                // Shows the number of deleted persons
                if (rowsAffected > 0)
                {
                    Message = $"{rowsAffected} pessoa(s) deletada(s) com sucesso";
                }
                else
                {
                    Message = "Nenhuma pessoa encontrada para deletar";
                }
            }
            catch (Exception ex)
            {
                Message = $"Erro ao Deletar Pessoas: {ex.Message}\n{ex.StackTrace}";
            }
            finally
            {
                // Disconnect from the Database
                _dbConnection.Disconnect();
            }
        }

    }

}
