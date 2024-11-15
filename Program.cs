using DatabaseConnectionADO.Domain;
using DatabaseConnectionADO.Repository;
using System;

namespace DatabaseConnectionADO
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Programa de Cadastro de Pessoas\n" +
                "Este programa serve para demonstrar como funciona a conexão com o Banco de Dados utilizando ADO.NET.");

            var personRepositoryLegacy = new PersonRepository();
            var personRepositoryNormal = new PersonRepository();

            bool continueProgram = true;
            while (continueProgram)
            {
                Console.WriteLine("\nEscolha uma opção:");
                Console.WriteLine("1 - Inserir nova pessoa (Legacy)");
                Console.WriteLine("2 - Obter pessoa por ID (Legacy)");
                Console.WriteLine("3 - Obter lista de todas as pessoas (Legacy)");
                Console.WriteLine("4 - Atualizar pessoa (Legacy)");
                Console.WriteLine("5 - Deletar pessoa (Legacy)");
                Console.WriteLine("6 - Deletar todas as pessoas (Legacy)");
                Console.WriteLine("7 - Inserir nova pessoa (Normal)");
                Console.WriteLine("8 - Obter pessoa por ID (Normal)");
                Console.WriteLine("9 - Obter lista de todas as pessoas (Normal)");
                Console.WriteLine("10 - Atualizar pessoa (Normal)");
                Console.WriteLine("11 - Deletar pessoa (Normal)");
                Console.WriteLine("12 - Deletar todas as pessoas (Normal)");
                Console.WriteLine("0 - Sair");

                string option = Console.ReadLine();

                switch (option)
                {
                    // Legacy Methods
                    case "1":
                        // Insert New Person (Legacy)
                        Console.WriteLine("\nInserir nova pessoa (Legacy)");
                        Console.Write("Nome: ");
                        string name = Console.ReadLine();
                        Console.Write("Email: ");
                        string email = Console.ReadLine();
                        Console.Write("Cidade: ");
                        string city = Console.ReadLine();

                        var newPersonLegacy = new Person
                        {
                            Name = name,
                            Email = email,
                            City = city
                        };

                        personRepositoryLegacy.InsertLEGACY(newPersonLegacy);
                        Console.WriteLine(personRepositoryLegacy.Message); 
                        break;

                    case "2":
                        // Getting a Person By Id (Legacy)
                        Console.WriteLine("\nObter pessoa por ID (Legacy)");
                        Console.Write("ID da pessoa: ");
                        if (int.TryParse(Console.ReadLine(), out int personIdLegacy))
                        {
                            Person person = personRepositoryLegacy.GetByIdLEGACY(personIdLegacy);
                            Console.WriteLine($"ID: {person.Id}, Nome: {person.Name}, Email: {person.Email}, Cidade: {person.City}");
                        }
                        else
                        {
                            Console.WriteLine("ID inválido.");
                        }
                        break;

                    case "3":
                        // Getting all persons (Legacy)
                        Console.WriteLine("\nLista de todas as pessoas (Legacy):");
                        var personsLegacy = personRepositoryLegacy.GetPersonListLEGACY();
                        foreach (var p in personsLegacy)
                        {
                            Console.WriteLine($"ID: {p.Id}, Nome: {p.Name}, Email: {p.Email}, Cidade: {p.City}");
                        }
                        break;

                    case "4":
                        //Updating a person (Legacy)
                        Console.WriteLine("\nAtualizar pessoa (Legacy)");
                        Console.Write("ID da pessoa a ser atualizada: ");
                        if (int.TryParse(Console.ReadLine(), out int updateIdLegacy))
                        {
                            Console.Write("Novo nome: ");
                            string updatedName = Console.ReadLine();
                            Console.Write("Novo email: ");
                            string updatedEmail = Console.ReadLine();
                            Console.Write("Nova cidade: ");
                            string updatedCity = Console.ReadLine();

                            var updatedPersonLegacy = new Person
                            {
                                Id = updateIdLegacy,
                                Name = updatedName,
                                Email = updatedEmail,
                                City = updatedCity
                            };

                            personRepositoryLegacy.UpdateLEGACY(updatedPersonLegacy);
                            Console.WriteLine(personRepositoryLegacy.Message);
                        }
                        else
                        {
                            Console.WriteLine("ID inválido.");
                        }
                        break;

                    case "5":
                        // Deleting a person (Legacy)
                        Console.WriteLine("\nDeletar pessoa (Legacy)");
                        Console.Write("ID da pessoa a ser deletada: ");
                        if (int.TryParse(Console.ReadLine(), out int deleteIdLegacy))
                        {
                            personRepositoryLegacy.DeleteLEGACY(deleteIdLegacy);
                            Console.WriteLine(personRepositoryLegacy.Message);
                        }
                        else
                        {
                            Console.WriteLine("ID inválido.");
                        }
                        break;

                    case "6":
                        //Deleting all persons (Legacy)
                        Console.WriteLine("\nDeletar todas as pessoas (Legacy)");
                        personRepositoryLegacy.DeleteAllLEGACY();
                        Console.WriteLine(personRepositoryLegacy.Message); 
                        break;

                    // Normal Methods
                    case "7":
                        // Insert New Person
                        Console.WriteLine("\nInserir nova pessoa (Normal)");
                        Console.Write("Nome: ");
                        name = Console.ReadLine();
                        Console.Write("Email: ");
                        email = Console.ReadLine();
                        Console.Write("Cidade: ");
                        city = Console.ReadLine();

                        var newPersonNormal = new Person
                        {
                            Name = name,
                            Email = email,
                            City = city
                        };

                        personRepositoryNormal.Insert(newPersonNormal);
                        Console.WriteLine(personRepositoryNormal.Message);
                        break;

                    case "8":
                        // Getting a Person By Id
                        Console.WriteLine("\nObter pessoa por ID (Normal)");
                        Console.Write("ID da pessoa: ");
                        if (int.TryParse(Console.ReadLine(), out int personIdNormal))
                        {
                            Person person = personRepositoryNormal.GetById(personIdNormal);
                            Console.WriteLine($"ID: {person.Id}, Nome: {person.Name}, Email: {person.Email}, Cidade: {person.City}");
                        }
                        else
                        {
                            Console.WriteLine("ID inválido.");
                        }
                        break;

                    case "9":
                        // Getting all persons
                        Console.WriteLine("\nLista de todas as pessoas (Normal):");
                        var personsNormal = personRepositoryNormal.GetPersonList();
                        foreach (var p in personsNormal)
                        {
                            Console.WriteLine($"ID: {p.Id}, Nome: {p.Name}, Email: {p.Email}, Cidade: {p.City}");
                        }
                        break;

                    case "10":
                        //Updating a person
                        Console.WriteLine("\nAtualizar pessoa (Normal)");
                        Console.Write("ID da pessoa a ser atualizada: ");
                        if (int.TryParse(Console.ReadLine(), out int updateIdNormal))
                        {
                            Console.Write("Novo nome: ");
                            string updatedName = Console.ReadLine();
                            Console.Write("Novo email: ");
                            string updatedEmail = Console.ReadLine();
                            Console.Write("Nova cidade: ");
                            string updatedCity = Console.ReadLine();

                            var updatedPersonNormal = new Person
                            {
                                Id = updateIdNormal,
                                Name = updatedName,
                                Email = updatedEmail,
                                City = updatedCity
                            };

                            personRepositoryNormal.Update(updatedPersonNormal);
                            Console.WriteLine(personRepositoryNormal.Message);  
                        }
                        else
                        {
                            Console.WriteLine("ID inválido.");
                        }
                        break;

                    case "11":
                        // Deleting a person
                        Console.WriteLine("\nDeletar pessoa (Normal)");
                        Console.Write("ID da pessoa a ser deletada: ");
                        if (int.TryParse(Console.ReadLine(), out int deleteIdNormal))
                        {
                            personRepositoryNormal.Delete(deleteIdNormal);
                            Console.WriteLine(personRepositoryNormal.Message);
                        }
                        else
                        {
                            Console.WriteLine("ID inválido.");
                        }
                        break;

                    case "12":
                        // Deleting all persons
                        Console.WriteLine("\nDeletar todas as pessoas (Normal)");
                        personRepositoryNormal.DeleteAll();
                        Console.WriteLine(personRepositoryNormal.Message);
                        break;

                    case "0":
                        // Exit
                        continueProgram = false;
                        Console.WriteLine("Saindo do programa...");
                        break;

                    default:
                        Console.WriteLine("Opção inválida! Tente novamente.");
                        break;
                }
            }
        }
    }
}
